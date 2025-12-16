using ConsoleHostExample.Services;

namespace ConsoleHostExample;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMetricsCollector _metricsCollector;

    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        IMetricsCollector metricsCollector)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _metricsCollector = metricsCollector;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started. Services auto-registered via Scrutor.AspNetCore!");
        
        var iteration = 0;
        while (!stoppingToken.IsCancellationRequested && iteration < 5)
        {
            iteration++;
            _logger.LogInformation("Worker iteration {Iteration} at: {Time}", iteration, DateTimeOffset.Now);

            // Create a scope for scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                var dataProcessor = scope.ServiceProvider.GetRequiredService<IDataProcessor>();
                
                // Use the auto-registered scoped service
                var result = await dataProcessor.ProcessDataAsync($"Data-{iteration}");
                _logger.LogInformation("Result: {Result}", result);
            }

            // Record metrics using the singleton service (injected directly)
            _metricsCollector.RecordMetric("ProcessedItems", 1);
            _metricsCollector.RecordMetric("TotalIterations", iteration);

            await Task.Delay(2000, stoppingToken);
        }

        // Display final metrics
        _metricsCollector.DisplayMetrics();
        _logger.LogInformation("Worker completed all iterations");
    }
}

using ConsoleHostExample.Services;

namespace ConsoleHostExample;

/// <summary>
/// Background worker that demonstrates service usage with EasyScrutor.
/// </summary>
public class Worker : BackgroundService {
    /// <summary>
    /// Logger instance for this class.
    /// </summary>
    private readonly ILogger<Worker> _logger;

    /// <summary>
    /// Service provider for creating scopes.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Metrics collector service.
    /// </summary>
    private readonly IMetricsCollector _metricsCollector;

    /// <summary>
    /// Initializes a new instance of the <see cref="Worker"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="serviceProvider">The service provider for creating scopes.</param>
    /// <param name="metricsCollector">The metrics collector service.</param>
    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        IMetricsCollector metricsCollector) {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _metricsCollector = metricsCollector;
    }

    /// <summary>
    /// Executes the background worker task.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token to stop the worker.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _logger.LogInformation("Worker started. Services auto-registered via EasyScrutor!");

        var iteration = 0;
        while (!stoppingToken.IsCancellationRequested && iteration < 5) {
            iteration++;
            _logger.LogInformation("Worker iteration {Iteration} at: {Time}", iteration, DateTimeOffset.Now);

            // Create a scope for scoped services
            using (var scope = _serviceProvider.CreateScope()) {
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
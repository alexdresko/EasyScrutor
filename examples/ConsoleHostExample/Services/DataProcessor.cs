using Scrutor.AspNetCore;

namespace ConsoleHostExample.Services;

// Auto-registered as Scoped
public class DataProcessor : IDataProcessor, IScopedLifetime
{
    private readonly ILogger<DataProcessor> _logger;

    public DataProcessor(ILogger<DataProcessor> logger)
    {
        _logger = logger;
    }

    public async Task<string> ProcessDataAsync(string input)
    {
        _logger.LogInformation("Processing data: {Input}", input);
        await Task.Delay(100); // Simulate work
        return $"Processed: {input}";
    }
}

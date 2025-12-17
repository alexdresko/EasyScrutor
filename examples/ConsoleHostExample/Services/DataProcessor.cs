using EasyScrutor;

namespace ConsoleHostExample.Services;

/// <summary>
/// Processes data with scoped lifetime.
/// Auto-registered as Scoped using EasyScrutor.
/// </summary>
public class DataProcessor : IDataProcessor, IScopedLifetime {
    /// <summary>
    /// Logger instance for this class.
    /// </summary>
    private readonly ILogger<DataProcessor> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataProcessor"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public DataProcessor(ILogger<DataProcessor> logger) {
        _logger = logger;
    }

    /// <summary>
    /// Processes the input data asynchronously.
    /// </summary>
    /// <param name="input">The input data to process.</param>
    /// <returns>A task representing the asynchronous operation with the processed result.</returns>
    public async Task<string> ProcessDataAsync(string input) {
        _logger.LogInformation("Processing data: {Input}", input);
        await Task.Delay(100); // Simulate work
        return $"Processed: {input}";
    }
}

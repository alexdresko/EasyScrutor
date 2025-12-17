namespace ConsoleHostExample.Services;

/// <summary>
/// Provides data processing functionality.
/// </summary>
public interface IDataProcessor {
    /// <summary>
    /// Processes the input data asynchronously.
    /// </summary>
    /// <param name="input">The input data to process.</param>
    /// <returns>A task representing the asynchronous operation with the processed result.</returns>
    Task<string> ProcessDataAsync(string input);
}

namespace ConsoleHostExample.Services;

public interface IDataProcessor
{
    Task<string> ProcessDataAsync(string input);
}

namespace ConsoleHostExample.Services;

public interface IMetricsCollector
{
    void RecordMetric(string name, int value);
    void DisplayMetrics();
}

namespace ConsoleHostExample.Services;

/// <summary>
/// Provides metrics collection and reporting functionality.
/// </summary>
public interface IMetricsCollector
{
    /// <summary>
    /// Records a metric value.
    /// </summary>
    /// <param name="name">The name of the metric.</param>
    /// <param name="value">The metric value to record.</param>
    void RecordMetric(string name, int value);
    
    /// <summary>
    /// Displays all collected metrics.
    /// </summary>
    void DisplayMetrics();
}

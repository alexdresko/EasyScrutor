using EasyScrutor;
using System.Collections.Concurrent;

namespace ConsoleHostExample.Services;

/// <summary>
/// Collects and reports metrics with singleton lifetime.
/// Auto-registered as Singleton - shared across the entire application.
/// </summary>
public class MetricsCollector : IMetricsCollector, ISingletonLifetime
{
    /// <summary>
    /// Thread-safe dictionary storing metric values.
    /// </summary>
    private readonly ConcurrentDictionary<string, int> _metrics = new();

    /// <summary>
    /// Logger instance for this class.
    /// </summary>
    private readonly ILogger<MetricsCollector> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsCollector"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public MetricsCollector(ILogger<MetricsCollector> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Records a metric value, adding it to any existing value for the same metric name.
    /// </summary>
    /// <param name="name">The name of the metric.</param>
    /// <param name="value">The metric value to record.</param>
    public void RecordMetric(string name, int value)
    {
        _metrics.AddOrUpdate(name, value, (_, existing) => existing + value);
        _logger.LogDebug("Recorded metric {Name}: {Value}", name, value);
    }

    /// <summary>
    /// Displays all collected metrics to the log.
    /// </summary>
    public void DisplayMetrics()
    {
        _logger.LogInformation("=== Metrics Summary ===");
        foreach (var metric in _metrics)
        {
            _logger.LogInformation("{Name}: {Value}", metric.Key, metric.Value);
        }
    }
}

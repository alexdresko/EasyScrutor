using Scrutor.AspNetCore;
using System.Collections.Concurrent;

namespace ConsoleHostExample.Services;

// Auto-registered as Singleton - shared across the entire application
public class MetricsCollector : IMetricsCollector, ISingletonLifetime
{
    private readonly ConcurrentDictionary<string, int> _metrics = new();
    private readonly ILogger<MetricsCollector> _logger;

    public MetricsCollector(ILogger<MetricsCollector> logger)
    {
        _logger = logger;
    }

    public void RecordMetric(string name, int value)
    {
        _metrics.AddOrUpdate(name, value, (_, existing) => existing + value);
        _logger.LogDebug("Recorded metric {Name}: {Value}", name, value);
    }

    public void DisplayMetrics()
    {
        _logger.LogInformation("=== Metrics Summary ===");
        foreach (var metric in _metrics)
        {
            _logger.LogInformation("{Name}: {Value}", metric.Key, metric.Value);
        }
    }
}

using ConsoleHostExample;

var builder = Host.CreateApplicationBuilder(args);

// Add EasyScrutor - Automatically scans and registers all services
// This will find and register IDataProcessor and IMetricsCollector without manual configuration
builder.Services.AddAdvancedDependencyInjection();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
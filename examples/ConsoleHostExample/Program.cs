using ConsoleHostExample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Add Scrutor.AspNetCore - Automatically scans and registers all services
// This will find and register IDataProcessor and IMetricsCollector without manual configuration
builder.Services.AddAdvancedDependencyInjection();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

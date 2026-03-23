using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using McpSitecoreAiExample;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MonkeyService>();
builder.Services.AddSingleton<SellingService>();
builder.Services.AddSingleton<SitecoreAIService>();

builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();
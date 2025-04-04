using BotSharp.Weather.MCPServer;
using ModelContextProtocol;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMcpServer()
    .WithToolsFromAssembly();
var app = builder.Build();

app.MapGet("/", () => "This is a test server with only stub functionality!");
app.MapMcpSse();

app.Run();
using BotSharp.Core;
using BotSharp.OpenAPI;
using BotSharp.Logger;
using BotSharp.Plugin.ChatHub;
using Serilog;
using BotSharp.Plugin.ChatHub.Sinks;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//// Use Serilog
//Log.Logger = new LoggerConfiguration()
//#if DEBUG
//    .MinimumLevel.Debug()
//#else
//    .MinimumLevel.Warning()
//#endif
//    .WriteTo.Console()
//    .CreateLogger();
//builder.Host.UseSerilog();

// Add BotSharp
builder.Services.AddBotSharpCore(builder.Configuration)
    .AddBotSharpOpenAPI(builder.Configuration, new[]
    {
        "http://localhost:5015",
        "https://botsharp.scisharpstack.org",
        "https://chat.scisharpstack.org"
    }, builder.Environment, true)
    .AddBotSharpLogger(builder.Configuration);

// Add SignalR for WebSocket
builder.Services.AddSignalR();

builder.Host
    .UseSerilog((ctx, services, config) =>
    {
        config.MinimumLevel.Debug()
              .Enrich.FromLogContext()
              .WriteTo.SignalRSink<SignalRHub, ISignalRHub>(LogEventLevel.Debug, services);
    });



var app = builder.Build();

// Enable SignalR
app.MapHub<SignalRHub>("/chatHub");
app.UseMiddleware<WebSocketsMiddleware>();

// Use BotSharp
app.UseBotSharp()
    .UseBotSharpOpenAPI(app.Environment)
    .UseBotSharpUI();

app.Run();

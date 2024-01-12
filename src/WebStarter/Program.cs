using BotSharp.Core;
using BotSharp.OpenAPI;
using BotSharp.Logger;
using BotSharp.Plugin.ChatHub;
using Serilog;
using BotSharp.Plugin.ChatHub.Sinks;
using Serilog.Core;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<LogProducer>();

//// Use Serilog
//Log.Logger = new LoggerConfiguration()
//#if DEBUG
//    .MinimumLevel.Debug()
//#else
//    .MinimumLevel.Warning()
//#endif
//    //.WriteTo.Console()
//    //.WriteTo.File("logs/log-.txt", 
//    //    shared: true, 
//    //    rollingInterval: RollingInterval.Day)
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


builder.Host.ConfigureServices((ctx, services) =>
{
    services.AddTransient<LogProducer>();
    services.AddTransient<ILogEventSink, SignalRSink>();
}).UseSerilog((ctx, services, config) =>
{
    config.MinimumLevel.Debug();
    config.ReadFrom.Services(services.CreateScope().ServiceProvider);
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

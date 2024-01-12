using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Plugin.ChatHub.Sinks;

public class SignalRSink : ILogEventSink
{
    private readonly IFormatProvider _formatProvider;
    private readonly LogProducer _producer;

    /// <summary>
    /// A reasonable default for the number of events posted in
    /// each batch.
    /// </summary>
    public const int DefaultBatchPostingLimit = 5;

    /// <summary>
    /// A reasonable default time to wait between checking for event batches.
    /// </summary>
    public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

    public SignalRSink(
        LogProducer producer)
    {
        _producer = producer;
    }

    public void Emit(LogEvent logEvent)
    {
        Console.WriteLine($"Log event: {logEvent.MessageTemplate.Text}");
        _producer.Produce(logEvent).Wait();
    }
}

public class LogProducer
{
    private readonly IServiceProvider _services;

    public LogProducer(IServiceProvider services)
    {
        _services = services;
    }

    public async Task Produce(LogEvent log)
    {
        var user = _services.GetRequiredService<IUserIdentity>();
        var chatHub = _services.GetRequiredService<IHubContext<SignalRHub>>();
        var properties = log.Properties;
        properties.TryGetValue("ConnectionId", out var connId);
        if (chatHub == null || user?.Id == null) return;

        Console.WriteLine($"Sending message... to user {user?.Id}, ConnectionId {connId}");
        Console.WriteLine($"Sending log... {log.MessageTemplate?.Text}\n\n");

        await chatHub.Clients.All.SendAsync("SendLogEvent", log);
    }

    public async Task Produce(BotSharpLogEvent log)
    {
        var user = _services.GetRequiredService<IUserIdentity>();
        var chatHub = _services.GetRequiredService<IHubContext<SignalRHub>>();
        var properties = log.Properties;
        properties.TryGetValue("ConnectionId", out var connId);
        if (chatHub == null || user?.Id == null) return;

        Console.WriteLine($"Sending message... to user {user?.Id}, ConnectionId {connId}");
        Console.WriteLine($"Sending log... {log.MessageTemplate} {log.RenderedMessage}\n\n");
        if (log.MessageTemplate.Contains("Connection"))
        {
            await chatHub.Clients.All.SendAsync("SendLogEvent", log);
        }
        else
        {
            await chatHub.Clients.All.SendAsync("SendLogEvent", log);
        }
    }
}
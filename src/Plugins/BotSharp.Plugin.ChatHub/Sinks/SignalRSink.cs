using Microsoft.AspNetCore.SignalR;
using Serilog.Core;
using Serilog.Events;
namespace BotSharp.Plugin.ChatHub.Sinks;

public class SignalRSink<THub, T> : ILogEventSink where THub : Hub<T> where T : class, IHub
{
    private readonly IServiceProvider _services;
    private IHubContext<THub, T> _hubContext;
    private IUserIdentity _user;

    public SignalRSink(IServiceProvider services)
    {
        _services = services;
    }

    public void Emit(LogEvent logEvent)
    {
        if (_hubContext == null)
        {
            _hubContext = _services.CreateScope().ServiceProvider.GetRequiredService<IHubContext<THub, T>>();
        }

        if (_user == null)
        {
            _user = _services.CreateScope().ServiceProvider.GetRequiredService<IUserIdentity>();
        }

        // Sending message
        Console.WriteLine($"Sending log... to user {_user?.Id}, {logEvent.MessageTemplate?.Text}");
        _hubContext.Clients.All.SendLogAsObject(logEvent);
    }
}
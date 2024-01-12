using Microsoft.AspNetCore.SignalR;
using Serilog.Configuration;
using Serilog.Events;
using Serilog;

namespace BotSharp.Plugin.ChatHub.Sinks
{
    public static class SignalRSinkExtensions
    {
        public static LoggerConfiguration SignalRSink<THub, T>(
            this LoggerSinkConfiguration loggerConfiguration,
            LogEventLevel logEventLevel,
            IServiceProvider serviceProvider
        ) where THub : Hub<T> where T : class, IHub
        {
            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(loggerConfiguration));
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));

            }
            return loggerConfiguration.Sink(new SignalRSink<THub, T>(serviceProvider), logEventLevel);
        }
    }
}

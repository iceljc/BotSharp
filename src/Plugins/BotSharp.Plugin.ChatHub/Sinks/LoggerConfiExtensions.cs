using Microsoft.AspNetCore.SignalR;
using Serilog.Configuration;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Plugin.ChatHub.Sinks
{
    public static class LoggerConfiExtensions
    {
        public static LoggerConfiguration SignalR(
            this LoggerSinkConfiguration loggerConfiguration,
            LogProducer producer,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));

            return loggerConfiguration.Sink(new SignalRSink(producer), restrictedToMinimumLevel);
        }
    }
}

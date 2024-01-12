using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Plugin.ChatHub.Sinks
{
    public class BotSharpLogEvent
    {

        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public BotSharpLogEvent()
        {
        }

        /// <summary>
        /// Construct a new <see cref="LogEvent"/>.
        /// </summary>
        public BotSharpLogEvent(LogEvent logEvent, string renderedMessage)
        {
            Timestamp = logEvent.Timestamp;
            Exception = logEvent.Exception;
            MessageTemplate = logEvent.MessageTemplate.Text;
            Level = logEvent.Level;
            RenderedMessage = renderedMessage;
            Properties = new Dictionary<string, object>();
            foreach (var pair in logEvent.Properties)
            {
                Properties.Add(pair.Key, SignalRPropertyFormatter.Simplify(pair.Value));
            }
        }

        /// <summary>
        /// The time at which the event occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The template that was used for the log message.
        /// </summary>
        public string MessageTemplate { get; set; }

        /// <summary>
        /// The level of the log.
        /// </summary>
        public LogEventLevel Level { get; set; }

        /// <summary>
        /// A string representation of the exception that was attached to the log (if any).
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The rendered log message.
        /// </summary>
        public string RenderedMessage { get; set; }

        /// <summary>
        /// Properties associated with the event, including those presented in <see cref="Events.LogEvent.MessageTemplate"/>.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}

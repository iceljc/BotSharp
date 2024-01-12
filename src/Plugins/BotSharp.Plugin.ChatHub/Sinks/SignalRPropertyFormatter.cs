using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Plugin.ChatHub.Sinks
{
    public static class SignalRPropertyFormatter
    {
        static readonly HashSet<Type> SignalRSpecialScalars = new HashSet<Type>
        {
            typeof(bool),
            typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
            typeof(byte[])
        };

        public static object Simplify(LogEventPropertyValue value)
        {
            var scalar = value as ScalarValue;
            if (scalar != null)
                return SimplifyScalar(scalar.Value);

            var dict = value as DictionaryValue;
            if (dict != null)
                return dict
                    .Elements
                    .ToDictionary(kv => SimplifyScalar(kv.Key), kv => Simplify(kv.Value));

            var seq = value as SequenceValue;
            if (seq != null)
                return seq.Elements.Select(Simplify).ToArray();

            var str = value as StructureValue;
            if (str != null)
            {
                var props = str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));
                if (str.TypeTag != null)
                    props["$typeTag"] = str.TypeTag;
                return props;
            }

            return null;
        }

        static object SimplifyScalar(object value)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();
            if (SignalRSpecialScalars.Contains(valueType))
                return value;

            return value.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Plugin.ChatHub
{
    public interface IHub
    {
        Task SendLogAsObject(object messageObject);
    }

    public interface ISignalRHub : IHub
    {

    }
}

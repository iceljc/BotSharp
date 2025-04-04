using ModelContextProtocol.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Dynamic;
using System.Text.Json;

namespace BotSharp.Weather.MCPServer.Tools;

[McpServerToolType]
public static class GetWeather
{
    [McpServerTool(name: "get_weather"), Description("call this function to get weather information.")]
    public static string Get_Weather(
       [Description("The location where user requests"), Required] string location)
    {
        if (location is null)
        {
            throw new McpServerException("Missing required argument 'location'");
        }

        dynamic message = new ExpandoObject();
        message.weather = "It is a sunny day";
        var jso = new JsonSerializerOptions() { WriteIndented = true };
        var jsonMessage = JsonSerializer.Serialize(message, jso);

        return jsonMessage;
    }
}
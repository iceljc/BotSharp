using BotSharp.Abstraction.Browsing;
using BotSharp.Abstraction.Browsing.Models;
using BotSharp.Abstraction.Google.Models;
using BotSharp.Abstraction.Google.Settings;
using BotSharp.Abstraction.Options;

namespace BotSharp.OpenAPI.Controllers;

[Authorize]
[ApiController]
public class GoogleController : ControllerBase
{
    private readonly IServiceProvider _services;
    private readonly BotSharpOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public GoogleController(IServiceProvider services,
        IHttpClientFactory httpClientFactory,
        BotSharpOptions options)
    {
        _services = services;
        _options = options;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("/address/options")]
    public async Task<GoogleAddressResult> GetAddressOptions([FromQuery] string address, [FromQuery] string language = "en")
    {
        var result = new GoogleAddressResult();

        try
        {
            var settings = _services.GetRequiredService<GoogleApiSettings>();
            using var client = _httpClientFactory.CreateClient();
            var url = $"{settings.Map.Endpoint}?key={settings.ApiKey}&" +
                $"components={settings.Map.Components}&" +
                $"language={language}&" +
                $"address={address}";

            var response = await client.GetAsync(url);
            var responseStr = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<GoogleAddressResult>(responseStr, _options.JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error when calling google geocoding api... ${ex.Message}");
        }

        return result;
    }

    [HttpGet("/video/options")]
    public async Task<GoogleVideoResult> GetVideoOptions([FromQuery] string query, [FromQuery] int? maxResults, [FromQuery] string language = "en")
    {
        var result = new GoogleVideoResult();

        try
        {
            var settings = _services.GetRequiredService<GoogleApiSettings>();
            using var client = _httpClientFactory.CreateClient();
            var url = $"{settings.Youtube.Endpoint}?key={settings.ApiKey}&" +
                $"part={settings.Youtube.Part}&" +
                $"relevanceLanguage={language}&" +
                $"regionCode={settings.Youtube.RegionCode}&" +
                $"q={query}";

            if (maxResults.HasValue && maxResults > 0)
            {
                url += $"&maxResults={maxResults}";
            }

            var response = await client.GetAsync(url);
            var responseStr = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<GoogleVideoResult>(responseStr, _options.JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error when calling google youtube search api... ${ex.Message}");
        }

        return result;
    }

    [HttpPost("/web/test")]
    public async Task WebDriverTest()
    {
        var web = _services.GetRequiredService<IWebBrowser>();
        var id = Guid.NewGuid().ToString();
        var messageInfo = new MessageInfo()
        {
            ConversationId = id
        };
        var location = new ElementLocatingArgs()
        {
            AttributeName = "class",
            AttributeValue = "result_link",
            Index = 0
        };
        
        var res = await web.LaunchBrowser(id, "https://www.wikihow.com/wikiHowTo?search=how+to+fix+toilet+clog");
        var locateResult = await web.LocateElement(messageInfo, location);
        location = new ElementLocatingArgs()
        {
            AttributeName = "href"
        };
        var link = await web.GetAttributeValue(messageInfo, location, locateResult);
        location = new ElementLocatingArgs()
        {
            Selector = ".result_link .result .result_data .result_title",
            Index = 0
        };
        locateResult = await web.LocateElement(messageInfo, location);
        return;
    }
}

namespace BotSharp.Abstraction.Switch.Models;

public class SwitchResultModel
{
    [JsonPropertyName("template_name")]
    public string? SelectedTemplateName { get; set; }
}

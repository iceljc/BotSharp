namespace BotSharp.Core.Switch;

public class SwitchContext : ISwitchContext
{
    private readonly IServiceProvider _services;
    private readonly ILogger<SwitchContext> _logger;

    public SwitchContext(
        IServiceProvider services,
        ILogger<SwitchContext> logger)
    {
        _services = services;
        _logger = logger;
    }

    public async Task SwitchAsync(Agent agent, List<RoleDialogModel> dialogs, Func<Task> func)
    {
        if (string.IsNullOrWhiteSpace(agent.SwitchInstruction)
            || dialogs.IsNullOrEmpty())
        {
            await func();
            return;
        }

        var chatCompletion = CompletionProvider.GetChatCompletion(_services,
            provider: agent.LlmConfig?.Provider,
            model: agent.LlmConfig?.Model);

        var response = await chatCompletion.GetChatCompletions(new Agent
        {
            Id = agent.Id,
            Name = agent.Name,
            Instruction = agent.SwitchInstruction
        }, dialogs);

        var result = response?.Content?.JsonContent<SwitchResultModel>() ?? null;
        var found = agent.Templates.FirstOrDefault(x => x.Name.IsEqualTo(result?.SelectedTemplateName));
        if (string.IsNullOrWhiteSpace(found?.Content))
        {
            await func();
            return;
        }

        agent.StandbyInstruction = agent.Instruction;
        agent.Instruction = found.Content;

        await func();

        agent.Instruction = agent.StandbyInstruction;
        agent.StandbyInstruction = null;
        return;
    }
}

namespace BotSharp.Abstraction.Switch;

public interface ISwitchContext
{
    Task SwitchAsync(Agent agent, List<RoleDialogModel> dialogs, Func<Task> func);
}

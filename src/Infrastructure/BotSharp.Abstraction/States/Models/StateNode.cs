namespace BotSharp.Abstraction.States.Models;

public class StateNode
{
    public string Name { get; set; }
    public string Scope { get; set; }
    public StateContent Content { get; set; }
    public List<StateNode> Children { get; set; } = new List<StateNode>();

    public override string ToString()
    {
        var descendants = Children?.Select(x => $"<{x.Name}, {x.Scope}>")?.ToList() ?? new List<string>() { "None" };
        return $"<Name: {Name}, Scope: {Scope}> => (Children: {string.Join("; ", descendants)})";
    }
}
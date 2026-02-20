using System.Threading;

namespace BotSharp.Abstraction.Knowledges.Pipelines;

public interface IKnowledgePipelineStep
{
    /// <summary>
    /// Knowledge pipeline step name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Execute knowledge pipeline step
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> ExecuteAsync(KnwoledgePipelineContext context, CancellationToken? cancellation = null)
        => Task.FromResult(false);
}

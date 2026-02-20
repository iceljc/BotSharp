using BotSharp.Abstraction.Knowledges.Pipelines;
using System.Threading;

namespace BotSharp.Plugin.KnowledgeBase.Pipelines;

public sealed class KnowledgePipeline
{
    private readonly List<IKnowledgePipelineStep> _steps = new();

    private KnowledgePipeline()
    {
        
    }

    public static KnowledgePipeline Init()
    {
        return new();
    }

    public static KnowledgePipeline Init(IEnumerable<IKnowledgePipelineStep> steps)
    {
        var pipleline = Init();
        foreach (var step in steps)
        {
            pipleline = pipleline.AddStep(step);
        }
        return pipleline;
    }

    public KnowledgePipeline AddStep(IKnowledgePipelineStep step)
    {
        _steps.Add(step);
        return this;
    }

    public async Task<KnwoledgePipelineContext> RunAsync(KnwoledgePipelineContext context, CancellationToken? cancellation = null)
    {
        foreach (var step in _steps)
        {
            cancellation?.ThrowIfCancellationRequested();
            await step.ExecuteAsync(context, cancellation);
        }
        return context;
    }
}

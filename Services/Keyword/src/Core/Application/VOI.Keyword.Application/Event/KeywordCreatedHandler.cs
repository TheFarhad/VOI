namespace VOI.Keyword.Application.Event;

using Domain.Aggregates.Keyword.Event;
using Framework.Core.Application.Event;

public sealed class KeywordCreatedHandler
    : IEventHandler<KeywordCreated>
{
    public async Task HandleAsync(KeywordCreated request, CancellationToken token = default!)
    {
        await Task.CompletedTask;
    }
}

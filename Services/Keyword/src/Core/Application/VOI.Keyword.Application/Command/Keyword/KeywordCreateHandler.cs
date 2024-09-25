namespace VOI.Keyword.Application.Command.Keyword;

using Domain.Aggregates.Keyword.Entity;
using Framework.Core.Domain.Shared.Prop;
using Framework.Core.Application.Shared;
using Contract.Application.Command.Keyword;

public sealed class KeywordCreateHandler(IKeywordCommandRepository keywordRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<KeywordCreate, KeywordCreatePayload>
{
    private readonly IKeywordCommandRepository _keywordRepository = keywordRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<KeywordCreatePayload>> HandleAsync(KeywordCreate command, CancellationToken token = default)
    {
        Response<KeywordCreatePayload> result = default!;
        try
        {
            var model = new Keyword(command.Title, command.Description);
            await _keywordRepository.AddAsync(model, token);
            await _uow.SaveAsync(token);
            result = new KeywordCreatePayload(model.Id.Value);
        }
        catch (Exception e)
        {
            result = Error.BadRequest(e.Message);
        }
        return result;
    }
}

public sealed class KeywordChangeTitleAndDescriptionHandler(IKeywordCommandRepository keywordRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<KeywordChangeTitle, KeywordChangeTitlePayload>
{
    private readonly IKeywordCommandRepository _keywordRepository = keywordRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<KeywordChangeTitlePayload>> HandleAsync(KeywordChangeTitle command, CancellationToken token = default)
    {
        Response<KeywordChangeTitlePayload> result = default!;

        var code = Code.New(command.Code);
        var model = await _keywordRepository
                            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (model is { })
        {
            model.ChangeTitleAndDescription(command.Title);
            await _uow.SaveAsync();
            result = new KeywordChangeTitlePayload(model.Id.Value);
        }
        else
        {
            result = Error.NotFound("");
        }
        return result;
    }
}

public sealed class KeywordRemoveHandler(IKeywordCommandRepository keywordRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<KeywordDelete, KeywordDeletePayload>
{
    private readonly IKeywordCommandRepository _keywordRepository = keywordRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<KeywordDeletePayload>> HandleAsync(KeywordDelete command, CancellationToken token = default)
    {
        Response<KeywordDeletePayload> result = default!;

        var code = Code.New(command.Code);
        var model = await _keywordRepository
                            .FirstOrDefaultAsync(_ => _.Code == code, token);
        if (model is { })
        {
            _keywordRepository.Remove(model);
            await _uow.SaveAsync(token);
            result = new KeywordDeletePayload(model.Id.Value);
        }
        else
        {
            result = Error.NotFound("", "");
        }
        return result;
    }
}

public sealed class KeywordChangeStatusHandler(IKeywordCommandRepository keywordRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<KeywordChangeStatus, KeywordChangeStatusPayload>
{
    private readonly IKeywordCommandRepository _keywordRepository = keywordRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<KeywordChangeStatusPayload>> HandleAsync(KeywordChangeStatus command, CancellationToken token = default)
    {
        Response<KeywordChangeStatusPayload> result = default!;

        var code = Code.New(command.Code);
        var model = await _keywordRepository
            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (model is { })
        {
            model.ChangeStatus(command.Status);
            await _uow.SaveAsync(token);
            result = new KeywordChangeStatusPayload(model.Id.Value);
        }
        else
        {
            result = Error.NotFound("", "");
        }
        return result;
    }
}

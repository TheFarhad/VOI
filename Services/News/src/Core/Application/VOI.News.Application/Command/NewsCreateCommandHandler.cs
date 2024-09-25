namespace VOI.News.Application.Command;

using Domian.News.Entity;
using Contract.Application.Command;
using Framework.Core.Domain.Shared.Prop;
using Contract.Infrastructure.Persistence.Command;
using Framework.Core.Application.Shared;

public sealed class NewsCreateCommandHandler(INewsCommandRepository newsRepository, INewsServiceUnitOfWork uow)
    : IRequestHandler<NewsCreate, NewsCreatePayload>
{
    private readonly INewsCommandRepository _newsRepository = newsRepository;
    private readonly INewsServiceUnitOfWork _uow = uow;

    public async Task<Response<NewsCreatePayload>> HandleAsync(NewsCreate command, CancellationToken token = default!)
    {
        Response<NewsCreatePayload> result = default!;
        try
        {
            List<Keyword> keywords = [];
            command.Keywords.ForEach(_ =>
            {
                keywords.Add(_);
            });
            var news = News.New(command.Title, command.Description, command.Body, keywords);

            await _newsRepository.AddAsync(news, token);
            await _uow.SaveAsync(token);
            result = new NewsCreatePayload(news.Id.Value);
        }
        catch (DomainException e)
        {
            result = Error.BadRequest(e.Message);
        }
        return result;
    }
}

public sealed class NewsEditCommandHandler(INewsCommandRepository newsRepository, INewsServiceUnitOfWork uow)
    : IRequestHandler<NewsEdit, NewsEditPayload>
{
    private readonly INewsCommandRepository _newsRepository = newsRepository;
    private readonly INewsServiceUnitOfWork _uow = uow;

    public async Task<Response<NewsEditPayload>> HandleAsync(NewsEdit command, CancellationToken token = default!)
    {
        Response<NewsEditPayload> result = default!;

        var code = Code.New(command.Code);
        var news = await _newsRepository
            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (news is { })
        {
            List<Keyword> keywords = [];
            command.Keywords.ForEach(_ =>
            {
                keywords.Add(_);
            });
            news.EditNews(command.Title, command.Description, command.Body, keywords);

            await _uow.SaveAsync(token);
            result = new NewsEditPayload(news.Id.Value);
        }
        else
        {
            result = Error.NotFound("");
        }
        return result;
    }
}

public sealed class NewsRemoveCommandHandler(INewsCommandRepository newsRepository, INewsServiceUnitOfWork uow)
    : IRequestHandler<NewsDelete, NewsDeletePayload>
{
    private readonly INewsCommandRepository _newsRepository = newsRepository;
    private readonly INewsServiceUnitOfWork _uow = uow;

    public async Task<Response<NewsDeletePayload>> HandleAsync(NewsDelete command, CancellationToken token = default)
    {
        Response<NewsDeletePayload> result = default!;
        var code = Code.New(command.Code);
        var news = await _newsRepository
            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (news is { })
        {
            _newsRepository.Remove(news);
            await _uow.SaveAsync(token);
            result = new NewsDeletePayload(news.Id.Value);
        }
        else
        {
            result = Error.NotFound("");
        }
        return result;
    }
}

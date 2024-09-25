namespace VOI.Keyword.Application.Command.Category;

using Domain.Aggregates.Category.Entity;
using Framework.Core.Domain.Shared.Prop;
using Framework.Core.Application.Shared;
using Contract.Application.Command.Category;

public sealed class CategoryCreateHandler(ICategoryCommandRepository categoryRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<CategoryCreate, CategoryCreatePayload>
{
    private readonly ICategoryCommandRepository _categoryRepository = categoryRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<CategoryCreatePayload>> HandleAsync(CategoryCreate command, CancellationToken token = default)
    {
        Response<CategoryCreatePayload> result = default!;
        try
        {
            var model = Category.New(command.Title, command.Description);
            await _categoryRepository.AddAsync(model, token);
            await _uow.SaveAsync(token);
            result = new CategoryCreatePayload(model.Id.Value);
        }
        catch (Exception e)
        {
            result = Error.BadRequest("");
        }
        return result;
    }
}

public sealed class CategoryChangeTitleAndDescriptionHandler(ICategoryCommandRepository categoryRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<CategoryChangeTitleAndDescription, CategoryChangeTitleAndDescriptionPayload>
{
    private readonly ICategoryCommandRepository _categoryRepository = categoryRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<CategoryChangeTitleAndDescriptionPayload>> HandleAsync(CategoryChangeTitleAndDescription command, CancellationToken token = default)
    {
        Response<CategoryChangeTitleAndDescriptionPayload> result = default!;

        var code = Code.New(command.Code);
        var model = await _categoryRepository
            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (model is { })
        {
            model.ChangeTitleAndDescription(command.Title, command.Description);
            await _uow.SaveAsync(token);
            return new CategoryChangeTitleAndDescriptionPayload(model.Id.Value);
        }
        else
        {
            result = Error.NotFound("", "");
        }
        return result;
    }
}

public sealed class CategoryRemoveHandler(ICategoryCommandRepository categoryRepository, IKeywordServiceUnitOfWork uow)
    : IRequestHandler<CategoryDelete, CategoryDeletePayload>
{
    private readonly ICategoryCommandRepository _categoryRepository = categoryRepository;
    private readonly IKeywordServiceUnitOfWork _uow = uow;

    public async Task<Response<CategoryDeletePayload>> HandleAsync(CategoryDelete command, CancellationToken token = default)
    {
        Response<CategoryDeletePayload> result = default!;

        var code = Code.New(command.Code);
        var model = await _categoryRepository
            .FirstOrDefaultAsync(_ => _.Code == code, token);

        if (model is { })
        {
            _categoryRepository.Remove(model);
            await _uow.SaveAsync(token);
            result = new CategoryDeletePayload(model.Id.Value);
        }
        else
        {
            result = Error.NotFound("", "");
        }
        return result;
    }
}

namespace VOI.Keyword.Application.Query.Category;

using Contract.Application.DTO;
using Contract.Application.Query;
using Framework.Core.Application.Shared;
using Contract.Infrastructure.Persistence.Query;

public sealed class CategorySearchHandler(ICategoryQueryRepository categoryRepository)
    : IRequestHandler<CategorySearchByTitle, CategorySearchByTitlePayload>
{
    private readonly ICategoryQueryRepository _categoryRepository = categoryRepository;

    public async Task<Response<CategorySearchByTitlePayload>> HandleAsync(CategorySearchByTitle query, CancellationToken token = default)
    {
        var payload = await _categoryRepository.ListAsync(query, token);
        return payload?.Items?.Count > 0 ? payload : Error.NotFound("", "");
    }
}

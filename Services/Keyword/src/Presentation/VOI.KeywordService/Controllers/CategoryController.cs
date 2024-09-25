namespace VOI.KeywordService.Controllers;

using Keyword.Contract.Application.Command.Category;

public sealed class CategoryController : RestAPI
{
    [HttpPost("add-category")]
    public async Task<IResult> AddAsync([FromBody] CategoryCreate command, CancellationToken token = default!)
        => await PostAsync<CategoryCreate, CategoryCreatePayload>(command, token);

    [HttpPut("change-title")]
    public async Task<IResult> ChangeTitleAndDescriptionAsync([FromBody] CategoryChangeTitleAndDescription command, CancellationToken token = default!)
        => await PutAsync<CategoryChangeTitleAndDescription, CategoryChangeTitleAndDescriptionPayload>(command, token);

    [HttpGet]
    public async Task<IResult> GetAsync([FromBody] CategorySearchByTitle query, CancellationToken token = default!)
        => await GetAsync<CategorySearchByTitle, CategorySearchByTitlePayload>(query, token);

    [HttpDelete("delete-category")]
    public async Task<IResult> DeleteAsync([FromBody] CategoryDelete command, CancellationToken token = default!)
        => await DeleteAsync<CategoryDelete, CategoryDeletePayload>(command, token);
}

namespace VOI.NewsService.Controllers;

using News.Contract.Application.Dto;
using News.Contract.Application.Query;
using News.Contract.Application.Command;

public class NewsController : RestAPI
{
    [HttpPost("add-news")]
    public async Task<IResult> AddAsync([FromBody] NewsCreate command, CancellationToken token = default!)
        => await PostAsync<NewsCreate, NewsCreatePayload>(command, token);

    [HttpPut("edit-news")]
    public async Task<IResult> EditAsync([FromBody] NewsEdit command, CancellationToken token = default!)
        => await PutAsync<NewsEdit, NewsEditPayload>(command, token);

    [HttpDelete("delete-news")]
    public async Task<IResult> DeleteAsync([FromBody] NewsDelete command, CancellationToken token = default!)
        => await DeleteAsync<NewsDelete, NewsDeletePayload>(command, token);

    [HttpGet]
    public async Task<IResult> GetAsync([FromBody] NewsSearchByTitle query, CancellationToken token = default!)
        => await GetAsync<NewsSearchByTitle, NewsSearchByTitlePayload>(query, token);
}

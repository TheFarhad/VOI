namespace VOI.KeywordService.Controllers;

using Keyword.Contract.Application.Command.Keyword;

public sealed class KeywordController : RestAPI
{
    [HttpPost("add-keyword")]
    public async Task<IResult> AddAsync([FromBody] KeywordCreate command, CancellationToken token = default!)
        => await PostAsync<KeywordCreate, KeywordCreatePayload>(command, token);

    [HttpPut("change-title")]
    public async Task<IResult> ChangeTitleAsync([FromBody] KeywordChangeTitle command, CancellationToken token = default!)
        => await PutAsync<KeywordChangeTitle, KeywordChangeTitlePayload>(command, token);

    [HttpPut("change-status")]
    public async Task<IResult> ChangeStatusAsync([FromBody] KeywordChangeStatus command, CancellationToken token = default!)
    => await PutAsync<KeywordChangeStatus, KeywordChangeStatusPayload>(command, token);

    [HttpGet]
    public async Task<IResult> GetAsync([FromBody] KeywordSearchByTitleAndStatus query, CancellationToken token = default!)
        => await GetAsync<KeywordSearchByTitleAndStatus, KeywordSearchByTitleAndStatusPayload>(query, token);

    [HttpDelete("delete-keyword")]
    public async Task<IResult> DeleteAsync([FromBody] KeywordDelete command, CancellationToken token = default!)
        => await DeleteAsync<KeywordDelete, KeywordDeletePayload>(command, token);
}
namespace VOI.DescktopClient.Endpoints.Keyword;

public sealed class KeywordEndpoints(KeywordClient client) : CarterModule
{
    private readonly KeywordClient _client = client;

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var map = app.MapGroup("/Keyword/");

        map.MapPost("add", async
             ([FromBody] KeywordCreate command) =>
        {
            var response = await _client.PostAsync(command);
            return Results.Json(response);
        })
        .WithName(KeywordAddAction);

        map.MapPut("change-status", async
           ([FromBody] KeywordChangeStatus command) =>
        {
            var response = await _client.PutStatusAsync(command);
            return Results.Json(response);
        })
       .WithName(KeywordChangeStatusAction);

        map.MapPut("change-title", async
            ([FromBody] KeywordChangeTitle command) =>
        {
            var response = await _client.PutTitleAsync(command);
            return Results.Json(response);
        })
        .WithName(KeywordChangeTitleAction);

        map.MapDelete("delete", async
            ([FromBody] KeywordDelete command) =>
        {
            var response = await _client.DeleteAsync(command);
            return Results.Json(response);
        })
        .WithName(KeywordDeleteAction);

        map.MapGet("list", async
           ([FromBody] KeywordSearchByTitleAndStatus command) =>
        {
            var response = await _client.GetAsync(command);
            return Results.Json(response);
        })
       .WithName(KeywordListAction);
    }

    public const string KeywordAddAction = "KeywordAdd";
    public const string KeywordChangeTitleAction = "KeywordChangeTitle";
    public const string KeywordChangeStatusAction = "KeywordChangeStatus";
    public const string KeywordDeleteAction = "KeywordDelete";
    public const string KeywordListAction = "KeywordList";
}

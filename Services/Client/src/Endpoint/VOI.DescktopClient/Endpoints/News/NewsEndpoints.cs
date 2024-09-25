namespace VOI.DescktopClient.Endpoints.News;

public sealed class NewsEndpoints(NewsClient client) : CarterModule
{
    private readonly NewsClient _client = client;

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var map = app.MapGroup("/News/");

        map.MapPost("add", async
             ([FromBody] NewsCreate command) =>
        {
            var response = await _client.PostAsync(command);
            return Results.Json(response);
        })
        .WithName(NewsAddAction);

        map.MapPut("edit", async
           ([FromBody] NewsEdit command) =>
        {
            var response = await _client.PutAsync(command);
            return Results.Json(response);
        })
       .WithName(NewsEditAction);

        map.MapDelete("delete", async
            ([FromBody] NewsDelete command) =>
        {
            var response = await _client.DeleteAsync(command);
            return Results.Json(response);
        })
        .WithName(NewsDeleteAction);

        map.MapGet("list", async
           ([FromBody] NewsSearchByTitle command) =>
        {
            var response = await _client.GetAsync(command);
            return Results.Json(response);
        })
       .WithName(NewsListAction);
    }

    public const string NewsAddAction = "NewsAdd";
    public const string NewsEditAction = "NewsEdit";
    public const string NewsDeleteAction = "NewsDelete";
    public const string NewsListAction = "NewsList";
}

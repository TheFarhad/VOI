namespace VOI.DescktopClient.ClientServices;

public sealed class NewsClient
{
    private readonly HttpClient _client;
    private readonly ISerializeService _serializer;

    public NewsClient(HttpClient httpClient, ISerializeService serializeService)
    {
        _client = httpClient;
        _client.BaseAddress = new Uri("https://localhost:7100/ns/api/News/");
        _serializer = serializeService;
    }

    public async Task<NewsCreatePayload> PostAsync(NewsCreate command)
    {
        NewsCreatePayload result = default!;
        var response = await _client.PostAsJsonAsync("add-news", command);
        if (response.IsSuccessStatusCode)
        {
            var data = await response
                .Content
                .ReadFromJsonAsync<Response<NewsCreatePayload>>();

            result = data?.Payload!;
        }
        return result;
    }

    public async Task<NewsEditPayload> PutAsync(NewsEdit command)
    {
        NewsEditPayload? result = default!;
        var response = await _client.PutAsJsonAsync("edit-news", command);
        if (response.IsSuccessStatusCode)
        {
            var data = await response
                                .Content
                                .ReadFromJsonAsync<Response<NewsEditPayload>>();
            result = data?.Payload!;
        }
        return result;
    }

    public async Task<NewsDeletePayload> DeleteAsync(NewsDelete command)
    {
        var uri = $"{_client.BaseAddress}delete-news";
        var response = await _client
            .SendAsJsonAsync<NewsDelete, Response<NewsDeletePayload>>
            (uri, HttpMethod.Delete, command, _serializer);

        return response?.Payload!;
    }

    public async Task<NewsSearchByTitlePayload?> GetAsync(NewsSearchByTitle query)
    {
        var uri = $"{_client.BaseAddress}";
        var response = await _client
            .SendAsJsonAsync<NewsSearchByTitle, Response<NewsSearchByTitlePayload>>
            (uri, HttpMethod.Get, query, _serializer);

        return response?.Payload;
    }
}

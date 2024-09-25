namespace VOI.DescktopClient.ClientServices;

public sealed class KeywordClient
{
    private readonly HttpClient _client;
    private readonly ISerializeService _serializer;

    public KeywordClient(HttpClient client, ISerializeService serializeService)
    {
        _client = client;
        _client.BaseAddress = new("https://localhost:7100/ks/api/Keyword/");
        _serializer = serializeService;
    }

    public async Task<KeywordCreatePayload?> PostAsync(KeywordCreate command)
    {
        KeywordCreatePayload result = default!;
        var response = await _client.PostAsJsonAsync("add-keyword", command);
        if (response.IsSuccessStatusCode)
        {
            var data = await response
                .Content
                .ReadFromJsonAsync<Response<KeywordCreatePayload>>();

            result = data?.Payload!;
        }
        return result;
    }

    public async Task<KeywordChangeTitlePayload?> PutTitleAsync(KeywordChangeTitle command)
    {
        KeywordChangeTitlePayload? result = default!;
        var response = await _client.PutAsJsonAsync("change-title", command);
        if (response.IsSuccessStatusCode)
        {
            var data = await response
                                .Content
                                .ReadFromJsonAsync<Response<KeywordChangeTitlePayload>>();
            result = data?.Payload!;
        }
        return result;
    }

    public async Task<KeywordChangeStatusPayload?> PutStatusAsync(KeywordChangeStatus command)
    {
        KeywordChangeStatusPayload? result = default!;
        var response = await _client.PutAsJsonAsync("change-status", command);
        if (response.IsSuccessStatusCode)
        {
            var data = await response
                                .Content
                                .ReadFromJsonAsync<Response<KeywordChangeStatusPayload>>();
            result = data?.Payload!;
        }
        return result;
    }

    public async Task<KeywordDeletePayload?> DeleteAsync(KeywordDelete command)
    {
        var uri = $"{_client.BaseAddress}delete-keyword";
        var response = await _client
            .SendAsJsonAsync<KeywordDelete, Response<KeywordDeletePayload>>
            (uri, HttpMethod.Delete, command, _serializer);

        return response?.Payload!;
    }

    public async Task<KeywordSearchByTitleAndStatusPayload?> GetAsync(KeywordSearchByTitleAndStatus query)
    {
        var uri = $"{_client.BaseAddress}";
        var response = await _client
            .SendAsJsonAsync<KeywordSearchByTitleAndStatus, Response<KeywordSearchByTitleAndStatusPayload>>
            (uri, HttpMethod.Get, query, _serializer);

        return response?.Payload;
    }
}
namespace VOI.News.ExternalService.RabbitMQ.Data.Event;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Framework.Core.Application.Event;

public sealed class KeywordCreatedHandler(IConfiguration configuration)
    : IEventHandler<KeywordCreated>
{
    private readonly string _connectionString = configuration.GetConnectionString("NewsServiceDb")!;

    public async Task HandleAsync(KeywordCreated source, CancellationToken token)
    {
        var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();

        var commandText = @$"INSERT INTO 
            [NewsServiceDb].[Voi_News].[KeywordDetails] 
            ([Code], [Title]) 
            VALUES (@code, @title);";

        var command = new SqlCommand(commandText, sqlConnection);
        command
            .Parameters
            .AddRange(
            [
            new SqlParameter("@code", source.Code),
            new SqlParameter("@title", source.Title)
            ]);
        await command.ExecuteNonQueryAsync(token);

        await sqlConnection.CloseAsync();
    }
}

public sealed class KeywordEditedHandler(IConfiguration configuration)
    : IEventHandler<KeywordEdited>
{
    private readonly string _connectionString = configuration.GetConnectionString("NewsServiceDb")!;

    public async Task HandleAsync(KeywordEdited source, CancellationToken token)
    {
        var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();

        var commandText = @$"UPDATE 
            [NewsServiceDb].[Voi_News].[KeywordDetails] 
            SET [Title] = @title 
            WHERE [Code] = @code;";

        var command = new SqlCommand(commandText, sqlConnection);
        command
           .Parameters
           .AddRange(
           [
           new SqlParameter("@code", source.Code),
            new SqlParameter("@title", source.Title)
           ]);
        await command.ExecuteNonQueryAsync(token);

        await sqlConnection.CloseAsync();
    }
}

public sealed class KeywordRemovedHandler(IConfiguration configuration)
    : IEventHandler<KeywordRemoved>
{
    private readonly string _connectionString = configuration.GetConnectionString("NewsServiceDb")!;

    public async Task HandleAsync(KeywordRemoved source, CancellationToken token)
    {
        var sqlConnection = new SqlConnection(_connectionString);
        await sqlConnection.OpenAsync();

        var commandText = @$"DELETE FROM 
            [NewsServiceDb].[Voi_News].[KeywordDetails] 
            WHERE [Code] = @code;";

        var command = new SqlCommand(commandText, sqlConnection);
        command.Parameters.Add(new("@code", source.Code));
        await command.ExecuteNonQueryAsync(token);

        await sqlConnection.CloseAsync();
    }
}

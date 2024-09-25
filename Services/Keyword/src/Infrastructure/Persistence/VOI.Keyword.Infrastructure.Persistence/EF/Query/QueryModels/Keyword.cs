namespace VOI.Keyword.Infrastructure.Persistence.EF.Query.QueryModels;

using System;
using Framework.Infra.Persistence.Query;

internal sealed record Keyword(int Id, Guid Code, string Title, string Description, string Status)
    : IQueryModel;
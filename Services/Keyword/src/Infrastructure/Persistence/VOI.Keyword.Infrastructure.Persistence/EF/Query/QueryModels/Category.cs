namespace VOI.Keyword.Infrastructure.Persistence.EF.Query.QueryModels;

using System;
using Framework.Infra.Persistence.Query;

internal sealed record Category(int Id, Guid Code, string Title, string Description)
    : IQueryModel;

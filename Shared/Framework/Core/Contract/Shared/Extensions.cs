namespace Framework.Core.Contract.Shared;

using Microsoft.AspNetCore.Http;
using Tool.Shared;
using Application.Shared;
using Domain.Aggregate.Event;
using Core.Application.Shared.Pipeline;

public static partial class Extensions
{
    public static Type Type(this IEvent source)
        => GetType(source);

    public static Type Type<TOutput>(this IRequest<TOutput> source)
        => GetType(source);

    private static Type GetType(this object source)
        => source.GetType();

    public static RequestController RequestPipeline(this HttpContext source) => source.Service<RequestController>();
}

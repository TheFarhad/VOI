namespace Tool.Serialize;

using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection SerializationDependencies(this IServiceCollection source, SerializeFlag flag = SerializeFlag.NewtonSoft)
        => flag switch
        {
            SerializeFlag.NetJson
            => source.AddSingleton<ISerializeService, NetJsonSerializeService>(),
            SerializeFlag.NewtonSoft
            => source.AddSingleton<ISerializeService, NewtonSoftSerializeService>(),
            SerializeFlag.Json
            => source.AddSingleton<ISerializeService, JsonSerializeService>(),
            _ =>
            throw new NotImplementedException()
        };
}

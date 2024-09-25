namespace Tool.Encrypt;

using Microsoft.Extensions.DependencyInjection;

public static class Dependencies
{
    public static IServiceCollection EncryptionDependencies(this IServiceCollection source, EncryptFlag flag = EncryptFlag.Rfc)
         => flag switch
         {
             EncryptFlag.Rfc =>
             source.AddSingleton<IEncryptService, RfcEncryptionService>(),
             EncryptFlag.Bcrypt
             => source.AddSingleton<IEncryptService, BCryptEncryptionService>(),
             EncryptFlag.Script
             => source.AddSingleton<IEncryptService, ScryptEncryptionService>(),
             _ => throw new NotImplementedException()
         };
}

namespace Framework.Endpoint.HostedServices;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Framework.Shared;
using Infra.Persistence.ServiceRegistration;

public sealed class RegistryHostedService(RedisServiceRegistry serviceRegistery, IServer server)
    : ILifespanHostedService
{
    private readonly RedisServiceRegistry _registery = serviceRegistery;
    private readonly IServer _server = server;

    public async Task StartedAsync(CancellationToken cancellationToken)
        => await _registery.Register(URL()!);

    public async Task StoppedAsync(CancellationToken cancellationToken)
        => await _registery.Deregister(URL()!);

    private string? URL()
        => _server?
               .Features?
               .Get<IServerAddressesFeature>()?
               .Addresses
               .FirstOrDefault();
}
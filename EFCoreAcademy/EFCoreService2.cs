using Microsoft.Extensions.Hosting;

namespace EFCoreAcademy;

public class EFCoreService2 : IHostedService
{
    private readonly serviceToInject serviceToInject;

    public EFCoreService2(serviceToInject serviceToInject)
    {
        this.serviceToInject = serviceToInject;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("second hosted service started !");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("second hosted service stopped !");
        return Task.CompletedTask;
    }
}
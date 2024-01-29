using Microsoft.Extensions.Hosting;

namespace EFCoreAcademy;

public class EFCoreService2 : IHostedService
{
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
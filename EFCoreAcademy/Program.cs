using System.Text.Json;
using EFCoreAcademy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// See https://aka.ms/new-console-template for more information

var configBuilder = new ConfigurationBuilder();
configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = configBuilder.Build();
var connectionString = config["ConnectionStrings:EFCoreAcademy"];

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
hostBuilder.Build();

if (connectionString != null)
{
    var efCoreTest = new EfCoreTest(connectionString);
    await efCoreTest.Start();
}

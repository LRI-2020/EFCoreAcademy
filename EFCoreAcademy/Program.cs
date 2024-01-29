using System.Text.Json;
using EFCoreAcademy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// See https://aka.ms/new-console-template for more information

var configBuilder = new ConfigurationBuilder();
configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = configBuilder.Build();
var connectionString = config["ConnectionStrings:EFCoreAcademy"];

HostApplicationBuilder hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Logging.SetMinimumLevel(LogLevel.Warning);
hostBuilder.Services.AddScoped<serviceToInject>();
hostBuilder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
hostBuilder.Services.AddHostedService<EfCoreTest>();
hostBuilder.Services.AddHostedService<EFCoreService2>();
var host = hostBuilder.Build();
await host.RunAsync();

// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.Adapter.NetCoreServer;
using Pomelo.Contacts;
using SimpleCommandServer;

var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<ISocketService, MyCommandSocketService>();
    })
    .ConfigureLogging(factory =>
    {
        factory.AddConsole();
    })
    .UseNetCoreServer();

builder.RunConsoleAsync().Wait();
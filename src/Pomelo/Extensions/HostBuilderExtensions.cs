using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Pomelo.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UsePort(this IHostBuilder builder, int port)
        {
            return builder.ConfigureServices(services => { services.Configure<ServerOptions>(o => o.Port = port); });
        }

        public static IHostBuilder Configure(this IHostBuilder builder, Action<ServerOptions> configOptions)
        {
            return builder.ConfigureServices(services => { services.Configure<ServerOptions>(configOptions); });
        }

        public static IHostBuilder UsePomeloServices(this IHostBuilder builder)
        {
            return builder.ConfigureServices(services =>
            {
                services.AddPomeloServices();
            });
        }
    }
}

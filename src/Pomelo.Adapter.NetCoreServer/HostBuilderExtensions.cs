using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.Extensions;

namespace Pomelo.Adapter.NetCoreServer
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseNetCoreServer(this IHostBuilder builder, Action<ServerOptions>? configOptions = default)
        {
            return builder.UsePomeloServices().ConfigureServices(services =>
            {
                services.UseNetCoreServerAdapter();
                services.Configure<ServerOptions>(options =>
                {
                    configOptions?.Invoke(options);
                });
            });
        }
    }
}

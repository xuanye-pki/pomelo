using Pomelo.Adapter.NetCoreServer;
using Pomelo.Contacts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseNetCoreServerAdapter(this IServiceCollection services)
        {
            services.AddSingleton<ISocketServer, NetServerAdapter>();
            return services;
        }
    }
}

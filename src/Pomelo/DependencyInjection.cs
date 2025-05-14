using Microsoft.Extensions.Options;
using Pomelo;
using Pomelo.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPomeloServices(this IServiceCollection services)
        {
            services.AddHostedService<PomeloHostService>();

            services.AddSingleton(p =>
            {
                var optionsAccessor = p.GetRequiredService<IOptions<ServerOptions>>();
                var options = optionsAccessor.Value ?? new ServerOptions();
                return options;
            });
            return services;
        }
    }
}

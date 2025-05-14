using Microsoft.Extensions.Hosting;
using Pomelo.Contacts;
using System.Threading;
using System.Threading.Tasks;

namespace Pomelo.Hosting
{
    public class PomeloHostService : IHostedService
    {
        private readonly ISocketServer _socketServer;

        public PomeloHostService(ISocketServer socketServer)
        {
            _socketServer = socketServer;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _socketServer.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _socketServer.StopAsync();
        }
    }
}

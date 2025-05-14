using System;
using System.Threading.Tasks;

namespace Pomelo.Contacts
{
    public interface ISocketServer
    {
        ValueTask StartAsync();
        ValueTask StopAsync();

        ValueTask MulticastAsync(byte[] data);


        event EventHandler<ISocketContext>? SessionConnected;
        event EventHandler<ISocketContext>? SessionDisconnected;
    }
}

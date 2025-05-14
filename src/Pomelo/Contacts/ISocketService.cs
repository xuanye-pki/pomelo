using System;

namespace Pomelo.Contacts
{
    public interface ISocketService
    {
        void OnReceive(ISocketContext context, byte[] data);
        void OnConnected(ISocketContext context);
        void OnDisconnected(ISocketContext context);
        void OnException(ISocketContext context, Exception ex);
    }
}

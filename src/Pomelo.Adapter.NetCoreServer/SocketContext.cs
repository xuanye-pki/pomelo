using NetCoreServer;
using Pomelo.Contacts;
using System.Net;

namespace Pomelo.Adapter.NetCoreServer
{
    internal class SocketContext : ISocketContext
    {
        private readonly TcpSession _tcpSession;

        public SocketContext(TcpSession tcpSession)
        {
            _tcpSession = tcpSession;
        }
        public string Id => _tcpSession.Id.ToString("N");

        public IPEndPoint? LocalEndPoint => _tcpSession.Socket.LocalEndPoint as IPEndPoint;


        public IPEndPoint? RemoteEndPoint => _tcpSession.Socket.RemoteEndPoint as IPEndPoint;


        public bool IsConnected => _tcpSession.IsConnected;

        public ValueTask DisconnectAsync()
        {
            _tcpSession.Disconnect();
            return ValueTask.CompletedTask;
        }

        public ValueTask SendAsync(byte[] data)
        {
            _tcpSession.SendAsync(data);
            return ValueTask.CompletedTask;
        }
    }
}

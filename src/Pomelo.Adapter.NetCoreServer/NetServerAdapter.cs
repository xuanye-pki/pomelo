using Microsoft.Extensions.Logging;
using NetCoreServer;
using Pomelo.Contacts;
using System.Net;

namespace Pomelo.Adapter.NetCoreServer
{
    public class NetServerAdapter : ISocketServer
    {
        private readonly TcpServer _server;
        private readonly ServerOptions _options;
        private readonly ILogger<NetServerAdapter> _logger;

        public NetServerAdapter(ServerOptions options, ISocketService socketService, ILogger<NetServerAdapter> logger)
        {
            _options = options;
            _logger = logger;
            IPAddress = GetIPAddress();
            Port = options.Port;
            _server = new InternalServer(IPAddress, Port, this, socketService);

        }

        public event EventHandler<ISocketContext>? SessionConnected;
        public event EventHandler<ISocketContext>? SessionDisconnected;

        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }

        private IPAddress GetIPAddress()
        {
            IPAddress address;
            switch (_options.BindType)
            {
                case AddressBindType.Any:
                    address = IPAddress.Any;
                    break;
                case AddressBindType.InternalAddress:
                    address = IPUtility.GetLocalIntranetIP() ?? IPAddress.Loopback;
                    break;
                case AddressBindType.SpecialAddress:
                    address = IPAddress.Parse(_options.SpecialAddress);
                    break;
                case AddressBindType.Loopback:
                default:
                    address = IPAddress.Loopback;
                    break;
            }
            return address;
        }
        public ValueTask MulticastAsync(byte[] data)
        {
            if (_server.IsStarted)
                _server.Multicast(data);
            return ValueTask.CompletedTask;
        }

        public ValueTask StartAsync()
        {
            _server.Start();
            _logger.LogInformation(_options.StartupWords, $"{IPAddress}:{Port}");
            return ValueTask.CompletedTask;
        }
        public ValueTask StopAsync()
        {
            _server.Stop();

            return ValueTask.CompletedTask;
        }

        private class InternalServer : TcpServer
        {
            private readonly NetServerAdapter _adapter;
            private readonly ISocketService _socketService;

            public InternalServer(IPAddress address, int port, NetServerAdapter adapter, ISocketService socketService)
                : base(address, port)
            {
                _adapter = adapter;
                _socketService = socketService;
            }

            protected override TcpSession CreateSession()
                => new InternalSession(this, _adapter, _socketService);
        }

        private class InternalSession : TcpSession
        {
            private readonly NetServerAdapter _adapter;
            private readonly ISocketService _socketService;

            public InternalSession(TcpServer server, NetServerAdapter adapter, ISocketService socketService)
                : base(server)
            {
                _adapter = adapter;
                _socketService = socketService;
            }


            protected override void OnConnected()
            {
                var context = new SocketContext(this);
                _adapter.SessionConnected?.Invoke(this, context);
                _socketService.OnConnected(context);
            }

            protected override void OnDisconnected()
            {
                var context = new SocketContext(this);
                _adapter.SessionDisconnected?.Invoke(this, context);
                _socketService.OnDisconnected(context);
            }

            protected override void OnReceived(byte[] buffer, long offset, long size)
            {
                var data = new byte[size];
                Array.Copy(buffer, offset, data, 0, size);

                _socketService.OnReceive(new SocketContext(this), data);
            }



            public new ValueTask SendAsync(byte[] data)
            {
                base.SendAsync(data);

                return ValueTask.CompletedTask;
            }

            public ValueTask DisconnectAsync()
            {
                base.Disconnect();

                return ValueTask.CompletedTask;
            }

        }
    }

}

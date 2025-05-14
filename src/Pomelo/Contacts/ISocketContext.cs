using System.Net;
using System.Threading.Tasks;

namespace Pomelo.Contacts
{
    public interface ISocketContext
    {

        /// <summary>
        /// get the connection id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Local Address
        /// </summary>
        IPEndPoint? LocalEndPoint { get; }

        /// <summary>
        /// Remote Address
        /// </summary>
        IPEndPoint? RemoteEndPoint { get; }


        ValueTask SendAsync(byte[] data);

        ValueTask DisconnectAsync();

        bool IsConnected { get; }

        //event EventHandler<byte[]>? DataReceived;

        //event EventHandler? Disconnected;
    }
}

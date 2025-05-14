using Pomelo.Contacts;
using System;
using System.Threading.Tasks;

namespace Pomelo
{
    public abstract class BaseSocketService<TMessage> : ISocketService where TMessage : IMessage
    {
        public BaseSocketService(IProtocol<TMessage> protocol)
        {
            Protocol = protocol;
        }
        public virtual void OnConnected(ISocketContext context)
        {

        }

        public virtual void OnDisconnected(ISocketContext context)
        {

        }

        public virtual void OnException(ISocketContext context, Exception ex)
        {

        }

        public void OnReceive(ISocketContext context, byte[] data)
        {
            OnReceive(context, Protocol.Decode(data));
        }
        protected IProtocol<TMessage> Protocol { get; private set; }

        protected abstract void OnReceive(ISocketContext context, TMessage message);

        protected virtual Task SendAsync(ISocketContext context, TMessage message)
        {
            context.SendAsync(Protocol.Encode(message));
            return Task.CompletedTask;
        }

    }
}

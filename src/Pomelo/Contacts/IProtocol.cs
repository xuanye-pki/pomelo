namespace Pomelo.Contacts
{
    public interface IProtocol<TMessage> where TMessage : IMessage
    {
        TMessage Decode(byte[] data);


        byte[] Encode(TMessage message);
    }
}

namespace Pomelo.SimpleCommand
{
    public abstract class SimpleCommandSocketService : BaseSocketService<SimpleCommandMessage>
    {

        public SimpleCommandSocketService() : base(new SimpleCommandProtocol())
        {
        }

    }
}

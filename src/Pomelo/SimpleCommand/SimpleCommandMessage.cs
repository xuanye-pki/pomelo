using Pomelo.Contacts;
using System.Text;

namespace Pomelo.SimpleCommand
{
    public class SimpleCommandMessage : IMessage
    {
        public int Length { get; private set; } = 0;

        public SimpleCommandMessage(string command, params string[] args)
        {
            Command = command;
            Args = args;

            Length = CalculateLength();
        }

        private int CalculateLength()
        {
            int length = 0;

            length = Encoding.UTF8.GetByteCount(Command);
            if (Args != null && Args.Length > 0)
            {
                for (var i = 0; i < Args.Length; i++)
                {
                    length += Encoding.UTF8.GetByteCount(Args[i]);
                }
            }
            return length;
        }

        public string Command { get; private set; }
        public string[] Args { get; private set; }
    }
}

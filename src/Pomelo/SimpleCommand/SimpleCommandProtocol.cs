using Pomelo.Contacts;
using System.Text;

namespace Pomelo.SimpleCommand
{
    public class SimpleCommandProtocol : IProtocol<SimpleCommandMessage>
    {
        const char SPLITER = ' ';
        public SimpleCommandMessage Decode(byte[] data)
        {
            string dataString = System.Text.Encoding.UTF8.GetString(data);

            var parts = dataString.Split(SPLITER);
            var command = parts[0];
            SimpleCommandMessage message;
            if (parts.Length == 1)
            {
                message = new SimpleCommandMessage(command);
            }
            else
            {
                var args = new string[parts.Length - 1];
                for (int i = 1; i < parts.Length; i++)
                {
                    args[i - 1] = parts[i];
                }
                message = new SimpleCommandMessage(command, args);
            }

            return message;
        }

        public byte[] Encode(SimpleCommandMessage message)
        {
            if (message.Args?.Length > 0)
            {
                string content = $"{message.Command}{SPLITER}{string.Join(SPLITER, message.Args)}";
                return Encoding.UTF8.GetBytes(message.Command);
            }
            else
            {
                return Encoding.UTF8.GetBytes(message.Command);
            }

        }
    }
}

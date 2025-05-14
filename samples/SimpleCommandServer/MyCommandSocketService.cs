using Microsoft.Extensions.Logging;
using Pomelo.Contacts;
using Pomelo.SimpleCommand;

namespace SimpleCommandServer
{
    public class MyCommandSocketService : SimpleCommandSocketService
    {
        private readonly ILogger<MyCommandSocketService> _logger;
        public MyCommandSocketService(ILogger<MyCommandSocketService> logger)
        {
            _logger = logger;
        }
        public override void OnConnected(ISocketContext context)
        {
            _logger.LogInformation("client connected from {0}", context.RemoteEndPoint);
            base.OnConnected(context);
        }


        public override void OnDisconnected(ISocketContext context)
        {
            _logger.LogInformation("client disconnected from {0}", context.RemoteEndPoint);
            base.OnDisconnected(context);
        }

        public override void OnException(ISocketContext context, Exception ex)
        {
            _logger.LogError(ex, "client from {0},  occ error {1}", context.RemoteEndPoint, ex.Message);
            base.OnException(context, ex);
        }


        protected override void OnReceive(ISocketContext context, SimpleCommandMessage message)
        {
            _logger.LogInformation("receive msg from {0},{1}", context.RemoteEndPoint, message.Command);
            string replyMessage = string.Empty;
            string replyCmd = string.Empty;
            switch (message.Command)
            {
                case "echo":
                    replyMessage = message.Args[0];
                    replyCmd = "echo";
                    break;
                case "init":
                    replyMessage = "ok";
                    replyCmd = "init_reply";

                    break;
                case "idle":
                    replyMessage = "ok";
                    replyCmd = "idle_reply";
                    break;
                default:
                    replyMessage = "error unknow command";
                    break;
            }

            Task.Run(() =>
            {
                base.SendAsync(context, new SimpleCommandMessage(replyCmd, replyMessage));
            });
        }
    }

}

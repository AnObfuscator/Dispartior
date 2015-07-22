using System;
using Dispartior.Configuration;

namespace Dispartior.Messaging.Messages.Commands
{
    public class Register : BaseMessage
    {
        public string UUID { get; set; }

        public string Name { get; set; }

        public ServerConfiguration Configuration { get; set; }
    }
}


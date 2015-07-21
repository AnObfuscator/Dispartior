using System;

namespace Dispartior.Configuration
{
    public class ServerConfiguration
    {
        public ServerTypes Type { get; set; }

        public string IpAddress { get; set; }

        public int Port { get; set; }

		public int PoolSize { get; set; }
    }
}


using System;
using Dispartior.Configuration;
using Dispartior.Algorithms;
using Dispartior.Servers.Mediator;
using Dispartior.Servers.Compute;

namespace Dispartior.Servers
{
    public class ServerFactory
    {
        public SystemConfiguration SystemConfig { get; private set; }

		public AlgorithmFactory AlgorithmFactory { get; private set; }

        public ServerFactory(SystemConfiguration configuration, AlgorithmFactory algoFactory)
        {
            SystemConfig = configuration;
			AlgorithmFactory = algoFactory;
        }

        public IServer CreateServer(string serverName)
        {
            IServer server = null;
            var serverConfig = SystemConfig.Servers[serverName];

            if (serverConfig.Type == ServerTypes.Mediator)
            {
                server = new MediatorServer(SystemConfig);
            }
            else if (serverConfig.Type == ServerTypes.Compute) 
            {
				server = new ComputeServer(SystemConfig, serverName, AlgorithmFactory);
            }

            return server;
        }

    }
}


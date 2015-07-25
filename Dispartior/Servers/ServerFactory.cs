using System;
using Dispartior.Configuration;
using Dispartior.Algorithms;
using Dispartior.Servers.Mediator;
using Dispartior.Servers.Compute;
using Dispartior.Data;
using Dispartior.Servers.Cache;

namespace Dispartior.Servers
{
    public class ServerFactory
    {
        public SystemConfiguration SystemConfig { get; private set; }

        public AlgorithmFactory AlgorithmFactory { get; private set; }

        public DataSource DataSource { get; private set; }

        public ServerFactory(SystemConfiguration configuration, AlgorithmFactory algoFactory, DataSource dataSource)
        {
            SystemConfig = configuration;
            AlgorithmFactory = algoFactory;
            DataSource = dataSource;
        }

        public IServer CreateServer(string serverName)
        {
            IServer server = null;
            var serverConfig = SystemConfig.Servers[serverName];

            if (serverConfig.Type == ServerTypes.Mediator)
            {
                server = new MediatorServer(SystemConfig, DataSource);
            }
            else if (serverConfig.Type == ServerTypes.Compute)
            {
                server = new ComputeServer(SystemConfig, serverName, AlgorithmFactory);
            }
            else if (serverConfig.Type == ServerTypes.Cache)
            {
                server = new CacheServer(SystemConfig);
            }

            return server;
        }

    }
}


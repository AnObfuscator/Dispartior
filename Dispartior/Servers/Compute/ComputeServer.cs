using System;
using System.Threading;
using System.Linq;
using Nancy.Hosting.Self;
using Nancy;
using Dispartior.Configuration;
using Dispartior.Servers.Common;
using Dispartior.Algorithms;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Data;

namespace Dispartior.Servers.Compute
{
    public class ComputeServer : IServer
    {
        private readonly string uuid;
        private readonly string name;
        private readonly ServerConfiguration config;
        private readonly DefaultNancyBootstrapper bootstrapper;
        private readonly WorkerPool workerPool;
        private readonly MediatorConnector mediator;

        public ComputeServer(SystemConfiguration systemConfig, string name, AlgorithmFactory algoFactory)
        {
            uuid = Guid.NewGuid().ToString();
            this.name = name;
            config = systemConfig.Servers[name];

            var mediatorConfig = systemConfig.Servers.First(kvp => kvp.Value.Type == ServerTypes.Mediator).Value;
            mediator = new MediatorConnector(mediatorConfig);

            workerPool = new WorkerPool(config.PoolSize, uuid, mediator);

            //bootstrapper = new ComputeBootstrapper(this, workerPool);
            bootstrapper = new ApiBootstrapper(this, workerPool, algoFactory);
        }

        public void Start()
        {
            var url = BuildUri();
            var apiConfig = BuildApiConfiguration();
            using (var computeAPI = new NancyHost(url, bootstrapper, apiConfig))
            {
                computeAPI.Start();

                Console.WriteLine("Registering with mediator...");
                Register();

                Console.WriteLine("Compute running.");
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void Register()
        {
            var registration = new Register { Name = name, UUID = uuid, Configuration = config };
            while (!mediator.Register(registration))
            {
                Console.WriteLine("Registration failed... reattempting in 5 seconds...");
                Thread.Sleep(5000);
            }
        }

        public Uri BuildUri()
        {
            var address = string.Format("http://{0}:{1}", config.IpAddress, config.Port);
            return new Uri(address);
        }

        public HostConfiguration BuildApiConfiguration()
        {
            var hostConfig = new HostConfiguration();
            hostConfig.RewriteLocalhost = false;
            return hostConfig;
        }
           
    }
}


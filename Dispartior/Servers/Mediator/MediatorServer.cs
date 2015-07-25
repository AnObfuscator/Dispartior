using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Nancy.Hosting.Self;
using Nancy;
using Dispartior.Configuration;
using Dispartior.Data;
using Dispartior.Servers.Common;

namespace Dispartior.Servers.Mediator
{
    public class MediatorServer : IServer
    {
        private readonly ServerConfiguration config;
        private readonly Controller controller;
        private readonly DefaultNancyBootstrapper bootstrapper;

        public MediatorServer(SystemConfiguration systemConfig, DataSource dataSource)
        {
            config = systemConfig.Servers.First(kvp => kvp.Value.Type == ServerTypes.Mediator).Value;
            controller = new Controller(dataSource);
//			bootstrapper = new MediatorBootstrapper(controller);
            bootstrapper = new ApiBootstrapper(controller);
        }

        public void Start()
        {
            var url = ServerUtils.BuildUri(config);
            var apiConfig = ServerUtils.BuildApiConfiguration();
            StaticConfiguration.DisableErrorTraces = false;
            using (var mediatorAPI = new NancyHost(url, bootstrapper, apiConfig))
            {
                mediatorAPI.Start();

                var heartbeatTimer = new Timer(SendHeartbeats);
                heartbeatTimer.Change(0, 5000);

                var computationTimer = new Timer(UpdateComputations);
                computationTimer.Change(0, 1000);

                Console.WriteLine("Mediator running.");
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void SendHeartbeats(object timerParam)
        {
            var statusUpdates = controller.SendHeartbeats();
            //statusUpdates.ForEach(su => Console.WriteLine("Got status update for: "+su.Serialize()));
        }

        private void UpdateComputations(object timerParam)
        {
            controller.UpdateComputation();
        }
			
    }
}


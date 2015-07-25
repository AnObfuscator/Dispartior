using System;
using Nancy;
using Nancy.TinyIoc;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Dispartior.Configuration;
using Dispartior.Servers.Compute;
using Dispartior.Algorithms;
using Dispartior.Servers.Mediator;
using Dispartior.Data;
using Dispartior.Servers.Cache;

namespace Dispartior.Servers
{
    public class ApiBootstrapper : DefaultNancyBootstrapper
    {
        private readonly ServerTypes serverType;

        private readonly ComputeServer computeServer;
        private readonly WorkerPool workerPool;
        private readonly AlgorithmFactory algoFactory;

        private readonly Controller controller;

        public ApiBootstrapper()
        {
            serverType = ServerTypes.Cache;
        }

        public ApiBootstrapper(Controller controller)
        {
            this.controller = controller;
            serverType = ServerTypes.Mediator;
        }

        public ApiBootstrapper(ComputeServer computeServer, WorkerPool workerPool, AlgorithmFactory algoFactory)
        {
            this.computeServer = computeServer;
            this.workerPool = workerPool;
            this.algoFactory = algoFactory;
            serverType = ServerTypes.Compute;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
//			if (serverType == ServerTypes.Compute)
//			{
            container.Register<ComputeAPI>();
            container.Register(typeof(AlgorithmFactory), algoFactory);
            container.Register(typeof(ComputeServer), computeServer);
            container.Register(typeof(WorkerPool), workerPool);
//			}
//			else if (serverType == ServerTypes.Mediator)
//			{
            container.Register<MediatorAPI>();
            container.Register(typeof(Controller), controller);
//			}
            container.Register<CacheAPI>();
        }

    }
}


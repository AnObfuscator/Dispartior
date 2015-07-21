using System;
using Nancy;
using Nancy.TinyIoc;

namespace Dispartior.Servers.Compute
{
    public class ComputeBootstrapper : DefaultNancyBootstrapper
    {
		private readonly ComputeServer computeServer;
		private readonly WorkerPool workerPool;

		public ComputeBootstrapper(ComputeServer computeServer, WorkerPool workerPool)
		{
			this.computeServer = computeServer;
			this.workerPool = workerPool;
		}

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<ComputeAPI>();
			container.Register(typeof(ComputeServer), computeServer);
			container.Register(typeof(WorkerPool), workerPool);
        }
    }
}


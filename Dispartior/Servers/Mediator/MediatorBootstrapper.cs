using System;
using Nancy;
using Nancy.TinyIoc;

namespace Dispartior.Servers.Mediator
{
    public class MediatorBootstrapper : DefaultNancyBootstrapper
    {
		private readonly Controller controller;

		public MediatorBootstrapper(Controller controller)
		{
			this.controller = controller;
		}

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			container.Register<MediatorAPI>();
			container.Register(typeof(Controller), controller);
		}
    }
}


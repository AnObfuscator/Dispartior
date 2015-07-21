using System;
using Dispartior.Messaging.Messages.Responses;

namespace Dispartior.Servers.Common
{
	public class WorkerInfo
	{
		public ComputeConnector Connector { get; set; }

		public string Id
		{
			get;
			set;
		}

		public WorkerStatus Status
		{
			get;
			set;
		}
	}
}


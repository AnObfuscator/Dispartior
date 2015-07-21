using System;
using System.Collections.Generic;
using System.Linq;
using Dispartior.Configuration;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages.Requests;
using Dispartior.Messaging.Messages.Commands;

namespace Dispartior.Servers.Common
{
    public class ComputeConnector
    {
		private readonly ServerConfiguration computeNodeInfo;
		private readonly ServiceInterface serviceInterface;
		private readonly List<WorkerInfo> workers;

		public List<WorkerInfo> Workers
		{
			get
			{
				return workers;
			}
		}

		public IEnumerable<WorkerInfo> AvailableWorkers
		{
			get
			{
				return workers.Where(w => w.Status == WorkerStatus.Idle);
			}
		}

		public string UUID { get; private set; }

		public string Name { get; private set; }


		public ComputeConnector(string uuid, string name, ServerConfiguration computeInfo)
        {
			UUID = uuid;
			Name = name;
			this.computeNodeInfo = computeInfo;
			serviceInterface = new ServiceInterface(computeNodeInfo.IpAddress, computeNodeInfo.Port);

			workers = new List<WorkerInfo>();
			for (int i = 0; i < computeInfo.PoolSize; i++)
			{
				var workerInfo = new WorkerInfo();
				workerInfo.Id = string.Format("Worker_{0}", i);
				workerInfo.Status = WorkerStatus.Idle;
				workerInfo.Connector = this;
				workers.Add(workerInfo);
			}
        }

        public ComputeStatus SendHeartbeat(Heartbeat heartbeat)
        {
			ComputeStatus status;
			var success = serviceInterface.Post(heartbeat, "/heartbeat", out status);
			return status;
        }

		public void StartComputation(Computation computation)
		{
			Console.WriteLine("Calling start computation on node... " + computation.Serialize());
			serviceInterface.Post(computation, "/doComputation");
		}

    }
}


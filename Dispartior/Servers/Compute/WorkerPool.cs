using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Dispartior.Servers.Common;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Algorithms;
using Dispartior.Data;

namespace Dispartior.Servers.Compute
{
	public class WorkerPool
	{
		private readonly ConcurrentDictionary<string, Worker> workers; 
		private readonly MediatorConnector mediator;
		private readonly string nodeId;
		private readonly DataSourceFactory dataSourceFactory;

		public IDictionary<string, WorkerStatus> Status
		{
			get
			{
				var status = new Dictionary<string, WorkerStatus>();
				foreach (var entry in workers)
				{
					status[entry.Key] = entry.Value.Status;
				}
				return status;
			}
		}

		public WorkerPool(int poolSize, string nodeId, MediatorConnector mediator)
		{
			this.nodeId = nodeId;
			this.mediator = mediator;
			this.dataSourceFactory = dataSourceFactory;

			workers = new ConcurrentDictionary<string, Worker>();
			for (int i = 0; i < poolSize; i++)
			{
				var workerId = string.Format("Worker_{0}", i);
				workers[workerId] = new Worker(workerId, this);
			}
		}

		public void AssignToWorker(IAlgorithm algorithm, string workerId)
		{
			Console.WriteLine(string.Format("Running algo {0} on worker {1}", algorithm, workerId));
			workers[workerId].Run(algorithm);
		}

		public void FinishComputation(string workerId)
		{
			var result = new ComputationResult{ UUID = nodeId, WorkerId = workerId }; 
			mediator.SendComputationResult(result);
		}

	}
}


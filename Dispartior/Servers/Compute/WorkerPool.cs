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

		public IDictionary<string, RunnerStatus> Status
		{
			get
			{
				var status = new Dictionary<string, RunnerStatus>();
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
            try 
            {
			    workers[workerId].Run(algorithm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error running algo {0} on worker {1}: {2}", algorithm, workerId, ex.Message));
                FinishComputation(workerId, false);
            }
		}

        public void FinishComputation(string workerId, bool success)
		{
            var status = success ? ResultStatus.Success : ResultStatus.Failure;
            var result = new ComputationResult{ UUID = nodeId, WorkerId = workerId, Status = status }; 
			mediator.SendComputationResult(result);
		}

	}
}


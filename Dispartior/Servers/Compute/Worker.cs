using System;
using System.Threading;
using Dispartior.Algorithms;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Data;

namespace Dispartior.Servers.Compute
{
	public class Worker : IAlgorithmRunner
    {
		private volatile WorkerStatus status = WorkerStatus.Idle;
		public WorkerStatus Status 
		{ 
			get
			{
				return status;
			}
		}

		public string Id { get; private set; }

		private readonly WorkerPool workerPool;

		public Worker(string id, WorkerPool workerPool)
        {
			Id = id;
			this.workerPool = workerPool;
        }

		public void Run(IAlgorithm algorithm)
		{
			status = WorkerStatus.Running;
			algorithm.AlgorithmRunner = this;

			new Thread(() =>
				{
					Console.WriteLine("Worker thread running algorithm...");

					algorithm.Run();

					status = WorkerStatus.Idle;

					workerPool.FinishComputation(Id);
				}).Start();
		}
    }
}


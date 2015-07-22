using System;
using System.Collections.Generic;
using Dispartior.Servers.Common;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Utilities;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages.Requests;
using Dispartior.Messaging.Messages;
using Dispartior.Data;

namespace Dispartior.Servers.Mediator
{
	public class Controller
	{

		private readonly object controllerLock = new object();

		private readonly List<ComputeConnector> computeNodes;

		private volatile Computation currentComputation;

		private volatile bool computationInProgress;

		private readonly Queue<Computation> todo;

		private readonly DataSourceFactory dataSourceFactory;

		public Controller(DataSourceFactory dataSourceFactory)
		{
			todo = new Queue<Computation>();
			computeNodes = new List<ComputeConnector>();
			this.dataSourceFactory = dataSourceFactory;
		}

		public void StartComputation(Computation computation)
		{
			lock (controllerLock)
			{
				Console.WriteLine("Computation: " + computation.Serialize());
				currentComputation = computation;
				var dataSourceConfig = computation.DataSourceConfiguration;
				var partitioner = dataSourceFactory.GetDataPartitioner(dataSourceConfig);
				var partitions = partitioner.Partition(dataSourceConfig, computation.PartitionSize);
				foreach (var dataPartition in partitions)
				{
					var computationPartition = new Computation();
					computationPartition.Algorithm = computation.Algorithm;
					computationPartition.DataSourceConfiguration = dataPartition;
					todo.Enqueue(computationPartition);
				}
				computationInProgress = true;
				Console.WriteLine("Computation started.");
			}
		}
			
		public void UpdateComputation()
		{
			if (!computationInProgress)
			{
				return;
			}

			Console.WriteLine("Updating Computations...");
			lock (controllerLock)
			{
				Console.WriteLine("Available Tasks: " + todo.Count);
				var availableWorkers = new List<WorkerInfo>();
				foreach (var cc in computeNodes)
				{
					availableWorkers.AddRange(cc.AvailableWorkers);
				}
				Console.WriteLine("Avaliable workers: " + availableWorkers.Count);
				foreach (var worker in availableWorkers)
				{
					if (todo.IsEmpty())
					{
						Console.WriteLine("No more tasks.");
						break;
					}
					var computation = todo.Dequeue();
					computation.Worker = worker.Id;
					worker.Connector.StartComputation(computation);
					worker.Status = WorkerStatus.Running;
					Console.WriteLine("Performaing computation: " + computation.Serialize());
				}

				StopIfFinished();
			}
		}

		private void StopIfFinished()
		{
			if (todo.IsEmpty())
			{
				if (computeNodes.TrueForAll(cn => cn.Workers.TrueForAll(w => w.Status == WorkerStatus.Idle)))
				{
					computationInProgress = false;
					Console.WriteLine("Computation Finished.");
				}
			}
		}

		public void UpdateComputation(ComputationResult result)
		{
			lock (controllerLock)
			{
				var worker = computeNodes.Find(cn => cn.UUID == result.UUID).Workers.Find(w => w.Id == result.WorkerId);
				worker.Status = WorkerStatus.Idle;
			}
		}
			
		public List<ComputeStatus> SendHeartbeats()
		{
			lock (controllerLock)
			{
				var statusUpdates = new List<ComputeStatus>();
				foreach (var connector in computeNodes)
				{
					try
					{
						var heartbeat = Heartbeat.CreateNew();
						var status = connector.SendHeartbeat(heartbeat);
						statusUpdates.Add(status);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Heartbeat Error for " + connector.Name + ": "+ ex.Message);
					}
				}
				return statusUpdates;
			}
		}
			
		public void RegisterNew(Register registration)
		{
			var uuid = registration.UUID;
			var name = registration.Name;
			var config = registration.Configuration;
			var newNode = new ComputeConnector(uuid, name, config);

			lock (controllerLock)
			{
				computeNodes.Add(newNode);
			}
		}

		public void Unregister(Register registration)
		{
			lock (controllerLock)
			{
				var nodeToRemove = computeNodes.Find(cn => cn.UUID == registration.UUID);
				// TODO figure out how to reset calculations in progress
				computeNodes.Remove(nodeToRemove);
			}
		}
			
	}

}


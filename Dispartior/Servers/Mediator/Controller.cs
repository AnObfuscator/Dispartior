﻿using System;
using System.Collections.Generic;
using Dispartior.Servers.Common;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Utilities;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages.Requests;
using Dispartior.Messaging.Messages;
using Dispartior.Data;
using Dispartior.StatusCodes;

namespace Dispartior.Servers.Mediator
{
    public class Controller
    {
        private readonly object controllerLock = new object();

        private readonly List<ComputeConnector> computeNodes;

        private volatile Computation currentComputation;

        private volatile bool computationInProgress;

        private readonly Queue<Computation> todo;

        private readonly DataSource dataSource;

        public Controller(DataSource dataSource)
        {
            todo = new Queue<Computation>();
            computeNodes = new List<ComputeConnector>();
            this.dataSource = dataSource;
        }

        public void StartComputation(Computation computation)
        {
            lock (controllerLock)
            {
                currentComputation = computation;
                var dataSourceConfig = computation.DataSetDefinition;
                var partitioner = dataSource.GetDataPartitioner(dataSourceConfig);
                var partitions = partitioner.Partition(dataSourceConfig, computation.PartitionSize);
                foreach (var dataPartition in partitions)
                {
                    var computationPartition = new Computation();
                    computationPartition.Algorithm = computation.Algorithm;
                    computationPartition.Parameters = computation.Parameters;
                    computationPartition.DataSetDefinition = dataPartition;
                    todo.Enqueue(computationPartition);
                }
                computationInProgress = true;
                Console.WriteLine("Computation started with {0} tasks.", todo.Count);
            }
        }

        public void UpdateComputation()
        {
            lock (controllerLock)
            {
                if (!computationInProgress)
                {
                    return;
                }

                Console.WriteLine("Updating Computations...");

                Console.WriteLine("Available Tasks: {0}", todo.Count);
                var availableWorkers = new List<WorkerAdapter>();
                foreach (var cc in computeNodes)
                {
                    availableWorkers.AddRange(cc.AvailableWorkers);
                }
                Console.WriteLine("Avaliable workers: {0}", availableWorkers.Count);
                foreach (var worker in availableWorkers)
                {
                    if (todo.IsEmpty())
                    {
                        Console.WriteLine("No more tasks.");
                        break;
                    }
                    Console.WriteLine("Assigning next task to: {0}@{1}", worker.Id, worker.Connector.Name);
                    AsignNextTaskTo(worker);
                }

                StopIfFinished();
            }
        }

        private void AsignNextTaskTo(WorkerAdapter worker)
        {
            var computation = todo.Dequeue();
            try
            {
                worker.StartComputation(computation);
            }
            catch (Exception ex)
            {
                todo.Enqueue(computation);
                Console.WriteLine("Could not perform calculation on {0}@{1} -- {2}", worker.Id, worker.Connector.Name, ex.Message);  
            }
        }

        private void StopIfFinished()
        {
            if (todo.IsEmpty())
            {
                if (computeNodes.TrueForAll(cn => cn.Workers.TrueForAll(w => w.Status == RunnerStatus.Idle)))
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
                Console.WriteLine("Got update from {0}@{1} ", result.WorkerId, result.UUID);
                var worker = computeNodes.Find(cn => cn.UUID == result.UUID).Workers.Find(w => w.Id == result.WorkerId);
                var computation = worker.FinishComputation();
                if (result.Status == ResultStatus.Failure)
                {
                    Console.WriteLine("Computation failure... reattempting.");
                    todo.Enqueue(computation);
                }
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
                        Console.WriteLine("Heartbeat Error for {0}: {1}", connector.Name, ex.Message);
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


﻿using System;
using System.Threading;
using Dispartior.Algorithms;
using Dispartior.StatusCodes;

namespace Dispartior.Servers.Compute
{
    public class Worker : IAlgorithmRunner
    {
        private volatile RunnerStatus status = RunnerStatus.Idle;

        public RunnerStatus Status
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
            status = RunnerStatus.Running;
            algorithm.AlgorithmRunner = this;

            new Thread(() =>
                {
                    try
                    {
                        Console.WriteLine("Worker thread running algorithm...");
                        algorithm.Run();
                        status = RunnerStatus.Idle;
                        workerPool.FinishComputation(Id, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in thread running algorithm: " + ex.Message);
                        workerPool.FinishComputation(Id, false);
                    }
                }).Start();
        }
    }
}


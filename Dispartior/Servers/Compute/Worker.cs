using System;
using System.Threading;
using Dispartior.Algorithms;
using Dispartior.StatusCodes;
using System.Collections.Generic;

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

        public void Run(IAlgorithm algorithm, IDictionary<string, string> parameters)
        {
            status = RunnerStatus.Running;
            algorithm.AlgorithmRunner = this;

            new Thread(() =>
                {
                    try
                    {
                        Console.WriteLine("Worker thread running algorithm...");
                        var result = algorithm.Run(parameters);
                        workerPool.FinishComputation(Id, true, result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in thread running algorithm: " + ex.Message);
                        workerPool.FinishComputation(Id, false);
                    }
                    finally
                    {
                        status = RunnerStatus.Idle;
                    }
                }).Start();
        }
    }
}


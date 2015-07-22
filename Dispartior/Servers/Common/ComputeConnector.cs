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
        private readonly List<WorkerAdapter> workers;

        public List<WorkerAdapter> Workers
        {
            get
            {
                return workers;
            }
        }

        public IEnumerable<WorkerAdapter> AvailableWorkers
        {
            get
            {
                return workers.Where(w => w.Status == RunnerStatus.Idle);
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

            workers = new List<WorkerAdapter>();
            for (int i = 0; i < computeInfo.PoolSize; i++)
            {
                var workerInfo = new WorkerAdapter();
                workerInfo.Id = string.Format("Worker_{0}", i);
                workerInfo.Status = RunnerStatus.Idle;
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


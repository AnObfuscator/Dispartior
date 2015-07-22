using Dispartior.Messaging.Messages.Commands;
using Dispartior.StatusCodes;

namespace Dispartior.Servers.Common
{
    public class WorkerAdapter
    {
        public ComputeConnector Connector { get; set; }

        public string Id
        {
            get;
            set;
        }

        public RunnerStatus Status
        {
            get;
            set;
        }

        public Computation Computation
        {
            get;
            set;
        }

        public void StartComputation(Computation computation)
        {
            Status = RunnerStatus.Running;
            Computation = computation;
            Computation.Worker = Id;
            Connector.StartComputation(computation);
        }

        public Computation FinishComputation()
        {
            var computation = Computation;
            Computation = null;
            Status = RunnerStatus.Idle;
            return computation;
        }
    }
}


using System;
using Nancy;
using Nancy.Extensions;
using Dispartior.Algorithms;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Messaging.Messages.Requests;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages;

namespace Dispartior.Servers.Compute
{
    public class ComputeAPI : NancyModule
    {
		private readonly WorkerPool workerPool;
		private readonly AlgorithmFactory algorithmFactory;

		public ComputeAPI()
		{
			Console.WriteLine("Doing Compute");
		}

		public ComputeAPI(WorkerPool workerPool, AlgorithmFactory algorithmFactory) 
        {
			this.workerPool = workerPool;
			this.algorithmFactory = algorithmFactory;

            Post["/doComputation"] = _ =>
	            {
					Console.WriteLine("doing computation task...");
					var computation = DeserializeBody<Computation>();
					var workerId = computation.Worker;
					var algoName = computation.Algorithm;
					var algorithm = algorithmFactory.CreateAlgorithm(algoName);
					workerPool.AssignToWorker(algorithm, workerId);
	                return HttpStatusCode.OK;
	            };

            Post["/heartbeat"] = _ =>
	            {
					var heartbeat = DeserializeBody<Heartbeat>();
	                Console.WriteLine("Got heartbeat: " + heartbeat.Serialize());
	                var status = RespondToHeartbeat(heartbeat);
	                return status.Serialize();
	            };

            Get["/status"] = _ =>
	            {
	                return HttpStatusCode.OK;
	            };
        }

        public ComputeStatus RespondToHeartbeat(Heartbeat heartbeat)
        {
            var status = new ComputeStatus();
            status.Timestamp = DateTime.UtcNow;
            status.UUID = heartbeat.UUID;
            return status;
        }

		public T DeserializeBody<T>()
		{
			var body = Request.Body.AsString();
			return BaseMessage.Deserialize<T>(body);
		}
    }
}


using System;
using Nancy;
using Nancy.Extensions;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages;

namespace Dispartior.Servers.Mediator
{
    public class MediatorAPI : NancyModule
    {
        private readonly Controller controller;

        public MediatorAPI(Controller controller)
        {
            this.controller = controller;

            Post["/computationStart"] = _ =>
            {
                try
                {
                    Console.WriteLine("Starting computation...");
                    var computation = DeserializeBody<Computation>();
                    controller.StartComputation(computation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error starting computation: {0}", ex.Message);	
                }

                return HttpStatusCode.OK;
            };

            Post["/computationResult"] = _ =>
            {
                Console.WriteLine("Updating with result...");
                try
                {
                    var result = DeserializeBody<ComputationResult>();
                    Console.WriteLine("Result: {0}", result);
                    controller.UpdateComputation(result);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error updating result: {0}", ex.Message);
                }

                return HttpStatusCode.OK;
            };

            Get["/status"] = _ =>
            {
                Console.WriteLine("Master Status Called.");
                return HttpStatusCode.OK;
            };

            Post["/register"] = _ =>
            {
                Console.WriteLine("Registering new node...");
                var registration = DeserializeBody<Register>();
                controller.RegisterNew(registration);

                return HttpStatusCode.OK;
            };

            Delete["/unregister"] = _ =>
            {
                Console.WriteLine("Unregisetering node...");
                var registration = DeserializeBody<Register>();
                controller.Unregister(registration);

                return HttpStatusCode.NoContent;
            };
        }

        public T DeserializeBody<T>() //where T : BaseMessage
        {
            var body = Request.Body.AsString();
            return BaseMessage.Deserialize<T>(body);
        }
    }
}


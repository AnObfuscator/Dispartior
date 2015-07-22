using System;
using Dispartior.Configuration;
using Dispartior.Messaging.Messages.Responses;
using Dispartior.Messaging.Messages.Commands;

namespace Dispartior.Servers.Common
{
    public class MediatorConnector
    {
        private readonly ServiceInterface serviceInterface;

        public MediatorConnector(ServerConfiguration mediatorConfig)
        {
            serviceInterface = new ServiceInterface(mediatorConfig.IpAddress, mediatorConfig.Port);
        }

        public bool Register(Register registration)
        {
            try
            {
                return serviceInterface.Post(registration, "/register");
            }
            catch (Exception e)
            {
                Console.WriteLine("Registration error: " + e.Message);
            }

            return false;
        }

        public void SendComputationResult(ComputationResult result)
        {
            try
            {
                serviceInterface.Post(result, "/computationResult");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending results: " + ex.Message);
            }
        }
    }
}


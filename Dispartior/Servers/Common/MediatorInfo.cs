using System;
using System.Net;

namespace Dispartior.Servers.Common
{
    public class MediatorInfo
    {
        public IPEndPoint Endpoint { get; private set; }

        public MediatorInfo(string ipAddress, int port)
        {
            Endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }
    }
}


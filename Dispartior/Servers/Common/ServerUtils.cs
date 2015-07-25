using System;
using Dispartior.Configuration;
using Nancy.Hosting.Self;

namespace Dispartior.Servers.Common
{
    public static class ServerUtils
    {
        public static Uri BuildUri(ServerConfiguration config)
        {
            var address = string.Format("http://{0}:{1}", config.IpAddress, config.Port);
            return new Uri(address);
        }

        public static HostConfiguration BuildApiConfiguration()
        {
            var hostConfig = new HostConfiguration();
            hostConfig.RewriteLocalhost = false;
            return hostConfig;
        }
    }
}


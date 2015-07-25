using System;
using Dispartior.Configuration;
using Nancy;
using Nancy.Hosting.Self;
using Dispartior.Servers.Common;
using System.Threading;
using System.Linq;

namespace Dispartior.Servers.Cache
{
    public class CacheServer : IServer
    {
        private readonly string name;
        private readonly ServerConfiguration config;
        private readonly DefaultNancyBootstrapper bootstrapper;


        public CacheServer(SystemConfiguration systemConfig)
        {
            config = systemConfig.Servers.First(kvp => kvp.Value.Type == ServerTypes.Cache).Value;
            bootstrapper = new ApiBootstrapper();
        }
            
        public void Start()
        {
            var url = ServerUtils.BuildUri(config);
            var cacheApiConfig = ServerUtils.BuildApiConfiguration();
            using (var cacheAPI = new NancyHost(url, bootstrapper, cacheApiConfig))
            {
                cacheAPI.Start();

                Console.WriteLine("Cache Server running...");
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
        }

    }
}


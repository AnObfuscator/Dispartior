using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Dispartior.Configuration
{
    public class ConfigurationLoader
    {
        private readonly string configFilename;

        public ConfigurationLoader(string configFilename)
        {
            if (!File.Exists(configFilename))
            {
                throw new Exception(string.Format("Config file {0} not found.", configFilename));
            }

            this.configFilename = configFilename;
        }

        public SystemConfiguration LoadConfiguration()
        {
            var configJson = File.ReadAllText(configFilename);
            var config = JsonConvert.DeserializeObject<SystemConfiguration>(configJson);
            return config;
        }

        public SystemConfiguration GetDummyConfiguration()
        {
            var dummyMasterConfig = new ServerConfiguration { Type = ServerTypes.Mediator, IpAddress = "127.0.0.1", Port = 8080 };
            var dummySlaveConfig = new ServerConfiguration { Type = ServerTypes.Compute, IpAddress = "127.0.0.1", Port = 8081 };

            var servers = new Dictionary<string, ServerConfiguration>()
            { 
                { "master", dummyMasterConfig },
                { "slave", dummySlaveConfig }
            };

            var dummySystemConfig = new SystemConfiguration();
            dummySystemConfig.Servers = servers;

            var configJson = JsonConvert.SerializeObject(dummySystemConfig);
            Console.WriteLine(configJson);

            return dummySystemConfig;
        }
    }
}


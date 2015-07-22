using System;

/**
 * TODO
 * * (1) create a master & slave that talk to each other
 * * (1a) port to HTTP using NancyFX
 * * (2) create a simple algorithm, get the master to execute on the slave
 * (3) create a multi-step algorithm, get the master to execute each step on the slaves
 * (4) generalize the whole algorithm/multi-step thing like we talked (separate out the framework from the specific implementations)
 * * (5) multithread slave algorithms
 * (6) implement UI
 */

/*
 * Data Types:
 * (1) Range -- Range of numbers (start, end, stepsize), partitionSize -- number of numbers per worker
 * (2) Document -- Each Line is an entry, partitionSize -- number of lines per worker
 *          Need to specify: DocumentName, DocumentType
 */

using SampleServer.Algorithms;
using Dispartior.Algorithms;
using Dispartior.Messaging.Messages.Commands;
using Dispartior.Configuration;
using Dispartior.Servers;
using Dispartior.Data;

namespace SampleServer
{
    class MainClass
    {
        public const string DefaultConfigFilename = "config.json";

        public static void Main(string[] args)
        {
            var computation = new Computation { Algorithm = typeof(AlgoOne).ToString(), PartitionSize = 100 };
            Console.WriteLine("Computation start body: " + computation.Serialize());

            var serverName = args[0];

            var configLoader = new ConfigurationLoader(DefaultConfigFilename);
            var systemConfig = configLoader.LoadConfiguration();

            var dataSourceFactory = new DataSourceFactory();

            var algoFactory = new AlgorithmFactory(dataSourceFactory);
            algoFactory.RegisterAlgorithm<AlgoOne>();

            var serverFactory = new ServerFactory(systemConfig, algoFactory, dataSourceFactory);

            var server = serverFactory.CreateServer(serverName);
            server.Start();
        }

    }
}

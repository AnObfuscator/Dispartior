using System;
using System.Numerics;
using Dispartior.Algorithms;
using Dispartior.Data;
using System.Collections.Generic;

namespace SampleServer.Algorithms
{
    public class AlgoOne : IAlgorithm
    {
        public IAlgorithmRunner AlgorithmRunner { get; set; }

        public IDataSetDefinition DataSetDefinition { get; set; }

        public DataSource DataSource { get; set; }

        public AlgoOne()
        {
        }

        public IDataSetDefinition Run(IDictionary<string, string> parameters)
        {
            RandomFail();

            var number = BigInteger.Parse(parameters["number"]);
            var dataSet = DataSource.GetDataSet<BigInteger>(DataSetDefinition);
            var results = new List<BigInteger>();
            while (dataSet.HasNext())
            {
                var data = dataSet.GetNext();
                var result = number + data;
                results.Add(result);
                Console.WriteLine("Calculation: {0} + {1} = {2} " + number, data, result);

            }
            //dataSource.SaveResult(result);
            var resultDataSet = DataSource.SaveResults(results);

            Console.WriteLine("AlgoOne ran.");

            return resultDataSet;
        }

        private void RandomFail()
        {
            var random = new Random();
            if (random.NextDouble() < 0.2) // 20% chance of failure
            {
                throw new Exception("************* Whoops Random Error! *************");
            }
        }

    }
}


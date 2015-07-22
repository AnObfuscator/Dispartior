using System;
using System.Numerics;
using Dispartior.Algorithms;
using Dispartior.Data;

namespace SampleServer.Algorithms
{
	public class AlgoOne : IAlgorithm
	{
		public IAlgorithmRunner AlgorithmRunner { get; set; }

		public IDataSourceConfiguration DataSourceConfiguration { get; set; }

		public DataSourceFactory DataSourceFactory { get; set; }

        public AlgoOne()
        {
		}
			
		public void Run()
		{
            RandomFail();
            var dataSource = DataSourceFactory.GetDataSource<BigInteger>(DataSourceConfiguration);
			while (dataSource.HasNext())
			{
				var data = dataSource.GetNext();

				Console.WriteLine("Doing: " + data);
			}

			Console.WriteLine("AlgoOne ran.");
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


using System;
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
			var dataSource = DataSourceFactory.GetDataSource<int>(DataSourceConfiguration);
			while (dataSource.HasNext())
			{
				var data = dataSource.GetNext();

				Console.WriteLine("Doing: " + data);
			}

			Console.WriteLine("AlgoOne ran.");
		}

    }
}


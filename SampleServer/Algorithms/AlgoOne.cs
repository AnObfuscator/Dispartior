using System;
using Dispartior.Algorithms;
using Dispartior.Data;

namespace SampleServer.Algorithms
{
    public class AlgoOne : IAlgorithm
	{
		public IAlgorithmRunner AlgorithmRunner { get; set; }

		public IDataSource<int> DataSource { get; set; } 

        public AlgoOne()
        {
		}
			
		public void Run()
		{
//			while (DataSource.HasNext())
//			{
//				var data = DataSource.GetNext();
//
//				Console.WriteLine("Doing: " + data);
//			}

			Console.WriteLine("AlgoOne ran.");
		}

    }
}


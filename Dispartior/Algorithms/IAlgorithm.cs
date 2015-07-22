using System;
using Dispartior.Data;

namespace Dispartior.Algorithms
{
    public interface IAlgorithm
    {
		IDataSourceConfiguration DataSourceConfiguration { get; set; }

		IAlgorithmRunner AlgorithmRunner { get; set; }

		DataSourceFactory DataSourceFactory { get; set; }

		void Run();
    }
}


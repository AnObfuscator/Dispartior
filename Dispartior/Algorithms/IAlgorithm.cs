using System;
using Dispartior.Data;
using System.Collections.Generic;

namespace Dispartior.Algorithms
{
    public interface IAlgorithm
    {
        IDataSourceConfiguration DataSourceConfiguration { get; set; }

        IAlgorithmRunner AlgorithmRunner { get; set; }

        DataSourceFactory DataSourceFactory { get; set; }

        void Run(IDictionary<string, string> parameters);
    }
}


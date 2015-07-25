using System;
using Dispartior.Data;
using System.Collections.Generic;

namespace Dispartior.Algorithms
{
    public interface IAlgorithm
    {
        IDataSetDefinition DataSetDefinition { get; set; }

        IAlgorithmRunner AlgorithmRunner { get; set; }

        DataSource DataSource { get; set; }

        IDataSetDefinition Run(IDictionary<string, string> parameters);
    }
}


using System;
using System.Collections.Generic;

namespace Dispartior.Data
{
    public interface IDataPartitioner
    {
        IEnumerable<IDataSetDefinition> Partition(IDataSetDefinition dataSetDefinition, int partitionSize);
    }
}


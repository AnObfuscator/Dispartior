using System;
using System.Collections.Generic;

namespace Dispartior.Data.Database
{
    public class DatabasePartitioner : IDataPartitioner
    {
        public DatabasePartitioner(DatabaseDataSetDefinition databaseConfiguration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataSetDefinition> Partition(IDataSetDefinition dataSetDefinition, int partitionSize)
        {
            throw new NotImplementedException();
        }

    }
}


using System;
using System.Collections.Generic;

namespace Dispartior.Data.Database
{
    public class DatabasePartitioner : IDataPartitioner
    {
        public DatabasePartitioner(DatabaseConfiguration databaseConfiguration)
        {
            throw new NotImplementedException();
        }

        public List<IDataSourceConfiguration> Partition(IDataSourceConfiguration dataSourceConfiguration, int partitionSize)
        {
            throw new NotImplementedException();
        }

    }
}


using System;
using System.Collections.Generic;
using System.Numerics;

namespace Dispartior.Data.Database
{
    public class DatabaseDataSet<T> : IDataSet<T>
    {
        private readonly IDataSourceDriver driver;

        private readonly IEntrySerialization<T> serialization;

        private readonly long maxEntriesInMemory;

        private readonly IEnumerator<T> entries;

        private bool hasNext;

        public BigInteger Count { get; private set; }


        public DatabaseDataSet(DatabaseDataSetDefinition databaseConfiguration, IDataSourceDriver driver, IEntrySerialization<T> entrySerialization)
        {
            serialization = entrySerialization;
            Count = databaseConfiguration.PartitionSize;
            maxEntriesInMemory = databaseConfiguration.MemoryLimit;
            this.driver = driver;

            entries = driver.GetResultSet<T>(databaseConfiguration.Query, serialization);
        }

        public bool HasNext()
        {
            return hasNext;
        }

        public T GetNext()
        {
            if (!HasNext())
            {
                throw new IndexOutOfRangeException();
            }
           
            var toReturn = entries.Current;
            hasNext = entries.MoveNext();
            return toReturn;
        }
            
    }
}


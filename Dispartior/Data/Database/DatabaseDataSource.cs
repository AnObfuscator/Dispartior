using System;

namespace Dispartior.Data.Database
{
    public class DatabaseDataSource<T> : IDataSource<T>
    {
        private readonly IEntryDeserializer<T> deserializer;

        public DatabaseDataSource(DatabaseConfiguration databaseConfiguration, IEntryDeserializer<T> entryDeserializer)
        {
            deserializer = entryDeserializer;
        }

        public bool HasNext()
        {
            throw new NotImplementedException();
        }

        public T GetNext()
        {
            throw new NotImplementedException();
        }

    }
}


using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Dispartior.Data.Range;
using Dispartior.Data.Database;

namespace Dispartior.Data
{
    public class DataSourceFactory
    {
        private readonly IDictionary<Type, Type> customDataTypes = new ConcurrentDictionary<Type, Type>();
        private readonly IDictionary<string, Type> customDataPartitioners = new ConcurrentDictionary<string, Type>();


        public DataSourceFactory()
        {
        }

        public void RegisterDataType<T,E>() where E : IEntryDeserializer<T>, new()
        {
            var dataType = typeof(T); 
            customDataTypes.Add(dataType, typeof(E));
        }

        public void RegisterDataPartitioner<P>() where P : IDataPartitioner, new()
        {
            var partitionerType = typeof(P); 
            customDataPartitioners.Add(partitionerType.Name, partitionerType);
        }

        public IEntryDeserializer<T> GetDeserializer<T>()
        {
            if (customDataTypes.ContainsKey(typeof(T)))
            {
                var deserializerType = customDataTypes[typeof(T)];
                return (IEntryDeserializer<T>)Activator.CreateInstance(deserializerType);
            }

            if (Default.Deserializers.Contains(typeof(T)))
            {
                var deserializerType = Default.Deserializers.GetFor(typeof(T));
                return (IEntryDeserializer<T>)Activator.CreateInstance(deserializerType);
            }

            return null;
        }

        public IDataPartitioner GetDataPartitioner(IDataSourceConfiguration dataSourceConfiguration)
        {
            var typeName = dataSourceConfiguration.TypeName;
            if (customDataPartitioners.ContainsKey(typeName))
            {
                var partitionerType = customDataPartitioners[typeName];
                return (IDataPartitioner)Activator.CreateInstance(partitionerType);
            }

            if (typeof(RangeConfiguration).Name.Equals(typeName))
            {
                return new RangePartitioner();
            }

            if (typeof(DatabaseConfiguration).Name.Equals(typeName))
            {
                var databaseConfiguration = dataSourceConfiguration as DatabaseConfiguration;
                return new DatabasePartitioner(databaseConfiguration);
            }

            return null;
        }

        public IDataSource<T> GetDataSource<T>(IDataSourceConfiguration dataSourceConfiguration)
        {
            var rangeDefinition = dataSourceConfiguration as RangeConfiguration;
            if (rangeDefinition != null)
            {
                return (IDataSource<T>)new RangeDataSource(rangeDefinition);
            }

            var databaseConfiguration = dataSourceConfiguration as DatabaseConfiguration;
            if (databaseConfiguration != null)
            {
                var entryDeserializer = GetDeserializer<T>();
                return new DatabaseDataSource<T>(databaseConfiguration, entryDeserializer);
            }
				
            return null;
        }

    }
}
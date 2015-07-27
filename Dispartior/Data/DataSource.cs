using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Dispartior.Data.Range;
using Dispartior.Data.Database;
using Dispartior.Configuration;

namespace Dispartior.Data
{
    public class DataSource
    {
        private static readonly IDictionary<string, Type> customDrivers = new ConcurrentDictionary<string, Type>();

        private readonly IDictionary<Type, Type> customDataTypes = new ConcurrentDictionary<Type, Type>();
        private readonly IDictionary<string, Type> customDataPartitioners = new ConcurrentDictionary<string, Type>();
 
        private readonly IDataSourceDriver driver;

        public static void RegisterDriver<D>() where D : IDataSourceDriver
        {
            customDrivers.Add(typeof(D).ToString(), typeof(D));
        }

        public DataSource(DatabaseConfiguration databaseConfiguration)
        {
            var driverName = databaseConfiguration.Driver;
            Type driverType;
            if (customDrivers.ContainsKey(driverName))
            {
                driverType = customDrivers[driverName];
            }
            else if (Default.Driver.ExistsFor(driverName))
            {
                driverType = Default.Driver.Named(driverName);
            }
            else
            {
                throw new Exception("Cannot find DataSourceDriver with name: {0}", driverName);
            }

            driver = (IDataSourceDriver)Activator.CreateInstance(driverType);
            driver.ConnectionString = databaseConfiguration.ConnectionString;
        }

        public void RegisterDataType<T,E>() where E : IEntrySerialization<T>, new()
        {
            var dataType = typeof(T); 
            customDataTypes.Add(dataType, typeof(E));
        }

        public void RegisterDataPartitioner<P, D>() where P : IDataPartitioner, new() where D : IDataSetDefinition
        {
            customDataPartitioners.Add(typeof(D).Name, typeof(P));
        }

        public IEntrySerialization<T> GetEntrySerialization<T>()
        {
            if (customDataTypes.ContainsKey(typeof(T)))
            {
                var deserializerType = customDataTypes[typeof(T)];
                return (IEntrySerialization<T>)Activator.CreateInstance(deserializerType);
            }

            if (Default.Serialization.Contains(typeof(T)))
            {
                var deserializerType = Default.Serialization.GetFor(typeof(T));
                return (IEntrySerialization<T>)Activator.CreateInstance(deserializerType);
            }

            return null;
        }

        public IDataPartitioner GetDataPartitioner(IDataSetDefinition dataSetDefinition)
        {
            var typeName = dataSetDefinition.TypeName;
            if (customDataPartitioners.ContainsKey(typeName))
            {
                var partitionerType = customDataPartitioners[typeName];
                return (IDataPartitioner)Activator.CreateInstance(partitionerType);
            }

            if (typeof(RangeDataSetDefinition).FullName.Equals(typeName))
            {
                return new RangePartitioner();
            }

            if (typeof(DatabaseDataSetDefinition).FullName.Equals(typeName))
            {
                var databaseConfiguration = dataSetDefinition as DatabaseDataSetDefinition;
                return new DatabasePartitioner(databaseConfiguration);
            }

            return null;
        }

        public IDataSet<T> GetDataSet<T>(IDataSetDefinition dataSetDefinition)
        {
            var rangeDefinition = dataSetDefinition as RangeDataSetDefinition;
            if (rangeDefinition != null)
            {
                return (IDataSet<T>)new RangeDataSet(rangeDefinition);
            }

            var databaseConfiguration = dataSetDefinition as DatabaseDataSetDefinition;
            if (databaseConfiguration != null)
            {
                var entrySerialization = GetEntrySerialization<T>();
                return new DatabaseDataSet<T>(databaseConfiguration, driver, entrySerialization);
            }
				
            return null;
        }

        public IDataSetDefinition SaveResult<T>(T result)
        {
            var entrySerialization = GetEntrySerialization<T>();
            return driver.Persist<T>(result, entrySerialization);
        }

        public IDataSetDefinition SaveResults<T>(IList<T> results)
        {
            var entrySerialization = GetEntrySerialization<T>();
            return driver.Persist<T>(results, entrySerialization);
        }
    }
}
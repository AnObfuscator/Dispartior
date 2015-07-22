using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Dispartior.Data.Range;
using Dispartior.Data.Database;
using Dispartior.Data.Types;

namespace Dispartior.Data
{
	public class DataSourceFactory
	{
		private readonly IDictionary<Type, Type> dataTypes = new ConcurrentDictionary<Type, Type>();
		private readonly IDictionary<string, Type> dataPartitioners = new ConcurrentDictionary<string, Type>();


		public DataSourceFactory()
		{
//			RegisterDataType<int, IntDeserializer, RangePartitioner>();
		}
			
		public void RegisterDataType<T,E,P>() where E : IEntryDeserializer<T>, new() where P : IDataPartitioner
		{
			var dataType = typeof(T); 
			dataTypes.Add(dataType, typeof(E));
			dataPartitioners.Add(dataType.Name, typeof(P));
		}

		public IEntryDeserializer<T> GetDeserializer<T>()
		{
			if (dataTypes.ContainsKey(typeof(T)))
			{
				var deserializerType = dataTypes[typeof(T)];
				return (IEntryDeserializer<T>)Activator.CreateInstance(deserializerType);
			}

			return null;
		}

		public IDataPartitioner GetDataPartitioner(IDataSourceConfiguration dataSourceConfiguration)
		{
			var typeName = dataSourceConfiguration.TypeName;

			if (typeof(RangeConfiguration).Name.Equals(typeName))
			{
				return new RangePartitioner();
			}

			if (dataPartitioners.ContainsKey(typeName))
			{
				var partitionerType = dataPartitioners[typeName];
				return (IDataPartitioner)Activator.CreateInstance(partitionerType);
			}

			return null;
		}

		public IDataSource<T> GetDataSource<T>(IDataSourceConfiguration dataSourceConfiguration)
		{
			var rangeDefinition = dataSourceConfiguration as RangeConfiguration;
			if (rangeDefinition != null)
			{
				return new RangeDataSource(rangeDefinition) as IDataSource<T>;
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


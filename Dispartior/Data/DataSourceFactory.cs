using System;

namespace Dispartior.Data
{
	public class DataSourceFactory
	{
		public DataSourceFactory()
		{
		}


		public void RegisterDataType<T,E>() where E : IEntryDeserializer<T>
		{
		}

		public IEntryDeserializer<T> GetDeserializer<T>()
		{
			return null;
		}

		public IDataSource<T> GetDataSource<T>()
		{
			return null;
		}

	}
}


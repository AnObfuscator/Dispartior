using System;

namespace Dispartior.Data
{
	/*
	 * Interface to make DataSource type system and serialization work
	 */
	public interface IDataSourceConfiguration
	{
		string TypeName { get; set; }
	}
}


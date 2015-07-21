using System;

namespace Dispartior.Data
{
	public interface IEntryDeserializer<T>
	{
		T Deserialize(string entry);
	}
}


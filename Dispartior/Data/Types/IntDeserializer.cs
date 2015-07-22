using System;

namespace Dispartior.Data.Types
{
	public class IntDeserializer : IEntryDeserializer<int>
	{
		public int Deserialize(string entry)
		{
			int result;
			if (int.TryParse(entry, out result))
			{
				return result;
			}

			return 0; // TODO better to throw exception, probably
		}
	}
}


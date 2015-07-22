using System;
using System.Collections.Generic;

namespace Dispartior.Data
{
	public interface IDataPartitioner
	{
		List<IDataSourceConfiguration> Partition(IDataSourceConfiguration dataSourceConfiguration, int partitionSize);
	}
}


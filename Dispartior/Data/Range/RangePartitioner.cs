using System;
using System.Collections.Generic;

namespace Dispartior.Data.Range
{
	public class RangePartitioner : IDataPartitioner
	{
		public RangePartitioner()
		{
		}
			
		public List<IDataSourceConfiguration> Partition(IDataSourceConfiguration dataSourceConfiguration, int partitionCount)
		{
			var partitions = new List<IDataSourceConfiguration>();
			var rangeConfig = dataSourceConfiguration as RangeConfiguration;
			if (rangeConfig == null)
			{
				return partitions;
			}
				
			var numberOfEntries = (rangeConfig.End - rangeConfig.Start) / rangeConfig.StepSize;

			var partitionSize = numberOfEntries / partitionCount;
			var remainder = (numberOfEntries % partitionCount);

			for (int i = 0; i < partitionCount-1; i++)
			{
				var newPartition = BuildNewPartition(rangeConfig, i, partitionSize, partitionSize);
				partitions.Add(newPartition);
			}

			// account for ranges that don't divide evenly into their step sizes
			var finalRange = remainder != 0 ? remainder : partitionSize;
			var finalPartition = BuildNewPartition(rangeConfig, partitionCount-1, partitionSize, finalRange);
			partitions.Add(finalPartition);

			return partitions;
		}

		public RangeConfiguration BuildNewPartition(RangeConfiguration rangeConfig, int offset, int partitionSize, int range)
		{
			var newPartition = new RangeConfiguration();

			var startOffset = offset * partitionSize;
			newPartition.Start = startOffset + rangeConfig.Start;
			newPartition.End = newPartition.Start + range;
			newPartition.StepSize = rangeConfig.StepSize;

			return newPartition;
		}

	}
}


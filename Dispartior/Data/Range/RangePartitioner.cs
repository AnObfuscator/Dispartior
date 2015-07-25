using System;
using System.Collections.Generic;
using System.Numerics;

namespace Dispartior.Data.Range
{
    public class RangePartitioner : IDataPartitioner
    {
        public RangePartitioner()
        {
        }

        public IEnumerable<IDataSetDefinition> Partition(IDataSetDefinition dataSetDefinition, int partitionCount)
        {
            var partitions = new List<IDataSetDefinition>();
            var rangeConfig = dataSetDefinition as RangeDataSetDefinition;
            if (rangeConfig == null)
            {
                return partitions;
            }
				
            var numberOfEntries = (rangeConfig.End - rangeConfig.Start) / rangeConfig.StepSize;

            var partitionSize = numberOfEntries / partitionCount;
            var remainder = (numberOfEntries % partitionCount);

            for (BigInteger i = 0; i < partitionCount - 1; i++)
            {
                var newPartition = BuildNewPartition(rangeConfig, i, partitionSize, partitionSize);
                partitions.Add(newPartition);
            }

            // account for ranges that don't divide evenly into their step sizes
            var finalRange = remainder != 0 ? remainder : partitionSize;
            var finalPartition = BuildNewPartition(rangeConfig, partitionCount - 1, partitionSize, finalRange);
            partitions.Add(finalPartition);

            return partitions;
        }

        public RangeDataSetDefinition BuildNewPartition(RangeDataSetDefinition rangeConfig, BigInteger offset, BigInteger partitionSize, BigInteger range)
        {
            var newPartition = new RangeDataSetDefinition();

            var startOffset = offset * partitionSize;
            newPartition.Start = startOffset + rangeConfig.Start;
            newPartition.End = newPartition.Start + range;
            newPartition.StepSize = rangeConfig.StepSize;

            return newPartition;
        }

    }
}


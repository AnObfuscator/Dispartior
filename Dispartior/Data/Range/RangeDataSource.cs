using System;
using System.Numerics;

namespace Dispartior.Data.Range
{
    public class RangeDataSource : IDataSource<BigInteger>
    {
        private readonly BigInteger start;
        private readonly BigInteger end;
        private readonly BigInteger stepSize;

        private BigInteger next;

        public RangeDataSource(RangeConfiguration range)
        {
            start = range.Start;
            end = range.End;
            stepSize = range.StepSize;

            next = start;
        }

        public bool HasNext()
        {
            return next <= end;
        }

        public BigInteger GetNext()
        {
            if (!HasNext())
            {
                throw new IndexOutOfRangeException();
            }
            
            var toReturn = next;
            next += stepSize;
            return toReturn;
        }

    }
}


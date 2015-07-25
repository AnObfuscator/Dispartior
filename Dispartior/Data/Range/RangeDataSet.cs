using System;
using System.Numerics;

namespace Dispartior.Data.Range
{
    public class RangeDataSet : IDataSet<BigInteger>
    {
        private readonly BigInteger start;
        private readonly BigInteger end;
        private readonly BigInteger stepSize;

        private BigInteger next;

        public BigInteger Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RangeDataSet(RangeDataSetDefinition range)
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

        public void SaveResult(object result)
        {
            throw new NotImplementedException();
        }
    }
}


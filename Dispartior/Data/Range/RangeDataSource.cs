using System;

namespace Dispartior.Data.Range
{
    public class RangeDataSource : IDataSource<int>
    {
        private readonly int start;
        private readonly int end;
        private readonly int stepSize;

        private int next;

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

        public int GetNext()
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


using System;

namespace Dispartior.Data.Range
{
	public class RangeDataSource : IDataSource<int>
	{
		private readonly int start;
		private readonly int end;
		private readonly int stepSize;

		private int next;

		public RangeDataSource(Range range)
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
			var toReturn = next;
			next++;
			return toReturn;
		}

	}
}


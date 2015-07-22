using System;

namespace Dispartior.Data.Range
{
	public class RangeConfiguration : IDataSourceConfiguration
	{
		private static readonly string typeName = typeof(RangeConfiguration).Name;

		public string TypeName 
		{ 
			get
			{
				return typeName;
			}
			set { } 
		}

		public int Start { get; set; }

		public int End { get; set; }

		public int StepSize { get; set; }
	}
}


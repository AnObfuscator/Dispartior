using System;
using System.Numerics;

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

        public BigInteger Start { get; set; }

        public BigInteger End { get; set; }

        public BigInteger StepSize { get; set; }
	}
}


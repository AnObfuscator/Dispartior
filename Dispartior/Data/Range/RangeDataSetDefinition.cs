using System;
using System.Numerics;

namespace Dispartior.Data.Range
{
    public class RangeDataSetDefinition : IDataSetDefinition
    {
        private static readonly string typeName = typeof(RangeDataSetDefinition).FullName;

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


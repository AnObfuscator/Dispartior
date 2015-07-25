using System.Numerics;

namespace Dispartior.Data.Database
{
    public partial class DatabaseDataSetDefinition : IDataSetDefinition
    {
        public string TypeName { get; set; }

        public BigInteger PartitionSize
        {
            get;
            set;
        }

        public long MemoryLimit
        {
            get;
            set;
        }

        public string Query
        {
            get;
            set;
        }
    }
}


using System.Numerics;

namespace Dispartior.Data.Database
{
    public partial class DatabaseDataSetDefinition : IDataSetDefinition
    {
        private static readonly string typeName = typeof(DatabaseDataSetDefinition).FullName;

        public string TypeName
        { 
            get
            {
                return typeName;
            }
            set { } 
        }
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


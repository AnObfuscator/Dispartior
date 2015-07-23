using Dispartior.Data;
using System.Collections.Generic;

namespace Dispartior.Messaging.Messages.Commands
{
    public class Computation : BaseMessage
    {
        public string UUID { get; set; }

        public string Worker { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public string Algorithm { get; set; }

        public IDataSourceConfiguration DataSourceConfiguration { get; set; }

        public int PartitionSize { get; set; }
    }
}


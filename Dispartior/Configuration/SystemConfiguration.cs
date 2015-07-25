using System;
using System.Collections.Generic;

namespace Dispartior.Configuration
{
    public class SystemConfiguration
    {
        public IDictionary<string, ServerConfiguration> Servers { get; set; }

        public IDictionary<string, DatabaseConfiguration> Databases { get; set; }
    }
}


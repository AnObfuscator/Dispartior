using System;
using Dispartior.Data;

namespace Dispartior.Configuration
{
    public class DatabaseConfiguration
    {
        public string Name { get; set; }

        public string Driver { get; set; }

        public string ConnectionString { get; set; }
    }
}
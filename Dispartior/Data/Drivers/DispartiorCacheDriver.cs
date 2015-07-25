using System;
using System.Collections.Generic;

namespace Dispartior.Data.Drivers
{
    public class DispartiorCacheDriver : IDataSourceDriver
    {
        public string ConnectionString { get; set; }

        public DispartiorCacheDriver()
        {
        }
            
        public IEnumerator<T> GetResultSet<T>(string query, IEntrySerialization<T> serialization = null)
        {
            Console.WriteLine("Running query " + query);
            return new List<T>().GetEnumerator();
        }

        public IDataSetDefinition Persist<T>(T data, IEntrySerialization<T> entrySerialization = null)
        {
            Console.WriteLine("Saving data " + data.ToString());

            return null;
        }

        public IDataSetDefinition Persist<T>(IList<T> data, IEntrySerialization<T> entrySerialization = null)
        {
            Console.WriteLine("Saving data: " + data.ToString());

            return null;
        }
         

    }
}


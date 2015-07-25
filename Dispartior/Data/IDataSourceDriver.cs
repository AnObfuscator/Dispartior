using System;
using System.Collections.Generic;

namespace Dispartior.Data
{
    public interface IDataSourceDriver
    {
        string ConnectionString { get; set; }

        IEnumerator<T> GetResultSet<T>(string query, IEntrySerialization<T> serialization = null);

        IDataSetDefinition Persist<T>(T data, IEntrySerialization<T> entrySerialization = null);

        IDataSetDefinition Persist<T>(IList<T> data, IEntrySerialization<T> entrySerialization = null);
    }
}


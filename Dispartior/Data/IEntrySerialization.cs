using System;

namespace Dispartior.Data
{
    public interface IEntrySerialization<T>
    {
        T Deserialize(string entry);

        string Serialize(T entry);
    }
}


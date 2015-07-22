using System;

namespace Dispartior.Data
{
    public interface IDataSource<T>
    {
        bool HasNext();

        T GetNext();
    }
}


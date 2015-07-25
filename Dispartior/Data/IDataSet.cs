using System;
using System.Numerics;
using System.Collections.Generic;

namespace Dispartior.Data
{
    public interface IDataSet<T>
    {
        BigInteger Count { get; }

        bool HasNext();

        T GetNext();

//        T this[BigInteger i] { get; set; }

//        void AddAll(IEnumerable<T> toAdd);
    }
}


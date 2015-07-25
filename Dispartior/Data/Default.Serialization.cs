using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Numerics;

namespace Dispartior.Data
{
    public static partial class Default
    {
        public static class Serialization
        {
            public static bool Contains(Type type)
            {
                return defaultSerializations.ContainsKey(type);
            }

            public static Type GetFor(Type type)
            {
                return defaultSerializations[type];
            }

            private static readonly IDictionary<Type, Type> defaultSerializations = new Dictionary<Type, Type>
            {
//                { typeof(long), typeof(LongSerialization) },
//                { typeof(BigInteger), typeof(BigIntSerialization) },
//
//                { typeof(double), typeof(DoubleSerialization) },
//                { typeof(decimal), typeof(DecimalSerialization) }

            };

            public class LongSerialization : IEntrySerialization<long>
            {
                public string Serialize(long entry)
                {
                    throw new NotImplementedException();
                }

                public long Deserialize(string entry)
                {
                    // TODO better to throw exception, probably
                    long result;
                    return long.TryParse(entry, out result) ? result : 0;
                }
            }

            public class BigIntSerialization : IEntrySerialization<BigInteger>
            {
                public string Serialize(BigInteger entry)
                {
                    throw new NotImplementedException();
                }

                public BigInteger Deserialize(string entry)
                {
                    BigInteger result;
                    return BigInteger.TryParse(entry, out result) ? result : BigInteger.Zero;
                }
            }

            public class DoubleSerialization : IEntrySerialization<double>
            {
                public string Serialize(double entry)
                {
                    throw new NotImplementedException();
                }

                public double Deserialize(string entry)
                {
                    double result;
                    return double.TryParse(entry, out result) ? result : double.NaN;
                }
            }

            public class DecimalSerialization : IEntrySerialization<decimal>
            {
                public string Serialize(decimal entry)
                {
                    throw new NotImplementedException();
                }

                public decimal Deserialize(string entry)
                {
                    // TODO better to throw exception, probably
                    decimal result;
                    return decimal.TryParse(entry, out result) ? result : decimal.Zero;
                }
            }
        }
    }
}


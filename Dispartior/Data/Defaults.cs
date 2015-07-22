using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Numerics;

namespace Dispartior.Data
{
    public static class Default
    {
        public static class Deserializers
        {
            public static bool Contains(Type type)
            {
                 return defaultDeserializers.ContainsKey(type);
            }

            public static Type GetFor(Type type)
            {
                return defaultDeserializers[type];
            }

            private static readonly IDictionary<Type, Type> defaultDeserializers = new Dictionary<Type, Type>
            {
                { typeof(long), typeof(LongDeserializer) },
                { typeof(BigInteger), typeof(BigIntDeserializer) },

                { typeof(double), typeof(DoubleDeserializer) },
                { typeof(decimal), typeof(DecimalDeserializer) }

            };

            public class LongDeserializer : IEntryDeserializer<long>
            {
                public long Deserialize(string entry)
                {
                    // TODO better to throw exception, probably
                    long result;
                    return long.TryParse(entry, out result) ? result : 0;
                }
            }

            public class BigIntDeserializer : IEntryDeserializer<BigInteger>
            {
                public BigInteger Deserialize(string entry)
                {
                    BigInteger result;
                    return BigInteger.TryParse(entry, out result) ? result : BigInteger.Zero;
                }
            }

            public class DoubleDeserializer : IEntryDeserializer<double>
            {
                public double Deserialize(string entry)
                {
                    double result;
                    return double.TryParse(entry, out result) ? result : double.NaN;
                }
            }

            public class DecimalDeserializer : IEntryDeserializer<decimal>
            {
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


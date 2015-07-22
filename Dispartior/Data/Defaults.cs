using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Dispartior.Data.Types
{
    public static class Deserializers
    {
        private static readonly IDictionary<Type, Type> defaultDeserializers = new ConcurrentDictionary<Type, Type>
        {
            { typeof(int), typeof(IntDeserializer) }
        }

        public class IntDeserializer : IEntryDeserializer<int>
        {
            public int Deserialize(string entry)
            {
                int result;
                if (int.TryParse(entry, out result))
                {
                    return result;
                }

                return 0; // TODO better to throw exception, probably
            }
        }
    }
}


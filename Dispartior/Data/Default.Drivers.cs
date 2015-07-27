using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Dispartior.Data.Drivers;

namespace Dispartior.Data
{
    public static partial class Default
    {
        public static class Driver
        {
            private static IDictionary<string, Type> drivers = new Dictionary<string, Type>
            {
                { typeof(DispartiorCacheDriver).FullName, typeof(DispartiorCacheDriver) }
            };

            public static bool ExistsFor(string driverName)
            {
                return drivers.ContainsKey(driverName);
            }

            public static Type Named(string driverName)
            {
                return drivers[driverName];
            }
        }
    }
}
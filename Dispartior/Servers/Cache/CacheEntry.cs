using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;

namespace Dispartior.Servers.Cache
{
    public class CacheEntry//<T> //: IQueryable<T>
    {
        private ReaderWriterLockSlim entryLock = new ReaderWriterLockSlim();

        private readonly List<Item> items = new List<Item>();

        public CacheEntry()
        {
            //Items.
        }

        public void Add(string data)
        {
            var newItem = new Item { Data = data };
            entryLock.EnterReadLock();
            try
            {
                items.Add(newItem);
            }
            finally
            {
                entryLock.ExitWriteLock();
            }
        }

        public void AddAll(IEnumerable<string> data)
        {
            var newItems = data.Select(d => new Item { Data = d });
            entryLock.EnterWriteLock();
            try
            {
                items.AddRange(newItems);
            }
            finally
            {
                entryLock.ExitWriteLock();
            }
        }

        public IEnumerable<string> GetAll()
        {
            entryLock.EnterReadLock();
            try
            {
                return items.Select(i => i.Data);
            }
            finally
            {
                entryLock.ExitReadLock();
            }
        }

        public IEnumerable<string> GetPartition(long partition)
        {
            entryLock.EnterReadLock();
            try
            {
//                var result = 
                return null;
            }
            finally
            {
                entryLock.ExitReadLock();
            }
        }

        private class Item
        {
            public long Partition { get; set; }

            public string Data { get; set; }

        }
       
    }
}


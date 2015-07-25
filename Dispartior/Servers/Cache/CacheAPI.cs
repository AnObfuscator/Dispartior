using System;
using Nancy;
using Nancy.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dispartior.Servers.Cache
{
    // TODO Learn how to deserialize list<byte[]> data
    public class CacheAPI : NancyModule
    {
        private static readonly ConcurrentDictionary<string, CacheEntry> cache = new ConcurrentDictionary<string, CacheEntry>();

        public CacheAPI()
        {
            Post["/save/{id}"] = parameters =>
            {
                    var key = parameters.id;

//                    var bodySize = Request.Body.Length;
//                    var data = new byte[bodySize];
//                    Request.Body.Read(data, 0, (int)bodySize); // TODO WTF C#
                    var data = Request.Body.AsString();

                    CacheEntry entry;
                    if (!cache.TryGetValue(key, out entry))
                    {
                        var newEntry = new CacheEntry();
                        entry = cache.GetOrAdd(key, newEntry);
                    }
                    entry.Add(data);
                    return HttpStatusCode.Created;
            };

            Post["/save/all/{id}"] = parameters =>
                {
                    var key = parameters.id;
                    var body = Request.Body.AsString();
                    var data = JsonConvert.DeserializeObject<IEnumerable<string>>(body);

                    CacheEntry entry;
                    if (!cache.TryGetValue(key, out entry))
                    {
                        var newEntry = new CacheEntry();
                        entry = cache.GetOrAdd(key, newEntry);
                    }
                    entry.AddAll(data);
                    return HttpStatusCode.Created;
                };

            Get["/retrieve/{id}"] = parameters =>
            {
                    //TODO extract query params
                    var key = parameters.id;

                    CacheEntry entry;
                    if (!cache.TryGetValue(key, out entry))
                    {
                        return HttpStatusCode.NotFound;
                    }

                    // TODO extract data
                    return HttpStatusCode.OK;
            };

            Get["/retrieve/{id}/{index}"] = parameters =>
            {
                    //TODO extract query params
                    var key = parameters.id;

                    CacheEntry entry;
                    if (!cache.TryGetValue(key, out entry))
                    {
                        return HttpStatusCode.NotFound;
                    }

                    // TODO extract data
                    return HttpStatusCode.OK;
            };

            Get["/retrieve/{id}/from/{start}/to/{end}"] = parameters =>
            {
                    //TODO extract query params
                    var key = parameters.id;

                    CacheEntry entry;
                    if (!cache.TryGetValue(key, out entry))
                    {
                        return HttpStatusCode.NotFound;
                    }

                    // TODO extract data
                    return HttpStatusCode.OK;
            };

            Delete["/remove/{id}"] = parameters =>
            {
                    var key = parameters.id;

                    CacheEntry entry;
                    cache.TryRemove(key, out entry);

                    return HttpStatusCode.NoContent;
            };

        }
    }
}


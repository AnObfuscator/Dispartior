using System;
using Newtonsoft.Json;

namespace Dispartior.Messaging.Messages
{
    public abstract class BaseMessage
    {
        private static readonly DataSetDefinitionJsonConverter dataSourceConfigConverter = new DataSetDefinitionJsonConverter();

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return Serialize();
        }

        public static T Deserialize<T>(string json)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(dataSourceConfigConverter);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}


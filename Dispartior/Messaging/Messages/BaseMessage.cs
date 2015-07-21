using System;
using Newtonsoft.Json;

namespace Dispartior.Messaging.Messages
{
    public abstract class BaseMessage
    {
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
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}


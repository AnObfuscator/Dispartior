using System;
using Newtonsoft.Json;
using Dispartior.Data;
using Newtonsoft.Json.Linq;
using Dispartior.Data.Range;
using Dispartior.Data.Database;

namespace Dispartior.Messaging
{
	public class DataSourceConfigurationJsonConverter : JsonConverter
	{
		private Type interfaceType = typeof(IDataSourceConfiguration); 

		public override bool CanConvert(Type objectType)
		{
			return interfaceType.Equals(objectType);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var target = serializer.Deserialize<JObject>(reader);
			objectType = DetermineConcreteType(target);
			var result = Activator.CreateInstance(objectType);
			serializer.Populate(target.CreateReader(), result);
			return result;
		}

		private Type DetermineConcreteType(JObject target)
		{
			// TODO fix this...
//			var typeName = target.GetValue("TypeName");
			return typeof(RangeConfiguration);
		}
	}
}


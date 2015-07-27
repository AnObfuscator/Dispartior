using System;
using Newtonsoft.Json;
using Dispartior.Data;
using Newtonsoft.Json.Linq;

namespace Dispartior.Messaging
{
    public class DataSetDefinitionJsonConverter : JsonConverter
    {
        private Type interfaceType = typeof(IDataSetDefinition);

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
            var result = ConstructConcreteType(target);
            if (result != null)
            {
                serializer.Populate(target.CreateReader(), result);
            }

            return result;
        }

        private object ConstructConcreteType(JObject target)
        {
            // TODO make this better
            try
            {
                var typeName = target.GetValue("TypeName").ToString();
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var instance =asm.CreateInstance(typeName);
                    if (instance != null)
                    {
                        return instance;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating DataSetDefinition: {0}", ex.Message);
            }
            return null;
        }
    }
}


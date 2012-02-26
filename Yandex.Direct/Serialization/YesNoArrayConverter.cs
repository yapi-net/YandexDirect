using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct.Serialization
{
    public class YesNoArrayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                if (value is bool)
                {
                    serializer.Serialize(writer, new[] { (YesNo)(bool)value });
                }
                else if (value is YesNo)
                {
                    serializer.Serialize(writer, new[] { (YesNo)value });
                }
                else
                    throw new NotSupportedException("Unsupported value type. Supported types are System.Boolean and YesNo.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not implemented.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?) || objectType == typeof(YesNo) || objectType == typeof(YesNo?);
        }
    }
}

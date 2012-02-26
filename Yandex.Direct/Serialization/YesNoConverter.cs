using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct.Serialization
{
    public class YesNoConverter : JsonConverter
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
                    writer.WriteValue(new YesNo((bool)value).ToString());
                }
                else if (value is YesNo)
                {
                    writer.WriteValue(((YesNo)value).ToString());
                }
                else
                    throw new NotSupportedException("Unsupported value type. Supported types are System.Boolean and YesNo.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType == typeof(bool?) || objectType == typeof(YesNo?))
                {
                    return null;
                }
                else
                    throw new Exception("Cannot convert null value to System.Boolean.");
            }

            YesNo value;

            if (reader.TokenType == JsonToken.String && YesNo.TryParse(reader.Value.ToString(), out value))
            {
                if (objectType == typeof(bool) || objectType == typeof(bool?))
                {
                    return (bool)value;
                }
                else if (objectType == typeof(YesNo) || objectType == typeof(YesNo?))
                {
                    return value;
                }
                else
                    throw new NotSupportedException("Unsupported value type. Supported types are System.Boolean and YesNo.");
            }

            throw new Exception("Unexpected token. Expected Yes/No.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?) || objectType == typeof(YesNo) || objectType == typeof(YesNo?);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Yandex.Direct.Serialization
{
    public class YesNoBooleanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                bool boolValue = (bool)value;

                writer.WriteValue(boolValue ? "Yes" : "No");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType == typeof(bool?))
                {
                    return null;
                }
                else
                    throw new Exception("Cannot convert null value to System.Boolean.");
            }

            if (reader.TokenType == JsonToken.String)
            {
                switch (reader.Value.ToString().ToLowerInvariant())
                {
                    case "yes":
                        return true;

                    case "no":
                        return false;
                }
            }

            throw new Exception("Unexpected token. Expected Yes/No.");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }
    }
}

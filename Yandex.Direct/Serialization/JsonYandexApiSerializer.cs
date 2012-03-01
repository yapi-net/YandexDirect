using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Yandex.Direct.Serialization
{
    public class JsonYandexApiSerializer
    {
        private static readonly JsonSerializerSettings _jsonSettings;

        static JsonYandexApiSerializer()
        {
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" }, new StringEnumConverter() }
            };
        }

        public string Serialize(object objectGraph)
        {
            return JsonConvert.SerializeObject(objectGraph, Formatting.None, _jsonSettings);
        }

        public T Deserialize<T>(string serializedString)
        {
            return JsonConvert.DeserializeObject<T>(serializedString, _jsonSettings);
        }
    }
}
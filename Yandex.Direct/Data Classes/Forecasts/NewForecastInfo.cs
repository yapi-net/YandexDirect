using Newtonsoft.Json;

namespace Yandex.Direct
{
    [JsonObject]
    internal class CreateForecastInfo
    {
        /// <summary>ћассив, содержащий идентификаторы категорий яндекс. аталога, дл€ которых требуетс€ получить прогноз</summary>
        public int[] Categories { get; set; }

        /// <summary>ћассив фраз, дл€ которых требуетс€ получить прогноз (не более 100)</summary>
        public string[] Phrases { get; set; }

        /// <summary>ћассив, содержащий идентификаторы регионов дл€ составлени€ прогноза. ≈сли не задан, прогноз составл€етс€ по всем регионам</summary>
        [JsonProperty("GeoID")]
        public int[] GeoIds { get; set; }
    }
}
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public class ForecastBannerPhraseInfo
    {
        /// <summary>Текст фразы или идентификатор рубрики Яндекс.Каталога (в зависимости от параметра IsRubric)</summary>
        public string Phrase { get; set; }

        /// <summary>В качестве фразы используется рубрика Яндекс.Каталога — Yes/No. При значении Yes в параметре Phrase указан идентификатор рубрики</summary>
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsRubric { get; set; }

        /// <summary>Цена за клик на сайтах рекламной сети Яндекса</summary>
        public decimal ContextPrice { get; set; }

        /// <summary>Количество кликов по объявлению, найденному с помощью данной фразы на поиске Яндекса</summary>
        public int Clicks { get; set; }

        /// <summary>Количество показов объявления по данной фразе на поиске Яндекса</summary>
        public int Shows { get; set; }

        /// <summary>Стоимость гарантированного показа объявления</summary>
        public decimal Min { get; set; }

        /// <summary>Стоимость показа объявления на первом месте</summary>
        public decimal Max { get; set; }

        /// <summary>Стоимость показа объявления в спецразмещении</summary>
        public decimal PremiumMin { get; set; }

        /// <summary>Стоимость показа объявления на первом месте в спецразмещении</summary>
        public decimal PremiumMax { get; set; }

        /// <summary>Фраза имеет низкий CTR и может быть вскоре отключена</summary>
        [JsonProperty("LowCTRWarning")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsLowCtrWarning { get; set; }

        /// <summary>Фраза отключена на поиске за низкий CTR</summary>
        [JsonProperty("LowCTR")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsLowCtr { get; set; }

        /// <summary>Фраза отключена на сайтах рекламной сети Яндекса за низкий CTR</summary>
        [JsonProperty("ContextLowCTR")]
        [JsonConverter(typeof(YesNoBooleanConverter))]
        public bool IsContextLowCtr { get; set; }

        /// <summary>Прогнозируемое количество кликов при показе на первом месте</summary>
        public int FirstPlaceClicks { get; set; }

        /// <summary>Прогнозируемое количество кликов при показе в спецразмещении</summary>
        public int PremiumClicks { get; set; }

        /// <summary>Прогнозируемый CTR при показе на первом месте</summary>
        [JsonProperty("FirstPlaceCTR")]
        public decimal FirstPlaceCtr { get; set; }

        /// <summary>Прогнозируемый CTR при показе в спецразмещении</summary>
        [JsonProperty("PremiumCTR")]
        public decimal PremiumCtr { get; set; }

        // TODO Create CoverageInfo class
        //public CoverageInfo[] Coverage { get; set; }
        //public CoverageInfo[] ContextCoverage { get; set; }
    }
}
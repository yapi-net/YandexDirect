namespace Yandex.Direct
{
    public class ForecastCommonInfo
    {
        /// <summary>Идентификаторы регионов, перечисляемые через запятую</summary>
        public string Geo { get; set; }

        /// <summary>Прогнозируемое суммарное количество показов объявлений</summary>
        public int Shows { get; set; }

        /// <summary>Прогнозируемое суммарное количество кликов по объявлениям</summary>
        public int Clicks { get; set; }

        /// <summary>Прогнозируемое суммарное количество кликов по объявлениям на первом месте</summary>
        public int FirstPlaceClicks { get; set; }

        /// <summary>Прогнозируемое суммарное количество кликов по объявлениям в спецразмещении</summary>
        public int PremiumClicks { get; set; }

        /// <summary>Необходимый бюджет для прогнозируемого количества показов (кроме показов на первом месте и в спецразмещении)</summary>
        public decimal Min { get; set; }

        /// <summary>Необходимый бюджет для прогнозируемого количества показов на первом месте</summary>
        public decimal Max { get; set; }

        /// <summary>Необходимый бюджет для прогнозируемого количества показов в спецразмещении</summary>
        public decimal PremiumMin { get; set; }
    }
}
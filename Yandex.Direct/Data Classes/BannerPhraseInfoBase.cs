using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    [DebuggerDisplay("{Phrase}")]
    public abstract class BannerPhraseInfoBase
    {
        [JsonProperty("PhraseID")]
        public int PhraseId { get; set; }

        public string Phrase { get; set; }

        [JsonConverter(typeof(YesNoConverter))]
        public bool IsRubric { get; set; }

        public int? RubricId
        {
            get
            {
                return IsRubric ? int.Parse(Phrase, CultureInfo.InvariantCulture) : (int?)null;
            }
        }

        [JsonConverter(typeof(YesNoConverter))]
        public bool AutoBroker { get; set; }

        public AutoBudgetPriority? AutoBudgetPriority { get; set; }

        private decimal _price;
        private decimal? _contextPrice;

        public decimal? Price
        {
            get
            {
                return AutoBudgetPriority == null ? _price : (decimal?)null;
            }
            set
            {
                _price = (decimal)value;
            }
        }

        public decimal? ContextPrice
        {
            get
            {
                return AutoBudgetPriority == null ? _contextPrice : null;
            }
            set
            {
                _contextPrice = value;
            }
        }

        public PhraseUserParams UserParams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    public class BannerInfoWithPhrases<TPhrase> : BannerInfo
        where TPhrase : BannerPhraseInfo
    {
        public List<TPhrase> Phrases { get; set; }
    }
}

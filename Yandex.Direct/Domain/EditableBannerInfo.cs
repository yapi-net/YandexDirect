using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    public sealed class EditableBannerInfo : BannerInfoBase
    {
        public List<EditableBannerPhraseInfo> Phrases { get; set; }
    }
}

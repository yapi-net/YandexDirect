using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.Direct.Authentication;

namespace Yandex.Direct.Configuration
{
    public interface IYandexApiConfiguration
    {
        Uri ServiceUrl { get; set; }
        YandexApiLanguage Language { get; set; }
        IYandexApiAuthProvider AuthProvider { get; set; }
    }
}

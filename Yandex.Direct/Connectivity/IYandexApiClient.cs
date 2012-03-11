using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.Direct.Configuration;

namespace Yandex.Direct.Connectivity
{
    public interface IYandexApiClient
    {
        IYandexApiConfiguration Configuration { get; }

        T Invoke<T>(string method, object param = null, bool financeTokenRequired = false);
    }
}

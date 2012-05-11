using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    public partial class YandexDirectServiceStub : IYandexDirectService
    {
        public int PingApi()
        {
            return 1;
        }

        public void TestApiConnection()
        {
        }
    }
}

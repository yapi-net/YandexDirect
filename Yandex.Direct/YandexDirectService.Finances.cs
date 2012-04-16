using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectService
    {
        public void TransferMoney(TransferInfo from, TransferInfo to)
        {
            TransferMoney(new[] { from }, new[] { to });
        }

        public void TransferMoney(TransferInfo[] from, TransferInfo[] to)
        {
            var request = new { FromCampaigns = from, ToCampaigns = to };
            YandexApiClient.Invoke<int>(ApiMethod.TransferMoney, request, true);
        }
    }
}

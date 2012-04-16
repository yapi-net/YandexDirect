using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class ContractTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod, Description("Calling DeleteBanners with empty bannerIds should fail.")]
        public void GetBannersByIdsWithEmptyBannerIdsShouldFail()
        {
            var service = new YandexDirectService();
            service.DeleteBanners(1, new int[0]);
        }
    }
}
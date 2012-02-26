using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class ContractTests
    {
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod, Description("Calling GetForecast with negative parameter should fail")]
        public void GetBannersByIdsWithNullBannerIdsShouldFail()
        {
            var service = new YapiService();
            service.GetForecast(-1);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod, Description("Calling DeleteBanners with empty bannerIds fails")]
        public void GetBannersByIdsWithEmptyBannerIdsShouldFail()
        {
            var service = new YapiService();
            service.DeleteBanners(1, new int[0]);
        }
    }
}
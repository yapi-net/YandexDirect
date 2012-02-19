using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class ContractTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod, Description("Calling GetBannersByIds with null bannerIds fails")]
        public void GetBannersByIdsWithNullBannerIdsShouldFail()
        {
            var service = new YapiService();
            service.GetBannersByIds(null);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod, Description("Calling GetBannersByIds with empty bannerIds fails")]
        public void GetBannersByIdsWithEmptyBannerIdsShouldFail()
        {
            var service = new YapiService();
            service.GetBannersByIds(new int[0]);
        }
    }
}
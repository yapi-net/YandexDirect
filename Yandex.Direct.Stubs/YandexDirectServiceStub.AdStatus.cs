using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YandexDirectServiceStub
    {
        //TODO: Add Banner overloads

        public bool ArchiveBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }

        public bool DeleteBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }

        public bool ModerateBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }

        public bool ResumeBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }

        public bool StopBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }

        public bool UnArchiveBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            return true;
        }
    }
}

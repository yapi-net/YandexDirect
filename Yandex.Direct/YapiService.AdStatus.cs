using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    partial class YapiService
    {
        //TODO: Add Banner overloads

        public bool ArchiveBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.ArchiveBanners, request) == 1;
        }

        public bool DeleteBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.DeleteBanners, request) == 1;
        }

        public bool ModerateBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.ModerateBanners, request) == 1;
        }

        public bool ResumeBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.ResumeBanners, request) == 1;
        }

        public bool StopBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.StopBanners, request) == 1;
        }

        public bool UnArchiveBanners(int campaignId, int[] bannerIds)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            var request = new { CampaignID = campaignId, BannerIDS = bannerIds };

            return YandexApiClient.Invoke<int>(ApiMethod.UnArchiveBanners, request) == 1;
        }
    }
}

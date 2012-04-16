using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    partial class YandexDirectService
    {
        public BannerInfo GetBanner(int bannerId)
        {
            return GetBanners(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(BannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerWithPhrases(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(int bannerId)
        {
            return GetBannersWithPhrases(new[] { bannerId }).FirstOrDefault();
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(BannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerWithPhrasesAndStats(banner.BannerId);
        }

        public BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(int bannerId)
        {
            return GetBannersWithPhrasesAndStats(new[] { bannerId }).FirstOrDefault();
        }

        public List<BannerInfo> GetBanners(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of banner identifiers per call is 2000.");

            return GetBannersInternal<BannerInfo>(null, bannerIds, PhraseInfoType.No, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersWithPhrases(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of banner identifiers per call is 2000.");

            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfo>>(null, bannerIds, PhraseInfoType.Yes, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersWithPhrasesAndStats(int[] bannerIds, BannersFilterInfo filter = null)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 2000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of banner identifiers per call is 2000.");

            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfoWithStats>>(null, bannerIds, PhraseInfoType.WithPrices, filter);
        }

        public List<BannerInfo> GetBannersForCampaign(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfo>(new[] { campaignId }, null, PhraseInfoType.No, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersForCampaignWithPhrases(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfo>>(new[] { campaignId }, null, PhraseInfoType.Yes, filter);
        }

        public List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersForCampaignWithPhrasesAndStats(int campaignId, BannersFilterInfo filter = null)
        {
            return GetBannersInternal<BannerInfoWithPhrases<BannerPhraseInfoWithStats>>(new[] { campaignId }, null, PhraseInfoType.WithPrices, filter);
        }

        private List<T> GetBannersInternal<T>(int[] campaingIds, int[] bannerIds, PhraseInfoType phraseDetails, BannersFilterInfo filter)
            where T : BannerInfo
        {
            var request = new { CampaignIDS = campaingIds, BannerIDS = bannerIds, GetPhrases = phraseDetails, Filter = filter };

            return YandexApiClient.Invoke<List<T>>(ApiMethod.GetBanners, request);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(BannerInfo banner, bool considerTimeTarget = false)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerPhrases(banner.BannerId, considerTimeTarget);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(BannerInfo banner, bool considerTimeTarget = false)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return GetBannerPhrasesWithStats(banner.BannerId, considerTimeTarget);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(int bannerId, bool considerTimeTarget = false)
        {
            return GetBannerPhrases(new[] { bannerId }, considerTimeTarget);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int bannerId, bool considerTimeTarget = false)
        {
            return GetBannerPhrasesWithStats(new[] { bannerId }, considerTimeTarget);
        }

        public List<BannerPhraseInfo> GetBannerPhrases(int[] bannerIds, bool considerTimeTarget = false)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 1000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of banner identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.No, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return YandexApiClient.Invoke<List<BannerPhraseInfo>>(ApiMethod.GetBannerPhrasesFilter, request);
        }

        public List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int[] bannerIds, bool considerTimeTarget = false)
        {
            if (bannerIds == null || bannerIds.Length == 0)
                throw new ArgumentNullException("bannerIds");

            if (bannerIds.Length > 1000)
                throw new ArgumentOutOfRangeException("bannerIds", "Maximum allowed number of banner identifiers per call is 1000.");

            var request = new { BannerIDS = bannerIds, RequestPrices = YesNo.Yes, ConsiderTimeTarget = (YesNo)considerTimeTarget };

            return YandexApiClient.Invoke<List<BannerPhraseInfoWithStats>>(ApiMethod.GetBannerPhrasesFilter, request);
        }

        public int CreateOrUpdateBanner(EditableBannerInfo banner)
        {
            if (banner == null)
                throw new ArgumentNullException("banner");

            return CreateOrUpdateBanners(new[] { banner }).FirstOrDefault();
        }

        public List<int> CreateOrUpdateBanners(IEnumerable<EditableBannerInfo> banners)
        {
            if (banners == null)
                throw new ArgumentNullException("banners");

            var bannersArray = banners.ToArray();

            if (bannersArray.Contains(null))
                throw new ArgumentNullException("banners", "One of the items is null.");

            return YandexApiClient.Invoke<List<int>>(ApiMethod.CreateOrUpdateBanners, bannersArray);
        }
    }
}

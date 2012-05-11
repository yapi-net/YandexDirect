using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yandex.Direct
{
    public interface IYandexDirectService
    {
        // Finances

        void TransferMoney(TransferInfo from, TransferInfo to);
        void TransferMoney(TransferInfo[] from, TransferInfo[] to);

        // Campaign statistics

        int CreateNewReport(NewReportInfo reportInfo);

        List<ReportInfo> GetReportList();

        List<GoalInfo> GetStatGoals(int campaignId);

        void DeleteReport(int reportId);

        // Keywords

        // Budget forecasting

        int CreateNewForecast(string[] phrases, int[] geoIds = null, int[] categoryIds = null);
        
        ForecastInfo GetForecast(int forecastId);
        
        List<ForecastStatus> GetForecastList();

        void DeleteForecastReport(int forecastReportId);

        // Campaigns

        List<ShortCampaignInfo> GetClientCampaigns(params string[] logins);

        // Campaign state

        // Ads and phrases

        BannerInfo GetBanner(int bannerId);

        BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(BannerInfo banner);

        BannerInfoWithPhrases<BannerPhraseInfo> GetBannerWithPhrases(int bannerId);

        BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(BannerInfo banner);

        BannerInfoWithPhrases<BannerPhraseInfoWithStats> GetBannerWithPhrasesAndStats(int bannerId);

        List<BannerInfo> GetBanners(int[] bannerIds, BannersFilterInfo filter = null);

        List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersWithPhrases(int[] bannerIds, BannersFilterInfo filter = null);

        List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersWithPhrasesAndStats(int[] bannerIds, BannersFilterInfo filter = null);

        List<BannerInfo> GetBannersForCampaign(int campaignId, BannersFilterInfo filter = null);

        List<BannerInfoWithPhrases<BannerPhraseInfo>> GetBannersForCampaignWithPhrases(int campaignId, BannersFilterInfo filter = null);

        List<BannerInfoWithPhrases<BannerPhraseInfoWithStats>> GetBannersForCampaignWithPhrasesAndStats(int campaignId, BannersFilterInfo filter = null);

        List<BannerPhraseInfo> GetBannerPhrases(BannerInfo banner, bool considerTimeTarget = false);

        List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(BannerInfo banner, bool considerTimeTarget = false);

        List<BannerPhraseInfo> GetBannerPhrases(int bannerId, bool considerTimeTarget = false);

        List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int bannerId, bool considerTimeTarget = false);

        List<BannerPhraseInfo> GetBannerPhrases(int[] bannerIds, bool considerTimeTarget = false);

        List<BannerPhraseInfoWithStats> GetBannerPhrasesWithStats(int[] bannerIds, bool considerTimeTarget = false);

        int CreateOrUpdateBanner(EditableBannerInfo banner);

        List<int> CreateOrUpdateBanners(IEnumerable<EditableBannerInfo> banners);

        // Ad status

        bool ArchiveBanners(int campaignId, int[] bannerIds);

        bool DeleteBanners(int campaignId, int[] bannerIds);

        bool ModerateBanners(int campaignId, int[] bannerIds);

        bool ResumeBanners(int campaignId, int[] bannerIds);

        bool StopBanners(int campaignId, int[] bannerIds);

        bool UnArchiveBanners(int campaignId, int[] bannerIds);

        // Cost Per Click

        // Clients

        List<ShortClientInfo> GetClientLogins();

        ClientUnitInfo[] GetClientsUnits(params string[] logins);

        // Yandex.Catalog and regions

        // Other methods

        int PingApi();

        void TestApiConnection();
    }
}

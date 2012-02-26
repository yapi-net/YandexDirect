namespace Yandex.Direct
{
    public partial class YapiService
    {
        public enum ApiCommand
        {
            // ReSharper disable InconsistentNaming

            // Finances

            CreateInvoice,
            GetCreditLimits,
            PayCampaigns,
            TransferMoney,

            // Campaign statistics

            GetSummaryStat,
            CreateNewReport,
            DeleteReport,
            GetReportList,
            GetStatGoals,

            // Keywords

            CreateNewWordstatReport,
            DeleteWordstatReport,
            GetKeywordsSuggestion,
            GetWordstatReport,
            GetWordstatReportList,
            
            // Budget forecasting

            CreateNewForecast,
            DeleteForecastReport,
            GetForecast,
            GetForecastList,

            // Campaigns

            CreateOrUpdateCampaign,
            GetBalance,
            GetCampaignParams,
            GetCampaignsList,
            GetCampaignsListFilter,
            GetCampaignsParams,
            
            // Campaign state

            ArchiveCampaign,
            DeleteCampaign,
            ResumeCampaign,
            StopCampaign,
            UnArchiveCampaign,

            // Ads and phrases

            CreateOrUpdateBanners,
            GetBanners,
            GetBannerPhrases,
            GetBannerPhrasesFilter,

            // Ad status

            ArchiveBanners,
            DeleteBanners,
            ModerateBanners,
            ResumeBanners,
            StopBanners,
            UnArchiveBanners,

            // Cost Per Click

            SetAutoPrice,
            UpdatePrices,

            // Clients

            CreateNewSubclient,
            GetClientInfo,
            GetClientsList,
            GetClientsUnits,
            GetSubClients,
            UpdateClientInfo,

            // Yandex.Catalog and regions

            GetRegions,
            GetRubrics,

            // Other methods

            GetAvailableVersions,
            GetChanges,
            GetTimeZones,
            GetVersion,
            PingAPI
            
            // ReSharper restore InconsistentNaming
        }
    }
}

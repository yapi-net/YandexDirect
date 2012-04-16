namespace Yandex.Direct
{
    public enum YandexApiErrorCode
    {
        None = 0,

        // Error codes related to operations with statistical reports

        InvalidCampaignId = 1,
        NoStatisticsForCampaign = 2,
        InvalidDateFormat = 3,
        InvalidTimeInterval = 5,
        LimitsAreInvalid = 8,
        FieldMustBeArray = 9,
        ReportInvalidFilterByAds = 10,
        ReportInvalidFilterByGeo = 11,
        ReportInvalidFilterByPage = 12,
        ReportInvalidFilterByPageType = 13,
        ReportInvalidFilterByPhrases = 14,
        ReportInvalidFilterByTargetPageType = 15,
        ReportInvalidFilterByDate = 16,
        ReportInvalidFieldName = 17,
        ReportInvalidOffset = 18,
        ReportInvalidReportType = 20,
        ReportInvalidCompressionType = 21,
        ReportInvalidReportId = 22,
        ReportInvalidBannerId = 23,
        ReportNotExists = 24,
        ReportQueueIsFull = 31,
        InvalidFilterByPositionType = 32,
        InvalidFilterByTarget = 33,

        // Common error codes

        YesOrNoOnly = 25,
        ArrayIsEmpty = 30,
        AuthenticationError = 53,
        PermissionDenied = 54,
        MethodDoesNotExist = 55,
        RequestLimitExceeded = 56,
        ArraySizeExceeded = 241,
        InternalServerError = 500,
        InvalidRequest = 501,
        ServiceUnavailible = 503,
        SimultaneousRequestLimitExceeded = 506,
        ApiVersionNotSupported = 508,
        MethodNotAvailable = 509,
        AccessDenied = 510,
        UnknownLanguage = 511,
        InvalidRequestMethod = 512,

        // Error codes when working with budget forecasts and keywords

        //TODO : Add error codes

        // Error codes related to operations with campaigns and ads

        InvalidPercentValue = 26,
        InvalidBannerId = 27,
        InvalidPhraseId = 28,
        InvalidCampaignType = 29,
        InvalidCampaignInfo = 111,
        InvalidFilter = 113,
        InvalidBannerInfo = 151,
        NotEnoughUnits = 152,
        BannerLimitExceeded = 153,
        InvalidPointOnMap = 154,
        InvalidMinusKeywords = 192,

        // Error codes related to campaign price updates

        CannotChangeContextPrice = 191,
        IncorrectPrice = 242,
        InvalidValueForAutobroker = 243,

        // Error codes when working with clients

        InvalidUserLogin = 251,
        LoginNameAlreadyInUse = 252,
        UnableToCreateLoginName = 253,
        UnableToCreateClient = 254,
        UnableToLinkClientToAgency = 255

        // Error codes when working with finance methods
        
        //TODO : Add error codes
    }
}
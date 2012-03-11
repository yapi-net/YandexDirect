namespace Yandex.Direct.Connectivity
{
    public enum YandexApiErrorCode
    {
        None = 0,

        // Statistic errors

        WrongCampaignId = 1,
        WrongDateFormat = 3,
        FieldShouldBeAnArray = 9,
        WrongBannerFilter = 10,
        WrongBannerId = 23,

        // General errors

        YesOrNoOnly = 25,
        ArrayIsEmpty = 30,
        LoginNotFound = 53,
        PermissionDenied = 54,
        MethodDoesNotExist = 55,
        TechLimitReached = 56,
        ArrayTooLarge = 241,
        ServerError = 500,
        WrongRequest = 501,
        ServiceUnavailible = 503,
        ApiInaccessible = 510,

        // Campaign and banner errors

        MultipleTenRequirement = 26,
        WrongBannerIdCase = 27,
        WrongPhraseId = 28,
        WrongCampaignType = 29,
        WrongCampaignInfo = 111,
        WrongBannerInfo = 151,
        LackOfClientUnits = 152,
        BannerLimitExceeded = 153,
        WrongMinusWords = 192,

        // Price update errors

        ContextPriceChangeDenial = 191,
        WrongPrice = 242,
        WrongAutobrokerValue = 243,

        // Other

        LoginDoesNotExist = 251
    }
}
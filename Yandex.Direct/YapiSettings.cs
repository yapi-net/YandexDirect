namespace Yandex.Direct
{
    public class YapiSettings
    {
        /// <summary>
        /// Получение данных для подключения к Яндексу из секции yandex.direct в app.config
        /// </summary>
        public static YapiSettings FromConfiguration()
        {
            var config = Configuration.YandexDirectSection.Default;
            var result = new YapiSettings
                             {
                                 MasterToken = config.MasterToken,
                                 CertificatePassword = config.CertificatePassword,
                                 CertificatePath = config.CertificatePath,
                                 Login = config.Login,
                             };
            if (config.Language.HasValue)
                result.Language = config.Language.Value;
            return result;
        }

        public YapiSettings()
        {
            this.ApiAddress = "https://soap.direct.yandex.ru/json-api/v4/";
            this.Language = YapiLanguage.English;
        }

        public YapiSettings(string certificatePath, string certificatePassword)
            : this()
        {
            this.CertificatePath = certificatePath;
            this.CertificatePassword = certificatePassword;
        }

        public string ApiAddress { get; set; }
        public YapiLanguage Language { get; set; }

        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }

        public string MasterToken { get; set; }
        public string Login { get; set; }
    }
}
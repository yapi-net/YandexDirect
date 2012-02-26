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
            if (config.AuthType == YapiAuthType.Token && (string.IsNullOrWhiteSpace(config.ApplicationId) || string.IsNullOrWhiteSpace(config.Token) || string.IsNullOrWhiteSpace(config.Login)))
                throw new YapiConfigurationException(string.Format("Using {0} auth requires ApplicationId, Token and Login to be set", YapiAuthType.Token));
            if (config.AuthType == YapiAuthType.Certificate && (config.CertificatePassword == null || string.IsNullOrWhiteSpace(config.CertificatePath)))
                throw new YapiConfigurationException(string.Format("Using {0} auth requires CertificatePath and CertificatePassword to be set", YapiAuthType.Certificate));

            var result = new YapiSettings
                             {
                                 ApplicationId = config.ApplicationId,
                                 Token = config.Token,
                                 AuthType = config.AuthType,
                                 MasterToken = config.MasterToken,
                                 CertificatePassword = config.CertificatePassword,
                                 CertificatePath = config.CertificatePath,
                                 Login = config.Login,
                                 Language = config.Language ?? YapiLanguage.Russian,
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
        public YapiSettings(string login, string applicationId, string token)
            : this()
        {
            this.AuthType = YapiAuthType.Token;
            this.Login = login;
            this.ApplicationId = applicationId;
            this.Token = token;
        }

        public YapiSettings(string certificatePath, string certificatePassword)
            : this()
        {
            this.AuthType = YapiAuthType.Certificate;
            this.CertificatePath = certificatePath;
            this.CertificatePassword = certificatePassword;
        }

        public string ApiAddress { get; set; }
        public YapiLanguage Language { get; set; }
        public YapiAuthType AuthType { get; set; }

        public string CertificatePath { get; set; }
        public string CertificatePassword { get; set; }

        public string MasterToken { get; set; }
        public string Login { get; set; }

        public string ApplicationId { get; set; }
        public string Token { get; set; }
    }
}
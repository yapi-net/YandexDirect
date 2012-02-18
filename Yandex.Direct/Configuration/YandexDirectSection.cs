using System.Configuration;
using System.Diagnostics;

namespace Yandex.Direct.Configuration
{
    internal class YandexDirectSection : ConfigurationSection
    {
        #region Default Instance

        protected YandexDirectSection() { }

        const string SectionPath = "yandex.direct";
        static YandexDirectSection Instance;

        public static YandexDirectSection Default
        {
            [DebuggerStepThrough]
            get
            {
                if (Instance == null)
                {
                    var exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    Instance = exeConfiguration.GetSection(SectionPath) as YandexDirectSection;
                    if (Instance == null)
                        throw new ConfigurationErrorsException(SectionPath + " not found in configuration file");
                }

                return Instance;
            }
        }

        #endregion

        #region string CertificatePassword

        private const string CertificatePasswordFieldName = "certificatePassword";

        /// <summary>
        /// Пароль к файлу с сертификатом
        /// </summary>
        [ConfigurationProperty(CertificatePasswordFieldName, IsRequired = false, DefaultValue = "")]
        public string CertificatePassword
        {
            get { return (string)this[CertificatePasswordFieldName]; }
            set { this[CertificatePasswordFieldName] = value; }
        }

        #endregion

        #region string CertificatePath

        private const string CertificatePathFieldName = "certificatePath";

        /// <summary>
        /// Путь к файлу с сертификатом
        /// </summary>
        [ConfigurationProperty(CertificatePathFieldName, IsRequired = true)]
        public string CertificatePath
        {
            get { return (string)this[CertificatePathFieldName]; }
            set { this[CertificatePathFieldName] = value; }
        }

        #endregion

        #region YapiLanguage? Language

        private const string LanguageFieldName = "language";

        /// <summary>
        /// Язык ответов от Яндекса
        /// </summary>
        [ConfigurationProperty(LanguageFieldName, IsRequired = false)]
        public YapiLanguage? Language
        {
            get { return (YapiLanguage?)this[LanguageFieldName]; }
            set { this[LanguageFieldName] = value; }
        }

        #endregion

        #region string MasterToken

        private const string MasterTokenFieldName = "masterToken";

        /// <summary>
        /// Мастер-токен для приложений, работающих с финансовыми методами
        /// </summary>
        [ConfigurationProperty(MasterTokenFieldName, IsRequired = false)]
        public string MasterToken
        {
            get { return (string)this[MasterTokenFieldName]; }
            set { this[MasterTokenFieldName] = value; }
        }

        #endregion

        #region string Login

        private const string LoginFieldName = "login";

        /// <summary>
        /// Логин в Яндексе (для приложений, работающих с финансовыми методами)
        /// </summary>
        [ConfigurationProperty(LoginFieldName, IsRequired = false)]
        public string Login
        {
            get { return (string)this[LoginFieldName]; }
            set { this[LoginFieldName] = value; }
        }

        #endregion
    }
}

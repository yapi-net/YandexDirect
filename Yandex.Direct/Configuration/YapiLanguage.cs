using System.Runtime.Serialization;

namespace Yandex.Direct.Configuration
{
    /// <summary>
    /// Yandex API reply language
    /// </summary>
    public enum YapiLanguage
    {
        ///<summary>English</summary>
        [EnumMember(Value = "en")]
        English,

        ///<summary>Russian</summary>
        [EnumMember(Value = "ru")]
        Russian,

        ///<summary>Ukranian</summary>
        [EnumMember(Value = "ua")]
        Ukrainian,
    }
}
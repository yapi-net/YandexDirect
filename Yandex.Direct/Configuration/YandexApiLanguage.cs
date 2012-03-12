using System.Runtime.Serialization;

namespace Yandex.Direct.Configuration
{
    /// <summary>
    /// Enumerates languages for responses.
    /// </summary>
    public enum YandexApiLanguage
    {
        ///<summary>English language.</summary>
        [EnumMember(Value = "en")]
        English,

        ///<summary>Russian language.</summary>
        [EnumMember(Value = "ru")]
        Russian,

        ///<summary>Ukranian language.</summary>
        [EnumMember(Value = "ua")]
        Ukrainian,
    }
}
using System.Runtime.Serialization;

namespace Yandex.Direct
{
    /// <summary>
    /// язык ответных сообщений от яндекса
    /// </summary>
    public enum YapiLanguage
    {
        ///<summary>јнглийский</summary>
        [EnumMember(Value = "en")]
        English,

        ///<summary>–усский</summary>
        [EnumMember(Value = "ru")]
        Russian,

        ///<summary>”краинский</summary>
        [EnumMember(Value = "ua")]
        Ukrainian,
    }
}
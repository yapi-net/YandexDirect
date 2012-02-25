using System.Runtime.Serialization;

namespace Yandex.Direct
{
    /// <summary>
    /// ���� �������� ��������� �� �������
    /// </summary>
    public enum YapiLanguage
    {
        ///<summary>����������</summary>
        [EnumMember(Value = "en")]
        English,

        ///<summary>�������</summary>
        [EnumMember(Value = "ru")]
        Russian,

        ///<summary>����������</summary>
        [EnumMember(Value = "ua")]
        Ukrainian,
    }
}
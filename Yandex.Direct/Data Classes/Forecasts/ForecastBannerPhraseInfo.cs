using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct
{
    public class ForecastBannerPhraseInfo
    {
        /// <summary>����� ����� ��� ������������� ������� ������.�������� (� ����������� �� ��������� IsRubric)</summary>
        public string Phrase { get; set; }

        /// <summary>� �������� ����� ������������ ������� ������.�������� � Yes/No. ��� �������� Yes � ��������� Phrase ������ ������������� �������</summary>
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsRubric { get; set; }

        /// <summary>���� �� ���� �� ������ ��������� ���� �������</summary>
        public decimal ContextPrice { get; set; }

        /// <summary>���������� ������ �� ����������, ���������� � ������� ������ ����� �� ������ �������</summary>
        public int Clicks { get; set; }

        /// <summary>���������� ������� ���������� �� ������ ����� �� ������ �������</summary>
        public int Shows { get; set; }

        /// <summary>��������� ���������������� ������ ����������</summary>
        public decimal Min { get; set; }

        /// <summary>��������� ������ ���������� �� ������ �����</summary>
        public decimal Max { get; set; }

        /// <summary>��������� ������ ���������� � ��������������</summary>
        public decimal PremiumMin { get; set; }

        /// <summary>��������� ������ ���������� �� ������ ����� � ��������������</summary>
        public decimal PremiumMax { get; set; }

        /// <summary>����� ����� ������ CTR � ����� ���� ������ ���������</summary>
        [JsonProperty("LowCTRWarning")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsLowCtrWarning { get; set; }

        /// <summary>����� ��������� �� ������ �� ������ CTR</summary>
        [JsonProperty("LowCTR")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsLowCtr { get; set; }

        /// <summary>����� ��������� �� ������ ��������� ���� ������� �� ������ CTR</summary>
        [JsonProperty("ContextLowCTR")]
        [JsonConverter(typeof(YesNoConverter))]
        public bool IsContextLowCtr { get; set; }

        /// <summary>�������������� ���������� ������ ��� ������ �� ������ �����</summary>
        public int FirstPlaceClicks { get; set; }

        /// <summary>�������������� ���������� ������ ��� ������ � ��������������</summary>
        public int PremiumClicks { get; set; }

        /// <summary>�������������� CTR ��� ������ �� ������ �����</summary>
        [JsonProperty("FirstPlaceCTR")]
        public decimal FirstPlaceCtr { get; set; }

        /// <summary>�������������� CTR ��� ������ � ��������������</summary>
        [JsonProperty("PremiumCTR")]
        public decimal PremiumCtr { get; set; }

        // TODO Create CoverageInfo class
        //public CoverageInfo[] Coverage { get; set; }
        //public CoverageInfo[] ContextCoverage { get; set; }
    }
}
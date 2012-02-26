using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class ErrorInfoDeserializationTests
    {
        const string ErrorDescription = "Some Details";
        const string ErrorString = "Error";

        private static string CreateErrorInfoJson(int errorCode, string errorString, string errorDescription)
        {
            return string.Format("{{error_detail:\"{0}\", error_str:\"{1}\", error_code: {2}}}", errorDescription, errorString, arg2: errorCode);
        }

        static T Deserialize<T>(string jsonString)
        {
            return new YapiSerializer().Deserialize<T>(jsonString);
        }

        [TestMethod]
        [Description("Simple deserialization of YandexErrorInfo works")]
        public void SimpleDeserializationWorks()
        {
            const YapiService.YapiErrorCode errorCode = YapiService.YapiErrorCode.BannerLimitExceeded;
            var json = CreateErrorInfoJson((int)errorCode, ErrorString, ErrorDescription);
            var result = Deserialize<YandexErrorInfo>(json);
            result.Error.ShouldBe(ErrorString);
            result.Description.ShouldBe(ErrorDescription);
            result.Code.ShouldBe(errorCode);
        }

        [TestMethod]
        [Description("Deserialization of YandexErrorInfo works even if Yandex returned unknown error code")]
        public void DeserializationOfNonexistent()
        {
            const int errorCode = 10010; // no such error code
            var json = CreateErrorInfoJson(errorCode, ErrorString, ErrorDescription);
            var result = Deserialize<YandexErrorInfo>(json);
            result.Error.ShouldBe(ErrorString);
            result.Description.ShouldBe(ErrorDescription);
            ((int)result.Code).ShouldBe(errorCode);
        }
    }
}
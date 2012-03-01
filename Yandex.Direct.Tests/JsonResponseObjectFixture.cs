using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yandex.Direct.Connectivity;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class JsonResponseObjectFixture
    {
        const YandexApiErrorCode ErrorCode = YandexApiErrorCode.BannerLimitExceeded;
        const string ErrorMessage = "ERROR_MESSAGE";
        const string ErrorDescription = "ERROR_DESCRIPTION";

        static T Deserialize<T>(string jsonString)
        {
            return new JsonYandexApiSerializer().Deserialize<T>(jsonString);
        }

        private static string CreateErrorInfoJson(int errorCode, string errorMessage, string errorDescription)
        {
            return string.Format("{{error_detail:\"{0}\", error_str:\"{1}\", error_code: {2}}}", errorDescription, errorMessage, errorCode);
        }

        [TestMethod]
        public void SimpleDeserializationWorks()
        {
            var json = CreateErrorInfoJson((int)ErrorCode, ErrorMessage, ErrorDescription);
            
            var result = Deserialize<JsonResponseObject<object>>(json);

            result.ErrorCode.ShouldBe(ErrorCode);
            result.ErrorMessage.ShouldBe(ErrorMessage);
            result.ErrorDescription.ShouldBe(ErrorDescription);

            result.Object.ShouldBe(null);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yandex.Direct.Connectivity;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class JsonResponseObjectFixture
    {
        const YandexApiErrorCode ERROR_CODE = YandexApiErrorCode.BannerLimitExceeded;
        const string ERROR_MESSAGE = "ERROR_MESSAGE";
        const string ERROR_DESCRIPTION = "ERROR_DESCRIPTION";

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
            var json = CreateErrorInfoJson((int)ERROR_CODE, ERROR_MESSAGE, ERROR_DESCRIPTION);
            
            var result = Deserialize<JsonResponseObject<object>>(json);

            result.ErrorCode.ShouldBe(ERROR_CODE);
            result.ErrorMessage.ShouldBe(ERROR_MESSAGE);
            result.ErrorDescription.ShouldBe(ERROR_DESCRIPTION);

            result.Object.ShouldBe(null);
        }
    }
}
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class YesNoFixture
    {
        public class YesNoObjectStub
        {
            [JsonConverter(typeof(YesNoArrayConverter))]
            public bool? Value { get; set; }
        }

        [TestMethod]
        public void YesNoConstructionTest()
        {
            Assert.AreEqual("Yes", YesNo.Yes.ToString());
            Assert.AreEqual("No", YesNo.No.ToString());

            Assert.AreEqual("Yes", ((YesNo)true).ToString());
            Assert.AreEqual("No", ((YesNo)false).ToString());

            Assert.AreEqual("Yes", new YesNo(true).ToString());
            Assert.AreEqual("No", new YesNo(false).ToString());
        }

        [TestMethod]
        public void YesNoEqualityTest()
        {
            // ReSharper disable EqualExpressionComparison
            
            Assert.AreEqual(YesNo.Yes, YesNo.Yes);
            Assert.AreEqual(YesNo.Yes, new YesNo(true));
            Assert.AreEqual(YesNo.Yes, (YesNo)true);

            Assert.IsTrue(YesNo.Yes == YesNo.Yes);
            Assert.IsTrue(YesNo.Yes == (YesNo)true);
            Assert.IsTrue(YesNo.Yes == new YesNo(true));
            Assert.IsTrue(YesNo.Yes == true);

            Assert.AreNotEqual(YesNo.Yes, YesNo.No);
            Assert.AreNotEqual(YesNo.Yes, new YesNo(false));
            Assert.AreNotEqual(YesNo.Yes, (YesNo)false);

            Assert.IsTrue(YesNo.Yes != YesNo.No);
            Assert.IsTrue(YesNo.Yes != (YesNo)false);
            Assert.IsTrue(YesNo.Yes != new YesNo(false));
            Assert.IsTrue(YesNo.Yes != false);

            // ReSharper restore EqualExpressionComparison
        }

        [TestMethod]
        public void YesNoSerializationTest()
        {
            JsonYandexApiSerializer serializer = new JsonYandexApiSerializer();

            Assert.AreEqual("\"Yes\"", serializer.Serialize(YesNo.Yes));

            Assert.AreEqual(YesNo.Yes, serializer.Deserialize<YesNo>("\"Yes\""));
            Assert.AreEqual(YesNo.Yes, serializer.Deserialize<YesNo?>("\"Yes\""));
            Assert.AreEqual(null, serializer.Deserialize<YesNo?>(string.Empty));
        }

        [TestMethod]
        public void YesNoArrayConverterTest()
        {
            JsonYandexApiSerializer serializer = new JsonYandexApiSerializer();

            Assert.AreEqual("{\"Value\":[\"Yes\"]}", serializer.Serialize(new YesNoObjectStub { Value = true }));
            Assert.AreEqual("{\"Value\":[\"No\"]}", serializer.Serialize(new YesNoObjectStub { Value = false }));
            Assert.AreEqual("{}", serializer.Serialize(new YesNoObjectStub { Value = null }));
        }
    }
}

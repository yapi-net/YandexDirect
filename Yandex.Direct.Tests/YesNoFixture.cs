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
            Assert.AreEqual("\"Yes\"", JsonConvert.SerializeObject(YesNo.Yes));

            Assert.AreEqual(YesNo.Yes, JsonConvert.DeserializeObject<YesNo>("\"Yes\""));
            Assert.AreEqual(YesNo.Yes, JsonConvert.DeserializeObject<YesNo?>("\"Yes\""));
            Assert.AreEqual(null, JsonConvert.DeserializeObject<YesNo?>(string.Empty));
        }

        [TestMethod]
        public void YesNoArrayConverterTest()
        {
            Assert.AreEqual("{\"Value\":[\"Yes\"]}", JsonConvert.SerializeObject(new YesNoObjectStub { Value = true }));
            Assert.AreEqual("{\"Value\":[\"No\"]}", JsonConvert.SerializeObject(new YesNoObjectStub { Value = false }));
            Assert.AreEqual("{\"Value\":null}", JsonConvert.SerializeObject(new YesNoObjectStub { Value = null }));
        }
    }
}

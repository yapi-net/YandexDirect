using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yandex.Direct.Serialization;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class JsonSerializerFixture
    {
        [TestMethod]
        public void SerializationTest()
        {
            JsonYandexApiSerializer serializer = new JsonYandexApiSerializer();

            Assert.AreEqual("{\"Param1\":\"Value1\",\"Param2\":100}", serializer.Serialize(new { Param1 = "Value1", Param2 = 100, Param3 = (string)null }));
            Assert.AreEqual("{\"Param1\":\"2001-11-22\"}", serializer.Serialize(new { Param1 = new DateTime(2001, 11, 22) }));

            Assert.AreEqual("10", serializer.Serialize(10));
            Assert.AreEqual("0", serializer.Serialize(0));
            Assert.AreEqual("123.456", serializer.Serialize(123.456));

            Assert.AreEqual("\"Value1\"", serializer.Serialize("Value1"));
            Assert.AreEqual("[\"Value1\",\"Value2\",\"Value3\"]", serializer.Serialize(new[] { "Value1", "Value2", "Value3" }));

            Assert.AreEqual("null", serializer.Serialize(null));
        }
    }
}

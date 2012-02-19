using Yandex.Direct;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Yandex.Direct.Tests
{
    [TestClass]
    public class StringMergingTests
    {
        [TestMethod, Description("Merging with null as separator is OK")]
        public void NullSeparatorIsOk()
        {
            var strings = new[] { "s1", "s2" };
            strings.Merge(null).ShouldBe("s1s2");
        }

        [TestMethod, Description("Merging collection with one item is OK")]
        public void OneItemIsOk()
        {
            var strings = new[] { "s1" };
            strings.Merge(",").ShouldBe("s1");
        }

        [TestMethod, Description("Merging collection string.Empty as separator is OK")]
        public void EmptySeparatorIsOk()
        {
            var strings = new[] { "s1", "s2" };
            var merge = strings.Merge("");
            merge.ShouldBe("s1s2");
        }
    }
}
using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Highway.Pavement.Tests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void ShouldAllowForParseOfSupportedTypes()
        {
            "1".Parse<int>().Should().Be(1);
            "1".Parse<Int16>().Should().Be(Convert.ToInt16(1));
            "1".Parse<Int32>().Should().Be(Convert.ToInt32(1));
            "1".Parse<Int64>().Should().Be(Convert.ToInt64(1));
            "1".Parse<UInt16>().Should().Be(Convert.ToUInt16(1));
            "1".Parse<UInt32>().Should().Be(Convert.ToUInt32(1));
            "1".Parse<UInt64>().Should().Be(Convert.ToUInt64(1));
            "1.2".Parse<double>().Should().Be(Convert.ToDouble(1.2));
            "1.2".Parse<decimal>().Should().Be(Convert.ToDecimal(1.2));
            "9-12-2013".Parse<DateTime>().Should().Be(new DateTime(2013, 9, 12));
            "09-12-2013".Parse<DateTime>().Should().Be(new DateTime(2013, 9, 12));
            "9-1-2013".Parse<DateTime>().Should().Be(new DateTime(2013, 9, 1));
            "09-01-2013".Parse<DateTime>().Should().Be(new DateTime(2013, 9, 1));

        }

        [TestMethod]
        public void ShouldAllFormatingOffAString()
        {
            "{0} and {1}".Format(2, "two").Should().Be("2 and two");
            "{0} and {1}".Format(2, 2).Should().Be("2 and 2");
        }
    }
}

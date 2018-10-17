namespace SWE.BasicType.Test.Utilities
{
    using global::Xunit;
    using SWE.BasicType.Test.Data.Enums;
    using SWE.BasicType.Utilities;
    using SWE.Xunit.Attributes;
    using System.Linq;

    public class EnumUtilitiesTest
    {
        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnEmpty_WhenEmpty()
        {
            Assert.Empty(EnumUtilities.GetValues<Empty>());
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnEmpty_WhenEmpty_With_Expression()
        {
            Assert.Empty(EnumUtilities.GetValues<Empty>(x => x.ToString().Contains("Value")));
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnTwo()
        {
            Assert.Equal(3, EnumUtilities.GetValues<NonEmpty>().Count());
        }

        [Fact]
        [Category("EnumUtilities")]
        public void GetValues_Should_ReturnOne_With_Expression()
        {
            Assert.Single(EnumUtilities.GetValues<NonEmpty>(x => x.ToString().Contains(nameof(NonEmpty.FirstValue))));
        }
    }
}
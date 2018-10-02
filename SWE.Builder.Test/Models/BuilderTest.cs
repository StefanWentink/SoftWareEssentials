namespace SWE.Builder.Test.Models
{
    using SWE.Builder.Test.Data;
    using SWE.Xunit.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::Xunit;

    public class BuilderTest
    {
        [Fact]
        [Category("CollectionExtensions")]
        public void Build_Should_ReturnDefaultResult_When_MemberNotSet()
        {
            var actual = new VehicleMemberBuilder().Build();
            Assert.Null(actual);
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void Build_Should_ReturnResult_When_MemberSet()
        {
            IVehicleStub member = new VehicleStub(1, 2);
            var actual = new VehicleMemberBuilder().SetMember(member).Build();
            Assert.NotNull(actual);
            Assert.Equal(member.Speed, actual.Speed);
            Assert.Equal(member.NumberOfWheels, actual.NumberOfWheels);
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void Build_Should_ReturnResult_When_MemberSetOnConstructor()
        {
            IVehicleStub member = new VehicleStub(1, 2);
            var actual = new VehicleMemberBuilder(member).Build();
            Assert.NotNull(actual);
            Assert.Equal(member.Speed, actual.Speed);
            Assert.Equal(member.NumberOfWheels, actual.NumberOfWheels);
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void Build_Should_ReturnResult_When_NoValuesSet()
        {
            var actual = new VehicleBuilder().Build();
            Assert.NotNull(actual);
            Assert.Equal(0, actual.Speed);
            Assert.Equal(0, actual.NumberOfWheels);
        }

        [Fact]
        [Category("CollectionExtensions")]
        public void Build_Should_ReturnResult_When_ValuesSet()
        {
            var actual = new VehicleBuilder().SetSpeed(1).SetNumberOfWheels(2).SetSpeed(3).Build();
            Assert.NotNull(actual);
            Assert.Equal(3, actual.Speed);
            Assert.Equal(2, actual.NumberOfWheels);
        }
    }
}

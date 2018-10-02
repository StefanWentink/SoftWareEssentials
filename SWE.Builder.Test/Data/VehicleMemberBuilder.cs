namespace SWE.Builder.Test.Data
{
    using SWE.Builder.Models;

    internal class VehicleMemberBuilder : MemberBuilder<VehicleStub, IVehicleStub>
    {
        public VehicleMemberBuilder()
        { }

        public VehicleMemberBuilder(IVehicleStub member)
            : base(member)
        { }

        protected override VehicleStub BuildResult()
        {
            return new VehicleStub(Member.Speed, Member.NumberOfWheels);
        }
    }
}
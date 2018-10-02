namespace SWE.Builder.Test.Data
{
    using SWE.Builder.Interfaces;
    using SWE.Builder.Models;

    internal class VehicleBuilder : Builder<VehicleStub, IVehicleStub>
    {
        private int _speed;

        private int _numberOfWheels;

        protected override VehicleStub BuildResult()
        {
            return new VehicleStub(_speed, _numberOfWheels);
        }

        internal VehicleBuilder SetSpeed(int speed)
        {
            _speed = speed;
            return this;
        }

        internal VehicleBuilder SetNumberOfWheels(int numberOfWheels)
        {
            _numberOfWheels = numberOfWheels;
            return this;
        }
    }
}
using System;

namespace SWE.Builder.Test.Data
{
    internal class VehicleStub : IVehicleStub
    {
        public int Speed { get; }

        public int NumberOfWheels { get; }

        [Obsolete("Only for serialization.", true)]
        internal VehicleStub()
            : this(0, 0)
        { }

        internal VehicleStub(int speed, int numberOfWheels)
        {
            Speed = speed;
            NumberOfWheels = numberOfWheels;
        }
    }
}
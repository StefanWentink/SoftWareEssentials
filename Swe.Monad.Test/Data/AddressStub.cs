namespace Swe.Monad.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class AddressStub
    {
        private int _number;

        public string Street { get; set; }

        public bool Delivery { get; }

        public int Number
        {
            get { return _number; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException($"Number ({value}) should be greater tham zero.");
                }

                _number = value;
            }
        }

        public AddressStub(string street, int number)
            : this(street, number, false)
        {
        }

        public AddressStub(string street, int number, bool delivery)
        {
            Street = street;
            Number = number;
            Delivery = delivery;
        }
    }
}
namespace Swe.Monad.Test.Data
{
    using System.Collections.Generic;

    internal class CustomerStub
    {
        public string Name { get; }

        public ICollection<AddressStub> Addresses { get; set; }

        public CustomerStub(string name)
        {
            Name = name;
        }

        public CustomerStub(string name, AddressStub address)
        {
            Name = name;
            Addresses = new List<AddressStub> { address };
        }
    }
}
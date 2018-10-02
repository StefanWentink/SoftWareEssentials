namespace SWE.Contract.Interfaces.Data.Command
{
    using System.Collections.Generic;

    public interface ICommandContract<in T> : IBaseCommandContract<T>
    {
        bool Delete(T value);

        bool Delete(IEnumerable<T> values);
    }
}
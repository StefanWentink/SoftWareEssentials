namespace SWE.Contract.Interfaces.Data.Command
{
    using System.Collections.Generic;

    public interface IBaseCommandContract<in T>
    {
        bool Create(T value);

        bool Create(IEnumerable<T> values);

        bool Update(T value);

        bool Update(IEnumerable<T> values);
    }
}
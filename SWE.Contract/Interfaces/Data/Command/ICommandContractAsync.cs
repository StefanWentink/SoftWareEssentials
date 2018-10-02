namespace SWE.Contract.Interfaces.Data.Command
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommandContractAsync<in T> : IBaseCommandContractAsync<T>
    {
        Task<bool> Delete(T value);

        Task<bool> Delete(IEnumerable<T> values);
    }
}
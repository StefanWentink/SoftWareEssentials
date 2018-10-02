namespace SWE.Contract.Interfaces.Data.Command
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseCommandContractAsync<in T>
    {
        Task<bool> CreateAsync(T value);

        Task<bool> CreateAsync(IEnumerable<T> values);

        Task<bool> UpdateAsync(T value);

        Task<bool> UpdateAsync(IEnumerable<T> values);
    }
}
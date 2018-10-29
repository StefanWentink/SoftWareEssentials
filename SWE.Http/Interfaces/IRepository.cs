namespace SWE.Http.Interfaces
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository
    {
        Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content);

        Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            string route);

        Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            Encoding encoding);

        Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            string route,
            Encoding encoding);
    }

    public interface IRepository<T>
    {
        Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content);

        Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            string route);

        Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            Encoding encoding);

        Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            string route,
            Encoding encoding);

        void SetTimeOut(int milliSeconds);
    }
}
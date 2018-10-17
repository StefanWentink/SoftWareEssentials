namespace SWE.Http.Interfaces
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IExchanger
    {
        Task<byte[]> GetBytes(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request);

        Task<Stream> GetStream(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request);

        Task<string> GetString(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request);
    }
}
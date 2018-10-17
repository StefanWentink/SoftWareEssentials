namespace SWE.Http.Models
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using SWE.Http.Enums;
    using SWE.Http.Interfaces;

    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Repository : BaseRepository, IRepository
    {
        public Repository(ILogger<Repository> logger, IExchanger exchanger, IActions actions)
            : base(logger, exchanger, actions)
        {
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content)
        {
            return await ReadAsync<T>(
                cancellationToken,
                securityToken,
                policy,
                content,
                Encoding.UTF8).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            string route)
        {
            return await ReadAsync<T>(
                cancellationToken,
                securityToken,
                policy,
                content,
                route,
                Encoding.UTF8).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            Encoding encoding)
        {
            return await ReadAsync<T>(
                cancellationToken,
                securityToken,
                policy,
                content,
                typeof(T).Name,
                encoding).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            string content,
            string route,
            Encoding encoding)
        {
            var request = CreateRequest(route, Actions.Read, content, HttpVerb.Get, encoding);

            var result = await Exchanger.GetString(
                cancellationToken,
                securityToken,
                policy,
                request).ConfigureAwait(false);

            try
            {
                return Deserialize<T>(result);
            }
            catch (JsonSerializationException exception)
            {
                LogSerializationException(exception, request, result);
                throw;
            }
        }

        protected virtual List<T> Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<List<T>>(value);
        }
    }
}
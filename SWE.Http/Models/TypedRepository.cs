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

    public class TypedRepository<T> : BaseRepository, IRepository<T>
    {
        protected ITimeOutPolicy<T> Policy { get; }

        public TypedRepository(
            ILogger logger,
            IExchanger exchanger,
            IActions actions,
            ITimeOutPolicy<T> policy)
            : base(logger, exchanger, actions)
        {
            Policy = policy;
        }

        public async Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content)
        {
            return await ReadAsync(
                cancellationToken,
                securityToken,
                content,
                Encoding.UTF8).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            string route)
        {
            return await ReadAsync(
                cancellationToken,
                securityToken,
                content,
                route,
                Encoding.UTF8).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            Encoding encoding)
        {
            return await ReadAsync(
                cancellationToken,
                securityToken,
                content,
                typeof(T).Name,
                encoding).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ReadAsync(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            string content,
            string route,
            Encoding encoding)
        {
            var request = CreateRequest(route, Actions.Read, content, HttpVerb.Get, encoding);

            var result = await Exchanger.GetString(
                cancellationToken,
                securityToken,
                Policy,
                request).ConfigureAwait(false);

            try
            {
                return Deserialize(result);
            }
            catch (JsonSerializationException exception)
            {
                LogSerializationException(exception, request, result);
                throw;
            }
        }

        protected virtual List<T> Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<List<T>>(value);
        }
    }
}
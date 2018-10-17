namespace SWE.Http.Models
{
    using Enums;
    using Interfaces;
    using Microsoft.Extensions.Logging;
    using SWE.Http.Extensions;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Exchanger : IExchanger, IDisposable
    {
        private ILogger Logger { get; set; }

        protected IUriContainer UriContainer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exchanger"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="uri"></param>
        public Exchanger(ILogger<Exchanger> logger, IUriContainer uriContainer)
        {
            Logger = logger;
            UriContainer = uriContainer;
        }

        protected HttpClient GetClient(
            ISecurityToken securityToken,
            ITimeOutPolicy policy)
        {
            var client = new HttpClient
            {
                BaseAddress = UriContainer.Uri
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(UriContainer.ContentType));

            if (!securityToken?.IsNullOrEmpty() ?? false)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(securityToken.Schema, securityToken.Parameter);
            }

            client.Timeout = ((policy?.TimeOutMilliseconds ?? 0) > 0)
                ? TimeSpan.FromMilliseconds(policy.TimeOutMilliseconds)
                : TimeSpan.FromSeconds(30);

            return client;
        }

        protected async Task<HttpResponseMessage> RequestContent(
                CancellationToken cancellationToken,
                ISecurityToken securityToken,
                ITimeOutPolicy policy,
                IRequest request)
        {
            var requestUri = string.Format(UriContainer.Format, request.Route, request.Action);
            var content = request.Content.ToStringContent(UriContainer.ContentType, Encoding.UTF8);

            return await RequestContent(
                cancellationToken,
                securityToken,
                policy,
                request,
                requestUri,
                content).ConfigureAwait(false);
        }

        protected virtual async Task<HttpResponseMessage> RequestContent(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request,
            string requestUri,
            StringContent content)
        {
            try
            {
                using (var client = GetClient(securityToken, policy))
                {
                    switch (request.Verb)
                    {
                        case HttpVerb.Get:
                            return await client.GetAsync(requestUri + request.Content, cancellationToken).ConfigureAwait(false);

                        case HttpVerb.Post:
                            return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);

                        case HttpVerb.Put:
                            return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);

                        case HttpVerb.Delete:
                            return await client.DeleteAsync(requestUri + request.Content, cancellationToken).ConfigureAwait(false);

                        default:
                            return null;
                    }
                }
            }
            catch (OperationCanceledException operationCanceledException)
            {
                Logger.LogWarning($"Operation cancelled: {operationCanceledException.Message}.");
                throw;
            }
            catch (Exception exception)
            {
                Logger.LogError($"Operation cancelled: {exception.Message}.");
                throw;
            }
        }

        public async Task<byte[]> GetBytes(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request)
        {
            var response = await RequestContent(
                cancellationToken,
                securityToken,
                policy,
                request).ConfigureAwait(false);

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public async Task<Stream> GetStream(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request)
        {
            var response = await RequestContent(
                cancellationToken,
                securityToken,
                policy,
                request).ConfigureAwait(false);

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public async Task<string> GetString(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request)
        {
            var response = await RequestContent(
                cancellationToken,
                securityToken,
                policy,
                request).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Exchanger()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Disposing class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Logger = null;
                UriContainer = null;
            }
        }
    }
}
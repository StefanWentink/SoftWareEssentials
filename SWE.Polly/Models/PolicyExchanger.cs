namespace SWE.Polly.Models
{
    using Microsoft.Extensions.Logging;
    using SWE.Http.Interfaces;
    using SWE.Http.Models;
    using SWE.Polly.Interfaces.Policies;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class PolicyExchanger : Exchanger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyExchanger"/> class.
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="token"></param>
        public PolicyExchanger(ILogger logger, IUriContainer uri)
            : base(logger, uri)
        {
        }

        protected override async Task<HttpResponseMessage> RequestContent(
            CancellationToken cancellationToken,
            ISecurityToken securityToken,
            ITimeOutPolicy policy,
            IRequest request,
            string requestUri,
            StringContent content)
        {
            var task = base.RequestContent(
                cancellationToken,
                securityToken,
                policy,
                request,
                requestUri,
                content);

            if (policy is IPollyPolicy pollyPolicy)
            {
                return await pollyPolicy.PolicyWrap.ExecuteAsync(() => task).ConfigureAwait(false);
            }

            return await task.ConfigureAwait(false);
        }
    }
}
using System.Net.Http;
using System.Text;

namespace SWE.Http.Extensions
{
    internal static class ContentExtensions
    {
        internal static StringContent ToStringContent(this string content, string contentType)
        {
            return content.ToStringContent(contentType, Encoding.UTF8);
        }

        internal static StringContent ToStringContent(this string content, string contentType, Encoding encoding)
        {
            return new StringContent(content, encoding, contentType);
        }
    }
}
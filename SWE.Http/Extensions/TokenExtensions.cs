namespace SWE.Http.Extensions
{
    using SWE.Http.Interfaces;

    public static class TokenExtensions
    {
        /// <summary>
        /// Returns if <see cref="ISecurityToken"/> is unusable when <see cref="ISecurityToken.Schema"/> or <see cref="ISecurityToken.Parameter"/> are empty.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this ISecurityToken token)
        {
            return string.IsNullOrWhiteSpace(token.Schema) || string.IsNullOrWhiteSpace(token.Parameter);
        }
    }
}
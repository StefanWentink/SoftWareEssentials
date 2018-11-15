namespace SWE.Contract.Enums
{
    using System.ComponentModel;

    public enum OrderDirective
    {
        Undefined = 0,

        /// <summary>
        /// Pre.
        /// </summary>
        [Description("Pre")]
        Pre = 1,

        /// <summary>
        /// Post.
        /// </summary>
        [Description("Post")]
        Post = 2
    }
}
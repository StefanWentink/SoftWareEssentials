namespace SWE.Contract.Enums
{
    using System.ComponentModel;

    public enum OperationDirective
    {
        Undefined = 0,

        /// <summary>
        /// Read
        /// </summary>
        [Description("Create")]
        Create = 1,

        /// <summary>
        /// Update
        /// </summary>
        [Description("Update")]
        Update = 2,

        /// <summary>
        /// Delete
        /// </summary>
        [Description("Delete")]
        Delete = 3
    }
}
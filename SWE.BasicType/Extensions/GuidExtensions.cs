namespace SWE.BasicType.Extensions
{
    using System;

    using SWE.BasicType.Utilities;

    public static class GuidExtensions
    {
        /// <summary>
        /// Returns false if <see cref="value"/> is null or default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GuidIsNullOrEmpty(this Guid? value)
        {
            return !value.HasValue || value.Value.GuidIsNullOrEmpty();
        }

        /// <summary>
        /// Returns false if <see cref="value"/> is default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GuidIsNullOrEmpty(this Guid value)
        {
            return ObjectUtilities.IsDefault(value);
        }
    }
}
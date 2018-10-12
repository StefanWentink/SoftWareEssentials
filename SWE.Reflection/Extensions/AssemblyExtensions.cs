namespace SWE.Reflection.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets all non-abstract types derived from <see cref="derivedType"/>
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="derivedType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllInstanceTypes(this Assembly assembly, Type derivedType)
        {
            return assembly.GetTypes()
                .Where(t =>
                t != derivedType
                && derivedType.IsAssignableFrom(t)
                && !t.GetTypeInfo().IsAbstract);
        }
    }
}
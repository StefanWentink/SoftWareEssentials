namespace SWE.Reflection.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class CustomAttributeUtilities
    {
        /// <summary>
        /// Returns <see cref="IEnumerable{Attribute}"/> for <see cref="T"/> <see cref="attributeProvider"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetCustomAttribute<T>(T attributeProvider)
            where T : ICustomAttributeProvider
        {
            return attributeProvider.GetCustomAttributes(false).Cast<Attribute>();
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{Attribute}"/> for <see cref="T"/> <see cref="attributeProvider"/>
        /// of type or derived from <see cref="attribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetCustomAttribute<T>(T attributeProvider, Attribute attribute)
            where T : ICustomAttributeProvider
        {
            return GetCustomAttribute(attributeProvider, new[] { attribute });
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{Attribute}"/> for <see cref="T"/> <see cref="attributeProvider"/>
        /// of type or derived from <see cref="attribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetCustomAttribute<T>(T attributeProvider, Type attributeType)
            where T : ICustomAttributeProvider
        {
            return GetCustomAttribute(attributeProvider, new[] { attributeType });
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{ICustomAttributeProvider}"/> for <see cref="T"/> <see cref="attributeProvider"/>
        /// of type or derived from <see cref="attributes"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetCustomAttribute<T>(T attributeProvider, IEnumerable<Attribute> attributes)
            where T : ICustomAttributeProvider
        {
            return GetCustomAttribute(attributeProvider, attributes.Select(x => x.GetType()));
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{Attribute}"/> for <see cref="T"/> <see cref="attributeProvider"/>
        /// of type or derived from <see cref="attributes"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetCustomAttribute<T>(T attributeProvider, IEnumerable<Type> attributeTypes)
            where T : ICustomAttributeProvider
        {
            return GetCustomAttribute(attributeProvider)
                .Select(x => new Tuple<Type, Attribute>(x.GetType(), x))
                .Where(x =>
                    attributeTypes.Any(a => a.IsAssignableFrom(x.Item1))
                    || attributeTypes.Any(a => a == x.Item1))
                    .Select(x => x.Item2);
        }
    }
}
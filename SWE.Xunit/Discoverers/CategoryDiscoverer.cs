namespace SWE.Xunit.Discoverers
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Xunit.Abstractions;
    using global::Xunit.Sdk;

    using SWE.Xunit.Attributes;

    /// <summary>
    /// This class discovers all of the tests and test classes that have
    /// applied the Category attribute
    /// </summary>
    public class CategoryDiscoverer : ITraitDiscoverer
    {
        private const string CategoryTrait = "Category";

        /// <summary>
        /// Gets the trait values from the <see cref="CategoryAttribute"/>.
        /// </summary>
        /// <param name="traitAttribute">The trait attribute containing the trait values.</param>
        /// <returns>The trait values.</returns>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            string category;
            var attributeInfo = traitAttribute as ReflectionAttributeInfo;

            if (attributeInfo?.Attribute is CategoryAttribute categoryAttribute)
            {
                category = categoryAttribute.Category;
            }
            else
            {
                var constructorArguments = traitAttribute.GetConstructorArguments().ToArray();
                category = constructorArguments[0].ToString();
            }

            yield return new KeyValuePair<string, string>(CategoryTrait, category);
        }
    }
}

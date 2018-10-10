namespace SWE.Xunit.Attributes
{
    using global::Xunit.Sdk;
    using System;

    /// <summary>
    /// Apply this attribute to your Xunit test method to specify a <see cref="Category"/>.
    /// </summary>
    [TraitDiscoverer("SWE.Xunit.Discoverers.CategoryDiscoverer", "SWE.Xunit")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CategoryAttribute : Attribute, ITraitAttribute
    {
        public string Category { get; set; }

        public CategoryAttribute(string category)
        {
            Category = category;
        }
    }
}
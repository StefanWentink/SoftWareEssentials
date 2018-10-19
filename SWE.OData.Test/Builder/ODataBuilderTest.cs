namespace SWE.OData.Test.Builder
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.OData.Builders;
    using SWE.OData.Models;
    using SWE.OData.Test.Data;
    using SWE.Xunit.Attributes;
    using System;

    public class ODataBuilderTest
    {
        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicEntityString()
        {
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation)).Build();
            actual.Should().Be("relation");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicEntityKeyString()
        {
            var key = Guid.NewGuid();
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation), key).Build();
            actual.Should().Be($"relation({key})");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicEntityString_WithOrder()
        {
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetOrder(nameof(Relation.Name))
                .Build();
            actual.Should().Be("relation?$orderby=name");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicEntityString_WithOrderDescending()
        {
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetOrder(nameof(Relation.Name))
                .SetDescending(true)
                .Build();
            actual.Should().Be("relation?$orderby=name desc");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicEntityString_WithOrderDescendingAndSkip()
        {
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetOrder(nameof(Relation.Name))
                .SetDescending(true)
                .SetSkip(2)
                .Build();
            actual.Should().Be("relation?$orderby=name desc&$skip=2");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnBasicSubEntityKeyString()
        {
            var key = Guid.NewGuid();
            Action action = () => new ODataBuilder<Relation, Guid>(nameof(Relation), key)
                .Expand(new ODataKeyBuilder<Relation, int>(nameof(Order), 1))
                .Build();
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ThrowException_WhenKeyFilterAndFilter()
        {
            var key = Guid.NewGuid();
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson");

            Action action = () => new ODataBuilder<Relation, Guid>(nameof(Relation), key)
                .SetFilter(new ODataFilters(filter))
                .Build();

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnFilteredQueryWithExpand()
        {
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson");

            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetFilter(new ODataFilters(filter))
                .Expand(new ODataBuilder<Relation, Guid>(nameof(Order)))
                .Build();

            actual.Should().Be($"relation?$filter=name eq 'Johnson'&$expand=order");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnFilteredQueryWithExpand_With_EntityNameOnFilter()
        {
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson", "Person");

            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetFilter(new ODataFilters(filter))
                .Expand(new ODataBuilder<Relation, Guid>(nameof(Order)))
                .Build();

            actual.Should().Be($"relation?$filter=person/name eq 'Johnson'&$expand=order");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnFilteredQueryWithExpand_With_Filter()
        {
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson");
            var expand = new ODataBuilder<Relation, Guid>(nameof(Order));
            expand.SetFilter(filter);
            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetFilter(new ODataFilters(filter))
                .Expand(expand)
                .Build();

            actual.Should().Be($"relation?$filter=name eq 'Johnson'&$expand=order($filter=name eq 'Johnson')");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnFilteredQueryWithExpand_With_FilterAndOrder()
        {
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson");
            var expand = new ODataBuilder<Relation, Guid>(nameof(Order));
            expand.SetFilter(filter);
            expand.SetOrder<Order, int>(x => x.OrderNumber);

            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation))
                .SetFilter(new ODataFilters(filter))
                .Expand(expand)
                .Build();

            actual.Should().Be("relation?$filter=name eq 'Johnson'&$expand=order($filter=name eq 'Johnson';$orderby=ordernumber)");
        }

        [Fact]
        [Category("ODataBuilder")]
        public void ODataBuilder_Should_ReturnWithBasicFilter()
        {
            var key = Guid.NewGuid();
            var filter = new ODataFilterSelector<Relation, string>(x => x.Name, Enums.FilterOperator.Equal, "Johnson");

            var expand = new ODataBuilder<Relation, int>(nameof(Order));
            expand.SetFilter(new ODataFilters(filter));

            var actual = new ODataBuilder<Relation, Guid>(nameof(Relation), key)
                .Expand(expand)
                .Build();
            actual.Should().Be($"relation({key})?$expand=order($filter=name eq 'Johnson')");
        }
    }
}
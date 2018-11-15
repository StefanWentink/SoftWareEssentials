namespace SWE.Contract.Utilities
{
    using SWE.BasicType.Utilities;
    using SWE.Contract.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public static class OrderDirectiveUtilities
    {
        private static IEnumerable<OrderDirective> _orderDirectives;

        public static IEnumerable<OrderDirective> ValidOrderDirectives
        {
            get { return _orderDirectives ?? (_orderDirectives = GetOrderDirectives()); }
        }

        private static IEnumerable<OrderDirective> GetOrderDirectives()
        {
            return EnumUtilities.GetValues<OrderDirective>().Where(x => x != OrderDirective.Undefined);
        }
    }
}
namespace SWE.Contract.Utilities
{
    using SWE.BasicType.Utilities;
    using SWE.Contract.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public static class OperationDirectiveUtilities
    {
        private static IEnumerable<OperationDirective> _operationDirectives;

        public static IEnumerable<OperationDirective> ValidOperationDirectives
        {
            get { return _operationDirectives ?? (_operationDirectives = GetOperationDirectives()); }
        }

        private static IEnumerable<OperationDirective> GetOperationDirectives()
        {
            return EnumUtilities.GetValues<OperationDirective>().Where(x => x != OperationDirective.Undefined);
        }
    }
}
namespace SWE.Collection.Test.Data
{
    using System.Collections.Generic;

    internal static class CollectionFactory
    {
        internal const int StartingValue = 1234;

        internal static List<(int key, int value)> GetDefaultList()
        {
            return new List<(int key, int value)>
            {
                ( StartingValue, StartingValue ),
                ( StartingValue + 1, StartingValue ),
                ( StartingValue + 2, StartingValue + 1 )
            };
        }

        internal static (int key, int value) GetDefaultValue(int value)
        {
            return (StartingValue + 3, value);
        }
    }
}
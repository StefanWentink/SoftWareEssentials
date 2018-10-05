namespace SWE.EventSourcing.Events.Change
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Collection.Extensions;
    using System;
    using System.Collections.Generic;

    public class ChangeEvent<T> : BasicChangeEvent<T>
    {
        public ChangeEvent(IPropertyAction<T> propertyAction)
            : base(new List<IPropertyAction<T>> { propertyAction })
        {
        }

        public ChangeEvent(IEnumerable<IPropertyAction<T>> propertyActions)
            : base(propertyActions)
        {
        }
    }
}
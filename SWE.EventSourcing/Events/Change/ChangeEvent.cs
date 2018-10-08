namespace SWE.EventSourcing.Events.Change
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System.Collections.Generic;

    public class ChangeEvent<T>
        : BasicEvent<T>
        , IChangeEvent<T>
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
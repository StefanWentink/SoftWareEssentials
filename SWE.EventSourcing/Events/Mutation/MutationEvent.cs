namespace SWE.EventSourcing.Events.Mutation
{
    using SWE.EventSourcing.Interfaces;
    using SWE.EventSourcing.Interfaces.Events;
    using System.Collections.Generic;

    public class MutationEvent<T>
        : BasicEvent<T>
        , IMutationEvent<T>
    {
        public MutationEvent(IPropertyAction<T> propertyAction)
            : base(new List<IPropertyAction<T>> { propertyAction })
        {
        }

        public MutationEvent(IEnumerable<IPropertyAction<T>> propertyActions)
            : base(propertyActions)
        {
        }
    }
}
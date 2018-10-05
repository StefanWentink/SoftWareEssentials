namespace SWE.EventSourcing.Events.Mutation
{
    using SWE.EventSourcing.Interfaces;
    using SWE.Collection.Extensions;
    using System;
    using System.Collections.Generic;

    public class MutationEvent<T> : BasicMutationEvent<T>
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
namespace SWE.EventSourcing.Test.Containers
{
    using FluentAssertions;
    using global::Xunit;
    using SWE.EventSourcing.EventArgs;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.Model.Interfaces;
    using SWE.Xunit.Attributes;
    using System;
    using System.Collections.Generic;

    public abstract class BasicEventContainerTest<TEventContainer, TEventCollection, T, TKey, TEventKey>
        : BasicEventCollectionTest<TEventCollection, T, TKey, TEventKey>
        where TEventContainer : IBasicEventContainer<TEventCollection, T, TKey, TEventKey>, IEventSourcingHandler<T, TEventKey>, IDisposable
        where TEventCollection : IEventCollection<T, TEventKey>, IEventSourcingHandler<T, TEventKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
    {
        protected internal TEventContainer GetDefaultEventContainer(TKey itemId)
        {
            return GetDefaultEventContainer(new[] { itemId });
        }

        protected internal abstract TEventContainer GetDefaultEventContainer(IEnumerable<TKey> itemIds);

        [Fact]
        [Category("EventContainer")]
        public void Add_Should_AddItem_Without_EffectingItem()
        {
            var item = DefaultItem;
            var item2 = DefaultItem;

            var eventContainer = GetDefaultEventContainer(new[] { item.Id, item2.Id });
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventContainer.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventContainer.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);

            var newEvent = NewEvent;
            eventContainer.Add(newEvent, item);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();
            AssertItemOriginal(item);

            eventContainer.ApplyAll(item);
            AssertItemApplied(item);
            AssertItemOriginal(item2);
        }

        [Fact]
        [Category("EventContainer")]
        public void AddAndApply_Should_AddItem_With_EffectingItem()
        {
            var item = DefaultItem;
            var item2 = DefaultItem;

            var eventContainer = GetDefaultEventContainer(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventContainer.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventContainer.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);

            var newEvent = NewEvent;
            eventContainer.AddAndApply(newEvent, item);

            AssertItemApplied(item);
            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            eventContainer.ApplyAll(item2);
            eventContainer.RevertAll(item2);
            AssertItemApplied(item);

            eventContainer.RevertAll(item);
            AssertItemReverted(item);
            AssertItemOriginal(item2);
        }

        [Fact]
        [Category("EventContainer")]
        public void Remove_Should_AddItem_Without_EffectingItem()
        {
            var item = DefaultItem;
            var item2 = DefaultItem;

            var eventContainer = GetDefaultEventContainer(new[] { item.Id, item2.Id });
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventContainer.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventContainer.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);

            var newEvent = NewEvent;
            eventContainer.Add(newEvent, item);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();
            AssertItemOriginal(item);

            eventContainer.Remove(newEvent, item);
            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            AssertItemOriginal(item);

            eventContainer.ApplyAll(item);
            AssertItemOriginal(item);
            AssertItemOriginal(item2);
        }

        [Fact]
        [Category("EventContainer")]
        public void RemoveAndRevert_Should_AddItem_With_EffectingItem()
        {
            var item = DefaultItem;
            var item2 = DefaultItem;

            var eventContainer = GetDefaultEventContainer(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventContainer.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventContainer.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);

            var newEvent = NewEvent;
            eventContainer.Add(newEvent, item);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();
            AssertItemOriginal(item);

            eventContainer.RemoveAndRevert(newEvent, item);
            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            AssertItemReverted(item);

            eventContainer.ApplyAll(item);
            AssertItemReverted(item);

            eventContainer.RevertAll(item2);
            eventContainer.ApplyAll(item2);
            AssertItemOriginal(item2);
        }
    }
}
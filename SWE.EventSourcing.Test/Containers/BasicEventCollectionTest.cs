namespace SWE.EventSourcing.Test.Containers
{
    using SWE.Xunit.Attributes;
    using global::Xunit;
    using FluentAssertions;
    using System.Collections.Generic;
    using SWE.EventSourcing.Interfaces.Events;
    using System;
    using SWE.EventSourcing.EventArgs;
    using SWE.EventSourcing.Interfaces.Containers;
    using SWE.Model.Interfaces;

    public abstract class BasicEventCollectionTest<TEventCollection, T, TKey, TEventKey>
        where TEventCollection : IEventCollection<T, TEventKey>, IEventSourcingHandler<T, TEventKey>
        where T : IKey<TKey>
        where TKey : IEquatable<TKey>
        where TEventKey : IEquatable<TEventKey>
    {
        protected internal abstract T DefaultItem { get; }

        protected internal abstract IEvent<T, TEventKey> NewEvent { get; }

        protected internal abstract TEventCollection GetDefaultEventCollection(TKey itemId);

        protected abstract void AssertItemApplied(T item);

        protected abstract void AssertItemOriginal(T item);

        protected abstract void AssertItemReverted(T item);

        [Theory]
        [Category("OrderedEventCollection")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Insert_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var count = eventCollection.Count;
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);

            var newEvent = NewEvent;
            eventCollection.Contains(newEvent).Should().BeFalse();

            eventCollection.Insert(index, newEvent);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            AssertItemOriginal(item);
            eventCollection.Contains(newEvent).Should().BeTrue();
            eventCollection.IndexOf(newEvent).Should().Be(index);
            eventCollection.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("OrderedEventCollection")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void InsertAndApply_Should_InsertAtIndex_With_EffectingItem(int index)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Contains(newEvent).Should().BeFalse();

            eventCollection.InsertAndApply(index, newEvent, item);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            AssertItemApplied(item);
            eventCollection.Contains(newEvent).Should().BeTrue();
            eventCollection.IndexOf(newEvent).Should().Be(index);
            eventCollection.Count.Should().Be(count + 1);
        }

        [Theory]
        [Category("OrderedEventCollection")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveAtIndex_Without_EffectingItem(int index)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Insert(index, newEvent);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(newEvent).Should().BeTrue();

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            eventCollection.RemoveAt(index);

            AssertItemOriginal(item);
            eventCollection.Contains(newEvent).Should().BeFalse();
            eventCollection.Count.Should().Be(count);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
        }

        [Theory]
        [Category("OrderedEventCollection")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAndRevert_Should_RemoveAtIndex_With_EffectingItem(int index)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Insert(index, newEvent);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(newEvent).Should().BeTrue();

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            eventCollection.RemoveAndRevert(index, item);

            AssertItemReverted(item);
            eventCollection.Contains(newEvent).Should().BeFalse();
            eventCollection.Count.Should().Be(count);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
        }

        [Theory]
        [Category("OrderedEventCollection")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void RemoveAt_Should_RemoveIfPresent_Without_EffectingItem(int index)
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Insert(index, newEvent);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(newEvent).Should().BeTrue();

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            eventCollection.Remove(newEvent).Should().BeTrue();
            eventCollection.Contains(newEvent).Should().BeFalse();
            eventCollection.Remove(newEvent).Should().BeFalse();

            AssertItemOriginal(item);
            eventCollection.Count.Should().Be(count);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
        }

        [Fact]
        [Category("OrderedEventCollection")]
        public void RemoveAndRevert_Should_RemoveIfPresent_With_EffectingItem()
        {
            var item = DefaultItem;
            var eventCollection = GetDefaultEventCollection(item.Id);
            var addedIds = new List<TEventKey>();
            var removedIds = new List<TEventKey>();
            eventCollection.EventAdded += (object sender, EventSourcingArgs<T, TEventKey> e) => addedIds.Add(e.Event.Id);
            eventCollection.EventRemoved += (object sender, EventSourcingArgs<T, TEventKey> e) => removedIds.Add(e.Event.Id);
            var count = eventCollection.Count;

            var newEvent = NewEvent;
            eventCollection.Add(newEvent);
            eventCollection.Count.Should().Be(count + 1);
            eventCollection.Contains(newEvent).Should().BeTrue();

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().BeEmpty();

            eventCollection.RemoveAndRevert(newEvent, item).Should().BeTrue();
            eventCollection.Contains(newEvent).Should().BeFalse();

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);

            eventCollection.RemoveAndRevert(newEvent, item).Should().BeFalse();

            AssertItemReverted(item);
            eventCollection.Count.Should().Be(count);

            addedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
            removedIds.Should().ContainSingle().Which.Should().Be(newEvent.Id);
        }
    }
}
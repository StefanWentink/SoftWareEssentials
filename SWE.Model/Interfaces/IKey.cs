namespace SWE.Model.Interfaces
{
    using System;

    /// <summary>
    /// Implements unique identifier <see cref="IKey{TKey}.Id"/> of type <see cref="Guid"/>
    /// </summary>
    public interface IKey : IKey<Guid>
    {
    }

    /// <summary>
    /// Implements unique identifier <see cref="IKey{TKey}.Id"/> of type <see cref="TKey"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IKey<out TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        TKey Id { get; }
    }
}
namespace SWE.EventSourcing.Interfaces
{
    public interface IPropertyChange<TValue>
    {
        /// <summary>
        /// <see cref="TValue"/> of previous state.
        /// </summary>
        TValue PreviousValue { get; set; }

        /// <summary>
        /// <see cref="TValue"/> of state.
        /// </summary>
        TValue Value { get; set; }
    }
}
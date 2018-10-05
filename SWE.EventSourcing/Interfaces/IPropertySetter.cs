namespace SWE.EventSourcing.Interfaces
{
    public interface IPropertyChange<TValue>
    {
        TValue PreviousValue { get; set; }

        TValue Value { get; set; }
    }
}
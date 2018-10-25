namespace SWE.AnomalyAnalysis.EventArgs
{
    using SWE.AnomalyAnalysis.Enums;

    public class ValueChangedEventArgs<TValue>
    {
        public TValue Value { get; }

        public Trigger Trigger { get; }

        public ValueChangedEventArgs(TValue value, Trigger trigger)
        {
            Value = value;
            Trigger = trigger;
        }
    }
}
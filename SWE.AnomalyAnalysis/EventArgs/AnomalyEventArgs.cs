namespace SWE.AnomalyAnalysis.EventArgs
{
    using SWE.AnomalyAnalysis.Enums;

    public class AnomalyEventArgs<TValue>
    {
        public TValue Value { get; }

        public Anomaly Anomaly { get; }

        public AnomalyEventArgs(TValue value, Anomaly anomaly)
        {
            Value = value;
            Anomaly = anomaly;
        }
    }
}
namespace SWE.Builder.Models
{
    using SWE.Builder.Interfaces;

    public abstract class Builder<TOut, TIn> : IBuilder<TOut, TIn>
    {
        public virtual TOut Build()
        {
            return BuildResult();
        }

        protected abstract TOut BuildResult();
    }
}

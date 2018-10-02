namespace SWE.Builder.Models
{
    using SWE.Builder.Interfaces;

    public abstract class MemberBuilder<TOut, TIn> : Builder<TOut, TIn>, IMemberBuilder<TOut, TIn>
        where TOut : TIn
    {
        protected TIn Member { get; private set; }

        protected MemberBuilder()
        {
        }

        protected MemberBuilder(TIn member)
        {
            Member = member;
        }

        public sealed override TOut Build()
        {
            if (Member == default)
            {
                return default;
            }

            return BuildResult();
        }

        public virtual IBuilder<TOut, TIn> SetMember(TIn member)
        {
            Member = member;
            return this;
        }
    }
}
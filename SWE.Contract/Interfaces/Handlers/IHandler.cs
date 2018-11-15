namespace SWE.Contract.Interfaces.Handlers
{
    using System;

    public interface IHandler<in T> : IDisposable
    {
        void Execute(T value);
    }
}
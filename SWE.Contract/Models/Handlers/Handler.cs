namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.Interfaces.Handlers;
    using System;

    public abstract class Handler<T>
        : IHandler<T>

    {
        protected bool IsDisposed { get; set; }

        public abstract void Execute(T value);

        ~Handler()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool isDisposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                if (isDisposing)
                {
                    DisposeHandler();
                }
            }
        }

        protected abstract void DisposeHandler();
    }
}
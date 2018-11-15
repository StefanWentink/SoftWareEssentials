namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.EventArgs;
    using SWE.Contract.Interfaces.Handlers;
    using System;
    using System.Threading.Tasks;

    public abstract class Handler<T>
        : IHandler<T>
        , IDisposable
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
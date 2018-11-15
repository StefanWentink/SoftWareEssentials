namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.EventArgs;
    using SWE.Contract.Interfaces.Handlers;
    using System.Collections.Generic;
    using System.Linq;

    public class HandlerContainer<T> : ValidationResultHandler<T>
    {
        private IEnumerable<IHandler<T>> Handlers { get; set; }

        public HandlerContainer()
            : this(new List<IHandler<T>>())
        {
        }

        public HandlerContainer(IHandler<T> handler)
            : this(new List<IHandler<T>> { handler })
        {
        }

        public HandlerContainer(IEnumerable<IHandler<T>> handlers)
        {
            Handlers = handlers ?? new List<IHandler<T>>();

            foreach (ValidationResultHandler<T> handler in Handlers/*.Where(x => x is ValidationResultHandler<T>)*/)
            {
                handler.InvalidResult += OnInvalidResult_Raised;
            }
        }

        public override void Execute(T value)
        {
            foreach (var handler in Handlers)
            {
                handler?.Execute(value);
            }
        }

        protected override void DisposeHandler()
        {
            foreach (var handler in Handlers)
            {
                if (handler is ValidationResultHandler<T> validationHandler)
                {
                    validationHandler.InvalidResult -= OnInvalidResult_Raised;
                }

                handler?.Dispose();
            }

            Handlers = null;
        }

        private void OnInvalidResult_Raised(object sender, ValidationHandlerArgs e)
        {
            if (e.ValidationResult != default)
            {
                OnInvalidResult(e);
            }
        }
    }
}
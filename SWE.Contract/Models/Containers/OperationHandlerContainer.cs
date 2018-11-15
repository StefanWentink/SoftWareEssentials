namespace SWE.Contract.Models.Containers
{
    using SWE.Contract.Enums;
    using SWE.Contract.Interfaces.Containers;
    using SWE.Contract.Interfaces.Handlers;
    using SWE.Contract.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OperationHandlerContainer<T>
        : IOperationHandlerContainer<T>
        , IDisposable
    {
        private Dictionary<OrderDirective, Dictionary<OperationDirective, HandlerContainer<T>>> HandlerContainers { get; set; }

        protected bool IsDisposed { get; set; }

        public OperationHandlerContainer(IOperationHandler<T> handler)
            : this(new[] { handler })
        {
        }

        public OperationHandlerContainer(IEnumerable<IOperationHandler<T>> handlers)
        {
            HandlerContainers = new Dictionary<OrderDirective, Dictionary<OperationDirective, HandlerContainer<T>>>();

            foreach (var orderDirective in OrderDirectiveUtilities.ValidOrderDirectives)
            {
                var dictionary = new Dictionary<OperationDirective, HandlerContainer<T>>();

                foreach (var operationDirective in OperationDirectiveUtilities.ValidOperationDirectives)
                {
                    dictionary.Add(
                        operationDirective,
                        new HandlerContainer<T>(
                            handlers
                                .Where(
                                x => x.OrderDirective == orderDirective
                                && x.OperationDirectives.Contains(operationDirective))
                                .Select(x => x.Handler)));
                }

                HandlerContainers.Add(orderDirective, dictionary);
            }
        }

        public IValidationResultHandler<T> GetHandlerContainer(OrderDirective orderDirective, OperationDirective operationDirective)
        {
            if (HandlerContainers.ContainsKey(orderDirective)
                && HandlerContainers[orderDirective].ContainsKey(operationDirective))
            {
                return HandlerContainers[orderDirective][operationDirective];
            }

            return null;
        }

        ~OperationHandlerContainer()
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
                    if (HandlerContainers != null)
                    {
                        foreach (var orderDirective in OrderDirectiveUtilities.ValidOrderDirectives)
                        {
                            foreach (var operationDirective in OperationDirectiveUtilities.ValidOperationDirectives)
                            {
                                GetHandlerContainer(orderDirective, operationDirective)?.Dispose();
                            }
                        }

                        HandlerContainers = null;
                    }
                }
            }
        }
    }
}
namespace SWE.Contract.Models.Handlers
{
    using SWE.Contract.Enums;
    using SWE.Contract.Interfaces.Handlers;
    using SWE.Contract.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OperationHandler<T> : IOperationHandler<T>
    {
        public IEnumerable<OperationDirective> OperationDirectives { get; }

        public OrderDirective OrderDirective { get; }

        public IHandler<T> Handler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationHandler"/> class.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="orderDirective"></param>
        /// <exception cref="ArgumentNullException">If <see cref="handler"/> is null.</exception>
        /// <exception cref="ArgumentException">If <see cref="orderDirective"/> is not a valid value.</exception>
        public OperationHandler(
            IHandler<T> handler,
            OrderDirective orderDirective)
            : this(
                  handler,
                  orderDirective,
                  new[] { OperationDirective.Create, OperationDirective.Update, OperationDirective.Delete })
        {
        }

        /// <summary>
        ///
        /// Initializes a new instance of the <see cref="OperationHandler"/> class.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="orderDirective"></param>
        /// <param name="operationDirective"></param>
        /// <exception cref="ArgumentNullException">If <see cref="handler"/> is null.</exception>
        /// <exception cref="ArgumentException">If <see cref="orderDirective"/> is not a valid value.</exception>
        /// <exception cref="ArgumentException">If <see cref="operationDirective"/> is not a valid value.</exception>
        public OperationHandler(
            IHandler<T> handler,
            OrderDirective orderDirective,
            OperationDirective operationDirective)
            : this(handler, orderDirective, new[] { operationDirective })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationHandler"/> class.
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="orderDirective"></param>
        /// <param name="operationDirectives"></param>
        /// <exception cref="ArgumentNullException">If <see cref="handler"/> is null.</exception>
        /// <exception cref="ArgumentException">If <see cref="orderDirective"/> is not a valid value.</exception>
        /// <exception cref="ArgumentException">If <see cref="operationDirective"/> contains no valid values.</exception>
        public OperationHandler(
            IHandler<T> handler,
            OrderDirective orderDirective,
            IEnumerable<OperationDirective> operationDirectives)
        {
            Handler = handler ??
                throw new ArgumentNullException(nameof(handler), $"{nameof(handler)} must not be null.");

            if (!OrderDirectiveUtilities.ValidOrderDirectives.Contains(orderDirective))
            {
                throw new ArgumentException($"{orderDirective} is not a valid value.", nameof(orderDirective));
            }

            OrderDirective = orderDirective;
            OperationDirectives = operationDirectives.Intersect(OperationDirectiveUtilities.ValidOperationDirectives);

            if ((OperationDirectives?.Count() ?? 0) <= 0)
            {
                throw new ArgumentException($"{nameof(operationDirectives)} contains no valid values.", nameof(operationDirectives));
            }
        }
    }
}
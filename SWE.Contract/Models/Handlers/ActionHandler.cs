namespace SWE.Contract.Models.Handlers
{
    using System;

    public class ActionHandler<T> : Handler<T>
    {
        private Func<T, bool> ActionCondition { get; set; }

        private Action<T> Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionHandler"/> class.
        /// </summary>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException">If <see cref="action"/> is null.</exception>
        public ActionHandler(Action<T> action)
            : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionHandler"/> class.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="actionCondition"></param>
        /// <exception cref="ArgumentNullException">If <see cref="action"/> is null.</exception>
        public ActionHandler(Action<T> action, Func<T, bool> actionCondition)
        {
            ActionCondition = actionCondition;

            Action = action ??
                throw new ArgumentNullException(nameof(action), $"{nameof(action)} must not be null.");
        }

        public override void Execute(T value)
        {
            if (ActionCondition == null || ActionCondition(value))
            {
                Action(value);
            }
        }

        protected override void DisposeHandler()
        {
            Action = null;
            ActionCondition = null;
        }
    }
}
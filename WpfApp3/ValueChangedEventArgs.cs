namespace WpfApp3
{
    #region

    using System;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     The ValueChangedEventArgs class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.EventArgs" />
    public class ValueChangedEventArgs<T> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueChangedEventArgs{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ValueChangedEventArgs([NotNull] string name, [CanBeNull] T oldValue, [CanBeNull] T newValue)
        {
            this.Name = name;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [NotNull]
        public string Name { get; }

        /// <summary>
        ///     Gets the old value.
        /// </summary>
        /// <value>
        ///     The old value.
        /// </value>
        [CanBeNull]
        public T OldValue { get; }

        /// <summary>
        ///     Creates new value.
        /// </summary>
        /// <value>
        ///     The new value.
        /// </value>
        [CanBeNull]
        public T NewValue { get; }
    }
}
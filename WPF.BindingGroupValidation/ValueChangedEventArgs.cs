// -----------------------------------------------------------------------
// <copyright file="ValueChangedEventArgs.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation
{
    using System;

    using JetBrains.Annotations;

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
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
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
// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyWatcher{T}..cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    ///     Dependency Property Watcher class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Windows.DependencyObject" />
    /// <seealso cref="System.IDisposable" />
    public class DependencyPropertyWatcher<T> : DependencyObject, IDisposable
    {
        /// <summary>
        ///     The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(object),
            typeof(DependencyPropertyWatcher<T>),
            new PropertyMetadata(null, OnPropertyChanged));

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyWatcher{T}" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyPath">The property path.</param>
        public DependencyPropertyWatcher(DependencyObject target, string propertyPath)
        {
            this.Target = target;
            var path = new PropertyPath(propertyPath);
            BindingOperations.SetBinding(
                this,
                ValueProperty,
                new Binding { Source = target, Path = path, Mode = BindingMode.OneWay });
        }

        /// <summary>
        ///     Occurs when [property changed].
        /// </summary>
        public event EventHandler PropertyChanged;

        /// <summary>
        ///     Gets the target.
        /// </summary>
        /// <value>
        ///     The target.
        /// </value>
        public DependencyObject Target { get; }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public T Value => (T)this.GetValue(ValueProperty);

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        public static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var source = (DependencyPropertyWatcher<T>)sender;

            source.PropertyChanged?.Invoke(source, EventArgs.Empty);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ClearValue(ValueProperty);
            }
        }
    }
}
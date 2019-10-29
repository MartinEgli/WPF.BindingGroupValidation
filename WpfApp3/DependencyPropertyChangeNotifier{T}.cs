namespace WpfApp3
{
    #region

    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     The generic DependencyPropertyChangeNotifier class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="DependencyPropertyChangeNotifier{Object}" />
    public class DependencyPropertyChangeNotifier<T> : DependencyObject, IDisposable
    {
        /// <summary>
        ///     The value property
        /// </summary>
        [NotNull]
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(T),
            typeof(DependencyPropertyChangeNotifier<T>),
            new FrameworkPropertyMetadata(default(T), OnPropertyChanged));

        /// <summary>
        ///     The property source
        /// </summary>
        [NotNull]
        private readonly WeakReference propertySource;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier{T}" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="path">The path.</param>
        public DependencyPropertyChangeNotifier([NotNull] DependencyObject propertySource, [NotNull] string path)
            : this(propertySource, new PropertyPath(path))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier{T}" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="property">The property.</param>
        public DependencyPropertyChangeNotifier(
            [NotNull] DependencyObject propertySource,
            [NotNull] DependencyProperty property)
            : this(propertySource, new PropertyPath(property))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier{T}" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="property">The property.</param>
        /// <exception cref="ArgumentNullException">
        ///     propertySource
        ///     or
        ///     property
        /// </exception>
        public DependencyPropertyChangeNotifier(
            [NotNull] DependencyObject propertySource,
            [NotNull] PropertyPath property)
        {
            if (propertySource == null) throw new ArgumentNullException(nameof(propertySource));
            if (property == null) throw new ArgumentNullException(nameof(property));
            this.propertySource = new WeakReference(propertySource);
            var binding = new Binding { Path = property, Mode = BindingMode.OneWay, Source = propertySource };
            BindingOperations.SetBinding(this, ValueProperty, binding);
        }

        /// <summary>
        ///     Gets the property source.
        /// </summary>
        /// <value>
        ///     The property source.
        /// </value>
        [CanBeNull]
        public DependencyObject PropertySource
        {
            get
            {
                try
                {
                    return this.propertySource.IsAlive ? this.propertySource.Target as DependencyObject : null;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     Returns/sets the value of the property
        /// </summary>
        /// <seealso cref="ValueProperty" />
        [Bindable(true)]
        [CanBeNull]
        public T Value => (T)this.GetValue(ValueProperty);

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Occurs when [dependency property changed].
        /// </summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> DependencyPropertyChanged;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;

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
                BindingOperations.ClearBinding(this, ValueProperty);
            }
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnPropertyChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            var notifier = (DependencyPropertyChangeNotifier<T>)dependencyObject;
            notifier.ValueChanged?.Invoke(
                notifier,
                new ValueChangedEventArgs<T>(args.Property.Name, (T)args.OldValue, (T)args.NewValue));
            notifier.DependencyPropertyChanged?.Invoke(notifier, args);
        }
    }
}
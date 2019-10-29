namespace WpfApp3
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class DependencyPropertyWatcher<T> : DependencyObject, IDisposable
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(DependencyPropertyWatcher<T>),
            new PropertyMetadata(null, OnPropertyChanged));

        public DependencyPropertyWatcher(DependencyObject target, string propertyPath)
        {
            this.Target = target;
            BindingOperations.SetBinding(
                this,
                ValueProperty,
                new Binding
                {
                    Source = target,
                    Path = new PropertyPath(propertyPath),
                    Mode = BindingMode.OneWay,
                });
        }

        public DependencyObject Target { get; private set; }

        public T Value => (T)this.GetValue(ValueProperty);

        public void Dispose()
        {
            this.ClearValue(ValueProperty);
        }

        public event EventHandler PropertyChanged;

        public static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var source = (DependencyPropertyWatcher<T>)sender;

            if (source.PropertyChanged != null)
            {
                source.PropertyChanged(source, EventArgs.Empty);
            }
        }
    }

    public class DependencyPropertyWatcher : DependencyPropertyWatcher<object>
    {
        public DependencyPropertyWatcher(DependencyObject target, string propertyPath)
            : base(target, propertyPath)
        {
        }
    }
}
namespace WpfApp3
{
    #region

    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Data;

    #endregion

    public class ExtendedBindingGroup : BindingGroup
    {
        public ExtendedBindingGroup()
        {
            ((INotifyCollectionChanged)this.BindingExpressions).CollectionChanged += this.OnBindingsChanged;
        }

        protected override bool ShouldSerializeProperty(DependencyProperty dp)
        {
            return base.ShouldSerializeProperty(dp);
        }

        private void OnBindingsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        if (item is BindingExpression expression)
                        {
                            var binding = expression.ParentBinding;

                            var s = expression.ParentBinding.Source;
                            var d = expression.DataItem;
                            var rs = expression.ResolvedSource;

                            var elementName = expression.ParentBinding.ElementName;
                            var propertyName = expression.ParentBinding.Path.Path;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }
    }
}
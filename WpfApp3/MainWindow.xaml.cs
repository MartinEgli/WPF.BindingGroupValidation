namespace WpfApp3
{
    #region

    using System;
    using System.ComponentModel;
    using System.Windows;

    #endregion

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isDirty;

        private DependencyPropertyChangeNotifier notifier;

        private DependencyPropertyWatcher<string> watcher;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        public bool IsDirty
        {
            get
            {
                return this._isDirty;
            }

            set
            {
                this._isDirty = value;
                this.OnChanged(nameof(this.IsDirty));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChanged(string propertyname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            watcher = new DependencyPropertyWatcher<string>(this.tbNumber, "Text");
            watcher.PropertyChanged += this.WatcherOnPropertyChanged;

            notifier = new DependencyPropertyChangeNotifier(this.tbNumber2, "Text");
            notifier.ValueChanged += this.NOnValueChanged;
            //   this.MyBindingGroup.BeginEdit(); // Not really needed?
            //this.gridMain.KeyUp += this.GridMain_KeyUp;
        }

        private void NOnValueChanged(object sender, EventArgs eventArgs)
        {
        }

        private void WatcherOnPropertyChanged(object sender, EventArgs eventArgs)
        {
        }

        //private void GridMain_KeyUp(object sender, KeyEventArgs e)
        //{
        //    this.MyBindingGroup.NotifyOnValidationError = true;
        //    var x = this.MyBindingGroup.ValidateWithoutUpdate();

        //    if (this.MyBindingGroup.IsDirty)
        //    {
        //        this.IsDirty = true;
        //    }
        //}

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.BindingGroup.CommitEdit())
            {
                var vm = (EmployeeViewModel)this.DataContext;
                vm.Save();
                this.IsDirty = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.BindingGroup.CancelEdit();
            this.IsDirty = false;
        }
    }
}
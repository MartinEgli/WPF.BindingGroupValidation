// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation.TestGui
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        ///     The is dirty
        /// </summary>
        private bool isDirty;

        /// <summary>
        ///     The notifier
        /// </summary>
        private DependencyPropertyChangeNotifier notifier;

        /// <summary>
        ///     The watcher
        /// </summary>
        private DependencyPropertyWatcher<string> watcher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is dirty.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty
        {
            get => this.isDirty;

            set
            {
                if (this.isDirty == value)
                {
                    return;
                }

                this.isDirty = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Called when [changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.watcher = new DependencyPropertyWatcher<string>(this.Number1TextBox, "Text");
            this.watcher.PropertyChanged += this.WatcherOnPropertyChanged;

            this.notifier = new DependencyPropertyChangeNotifier(this.Number1TextBox, "Text");
            this.notifier.ValueChanged += this.OnValueChanged;
        }

        /// <summary>
        ///     Called when [value changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnValueChanged(object sender, EventArgs eventArgs)
        {
        }

        /// <summary>
        ///     Watchers the on property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void WatcherOnPropertyChanged(object sender, EventArgs eventArgs)
        {
        }
    }
}
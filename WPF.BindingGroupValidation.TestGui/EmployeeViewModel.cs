// -----------------------------------------------------------------------
// <copyright file="EmployeeViewModel.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation.TestGui
{
    using System.Windows.Data;
    using System.Windows.Input;

    public class EmployeeViewModel : Bindable
    {
        private BindingGroup bindingGroup;

        private EmployeeModel employee;

        /// <summary>
        ///     The is dirty
        /// </summary>
        private bool isDirty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmployeeViewModel" /> class.
        /// </summary>
        public EmployeeViewModel()
        {
            this.CancelCommand = new RelayCommand(this.OnCancel, null);
            this.SaveCommand = new RelayCommand(this.OnSave, this.CanSave);
            this.LoadData();
        }

        /// <summary>
        ///     Gets or sets the employee.
        /// </summary>
        /// <value>
        ///     The employee.
        /// </value>
        public EmployeeModel Employee
        {
            get => this.employee;
            set
            {
                if (Equals(value, this.employee))
                {
                    return;
                }

                this.employee = value;
                this.OnPropertyChanged();
            }
        }

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
                if (value == this.isDirty)
                {
                    return;
                }

                this.isDirty = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the save button click command.
        /// </summary>
        /// <value>
        ///     The save button click command.
        /// </value>
        public ICommand SaveCommand { get; }

        /// <summary>
        ///     Gets the cancel command.
        /// </summary>
        /// <value>
        ///     The cancel command.
        /// </value>
        public ICommand CancelCommand { get; }

        /// <summary>
        ///     Gets or sets the binding group.
        /// </summary>
        /// <value>
        ///     The binding group.
        /// </value>
        public BindingGroup BindingGroup
        {
            get => this.bindingGroup;
            set
            {
                if (Equals(value, this.bindingGroup))
                {
                    return;
                }

                var group = this.bindingGroup;
                group?.CancelEdit();
                this.bindingGroup = value;
                value?.BeginEdit();
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Determines whether this instance can save the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can save the specified object; otherwise, <c>false</c>.
        /// </returns>
        private bool CanSave(object obj)
        {
            var group = this.BindingGroup;
            return group != null && group.ValidateWithoutUpdate();
        }

        /// <summary>
        ///     Called when [save].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnSave(object obj)
        {
            var group = this.BindingGroup;
            group?.CommitEdit();
        }

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnCancel(object obj)
        {
            var group = this.BindingGroup;
            group?.CancelEdit();
        }

        /// <summary>
        ///     Loads the data.
        /// </summary>
        private void LoadData()
        {
            this.Employee = new EmployeeModel { Firstname = "Billy", Lastname = "Wilder" };
        }
    }
}
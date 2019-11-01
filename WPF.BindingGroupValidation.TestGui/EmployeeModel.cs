// -----------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation.TestGui
{
    /// <summary>
    ///     Employee Model class.
    /// </summary>
    /// <seealso cref="Bindable" />
    public class EmployeeModel : Bindable
    {
        /// <summary>
        ///     The firstname
        /// </summary>
        private string firstname;

        /// <summary>
        ///     The is dirty
        /// </summary>
        private bool isDirty;

        /// <summary>
        ///     The lastname
        /// </summary>
        private string lastname;

        /// <summary>
        ///     The number
        /// </summary>
        private int number;

        /// <summary>
        ///     Gets or sets the number.
        /// </summary>
        /// <value>
        ///     The number.
        /// </value>
        public int Number
        {
            get => this.number;

            set
            {
                if (value == this.number)
                {
                    return;
                }

                this.number = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the firstname.
        /// </summary>
        /// <value>
        ///     The firstname.
        /// </value>
        public string Firstname
        {
            get => this.firstname;

            set
            {
                if (value == this.firstname)
                {
                    return;
                }

                this.firstname = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the lastname.
        /// </summary>
        /// <value>
        ///     The lastname.
        /// </value>
        public string Lastname
        {
            get => this.lastname;

            set
            {
                if (value == this.lastname)
                {
                    return;
                }

                this.lastname = value;
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
    }
}
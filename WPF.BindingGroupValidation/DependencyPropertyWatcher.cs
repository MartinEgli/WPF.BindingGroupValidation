// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyWatcher.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation
{
    using System.Windows;

    /// <summary>
    ///     DependencyPropertyWatcher
    /// </summary>
    /// <seealso cref="System.Windows.DependencyObject" />
    /// <seealso cref="System.IDisposable" />
    public class DependencyPropertyWatcher : DependencyPropertyWatcher<object>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyWatcher" /> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyPath">The property path.</param>
        public DependencyPropertyWatcher(DependencyObject target, string propertyPath)
            : base(target, propertyPath)
        {
        }
    }
}
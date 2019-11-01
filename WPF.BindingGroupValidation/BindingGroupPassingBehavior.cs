// -----------------------------------------------------------------------
// <copyright file="BindingGroupPassingBehavior.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace WPF.BindingGroupValidation
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Data;

    using Microsoft.Xaml.Behaviors;

    public class BindingGroupPassingBehavior : Behavior<DependencyObject>
    {
        /// <summary>
        ///     The target binding group property key
        /// </summary>
        public static readonly DependencyProperty TargetBindingGroupProperty = DependencyProperty.Register(
            nameof(TargetBindingGroup),
            typeof(BindingGroup),
            typeof(BindingGroupPassingBehavior),
            new PropertyMetadata(default(BindingGroup)));

        /// <summary>
        ///     The source binding group property
        /// </summary>
        public static readonly DependencyProperty SourceBindingGroupProperty = DependencyProperty.Register(
            nameof(SourceBindingGroup),
            typeof(BindingGroup),
            typeof(BindingGroupPassingBehavior),
            new PropertyMetadata(default(BindingGroup)));

        /// <summary>
        ///     The disposables
        /// </summary>
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        /// <summary>
        ///     Gets or sets the target binding group.
        /// </summary>
        /// <value>
        ///     The target binding group.
        /// </value>
        public BindingGroup TargetBindingGroup
        {
            get => (BindingGroup)this.GetValue(TargetBindingGroupProperty);
            set => this.SetValue(TargetBindingGroupProperty, value);
        }

        /// <summary>
        ///     Gets or sets the source binding group.
        /// </summary>
        /// <value>
        ///     The source binding group.
        /// </value>
        public BindingGroup SourceBindingGroup
        {
            get => (BindingGroup)this.GetValue(SourceBindingGroupProperty);
            set => this.SetValue(SourceBindingGroupProperty, value);
        }

        /// <summary>
        ///     Gets or sets the framework element.
        /// </summary>
        /// <value>
        ///     The framework element.
        /// </value>
        public FrameworkElement FrameworkElement { get; set; }

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            if (this.AssociatedObject is FrameworkElement frameworkElement)
            {
                this.FrameworkElement = frameworkElement;
                this.FrameworkElement.Loaded += this.OnLoaded;
            }
            else if (this.SourceBindingGroup == null)
            {
                if (this.AssociatedObject is BindingGroup bindingGroup)
                {
                    this.SourceBindingGroup = bindingGroup;
                    if (bindingGroup.Owner is FrameworkElement owner)
                    {
                        this.FrameworkElement = owner;
                        this.FrameworkElement.Loaded += this.OnLoaded;
                    }
                }
            }
            else
            {
                if (this.SourceBindingGroup.Owner is FrameworkElement owner)
                {
                    this.FrameworkElement = owner;
                    this.FrameworkElement.Loaded += this.OnLoaded;
                }

                this.TargetBindingGroup = this.SourceBindingGroup;
            }

            base.OnAttached();
        }

        /// <summary>
        ///     Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (this.SourceBindingGroup == null)
            {
                var frameworkElement = this.FrameworkElement;
                if (frameworkElement?.BindingGroup != null)
                {
                    this.SourceBindingGroup = frameworkElement.BindingGroup;
                }
            }

            this.TargetBindingGroup = this.SourceBindingGroup;
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            if (this.FrameworkElement != null)
            {
                this.FrameworkElement.Loaded -= this.OnLoaded;
                this.FrameworkElement = null;
            }

            this.disposables.Reverse();
            foreach (var disposable in this.disposables)
            {
                disposable.Dispose();
            }

            this.disposables.Clear();

            base.OnDetaching();
        }
    }
}
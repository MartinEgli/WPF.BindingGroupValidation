// -----------------------------------------------------------------------
// <copyright file="BindingGroupValidationOnPropertyChangedBehavior.cs" company="bfa solutions ltd">
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

    /// <summary>
    ///     BindingGroupValidationOnPropertyChangedBehavior class.
    /// </summary>
    /// <seealso cref="Behavior{T}" />
    public class BindingGroupValidationOnPropertyChangedBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        ///     The binding group property
        /// </summary>
        public static readonly DependencyProperty BindingGroupProperty = DependencyProperty.Register(
            nameof(BindingGroup),
            typeof(BindingGroup),
            typeof(BindingGroupValidationOnPropertyChangedBehavior),
            new PropertyMetadata(default(BindingGroup)));

        /// <summary>
        ///     The is dirty property
        /// </summary>
        public static readonly DependencyProperty IsDirtyProperty = DependencyProperty.Register(
            nameof(IsDirty),
            typeof(bool),
            typeof(BindingGroupValidationOnPropertyChangedBehavior),
            new FrameworkPropertyMetadata(default(bool), PropertyChangedCallback) { BindsTwoWayByDefault = true });

        /// <summary>
        ///     The is valid property
        /// </summary>
        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register(
            "IsValid",
            typeof(bool),
            typeof(BindingGroupValidationOnPropertyChangedBehavior),
            new FrameworkPropertyMetadata(default(bool), PropertyChangedCallback) { BindsTwoWayByDefault = true });

        /// <summary>
        ///     The disposables
        /// </summary>
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        /// <summary>
        ///     Gets or sets the binding group.
        /// </summary>
        /// <value>
        ///     The binding group.
        /// </value>
        public BindingGroup BindingGroup
        {
            get => (BindingGroup)this.GetValue(BindingGroupProperty);
            set => this.SetValue(BindingGroupProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is dirty.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is dirty; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty
        {
            get => (bool)this.GetValue(IsDirtyProperty);
            // ReSharper disable once ValueParameterNotUsed
            set
            {
            }
        }

        /// <summary>
        ///     Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get => (bool)this.GetValue(IsValidProperty);
            // ReSharper disable once ValueParameterNotUsed
            set
            {
            }
        }

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += this.OnLoaded;
            base.OnAttached();
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            this.disposables.Reverse();
            foreach (var disposable in this.disposables)
            {
                disposable.Dispose();
            }

            this.disposables.Clear();

            base.OnDetaching();
        }

        /// <summary>
        ///     Properties the changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">
        ///     The <see cref="DependencyPropertyChangedEventArgs" /> instance
        ///     containing the event data.
        /// </param>
        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
        }

        /// <summary>
        ///     Notifiers the on value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ValueChangedEventArgs{Object}" /> instance containing the event data.</param>
        private void NotifierOnValueChanged(object sender, ValueChangedEventArgs<object> e)
        {
            this.UpdateState();
        }

        /// <summary>
        ///     Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.AssociatedObject.Loaded -= this.OnLoaded;

            if (this.BindingGroup == null)
            {
                var frameworkElement = this.AssociatedObject;
                if (frameworkElement.BindingGroup == null)
                {
                    return;
                }

                this.BindingGroup = frameworkElement.BindingGroup;
            }

            foreach (var item in this.BindingGroup.BindingExpressions)
            {
                if (!(item is BindingExpression expression))
                {
                    continue;
                }

                var target = expression.Target;
                if (target == null)
                {
                    continue;
                }

                var name = expression.TargetProperty?.Name;
                if (name == null)
                {
                    continue;
                }

                var notifier = new DependencyPropertyChangeNotifier(target, name);
                this.disposables.Add(notifier);

                notifier.ValueChanged += this.NotifierOnValueChanged;
                this.disposables.Add(new Disposable(() => notifier.ValueChanged -= this.NotifierOnValueChanged));
            }

            this.UpdateState();
        }

        /// <summary>
        ///     Updates the state.
        /// </summary>
        /// <returns></returns>
        private bool UpdateState()
        {
            var bindingGroup = this.BindingGroup;
            var isValid = bindingGroup != null && bindingGroup.ValidateWithoutUpdate();
            this.SetValue(IsDirtyProperty, !isValid);
            this.SetValue(IsValidProperty, isValid);
            return isValid;
        }
    }
}
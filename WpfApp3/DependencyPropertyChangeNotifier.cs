namespace WpfApp3
{
    #region

    using System.Windows;

    #endregion

    /// <summary>
    ///     The specific DependencyPropertyChangeNotifier class
    /// </summary>
    /// <seealso cref="DependencyPropertyChangeNotifier{object}" />
    public class DependencyPropertyChangeNotifier : DependencyPropertyChangeNotifier<object>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="path">The path.</param>
        public DependencyPropertyChangeNotifier(DependencyObject propertySource, string path)
            : base(propertySource, path)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="property">The property.</param>
        public DependencyPropertyChangeNotifier(DependencyObject propertySource, DependencyProperty property)
            : base(propertySource, property)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DependencyPropertyChangeNotifier" /> class.
        /// </summary>
        /// <param name="propertySource">The property source.</param>
        /// <param name="property">The property.</param>
        public DependencyPropertyChangeNotifier(DependencyObject propertySource, PropertyPath property)
            : base(propertySource, property)
        {
        }
    }
}
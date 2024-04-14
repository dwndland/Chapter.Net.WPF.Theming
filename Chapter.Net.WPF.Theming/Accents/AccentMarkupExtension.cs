// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentMarkupExtension.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Markup;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Base for the both accent markup extensions.
    /// </summary>
    public abstract class AccentMarkupExtension : MarkupExtension
    {
        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        protected AccentMarkupExtension()
        {
            ColorSetChangeObserver.AddCallback(OnSystemColorsChanged);
        }

        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        /// <param name="accent">The requested accent.</param>
        protected AccentMarkupExtension(Accent accent)
            : this()
        {
            Accent = accent;
        }

        /// <summary>
        ///     The requested accent.
        /// </summary>
        public Accent Accent { get; set; }

        /// <summary>
        ///     Defines if the color shall be the foreground.
        /// </summary>
        public bool UseForeground { get; set; }

        /// <summary>
        ///     The object using the markup extension.
        /// </summary>
        protected DependencyObject TargetObject { get; private set; }

        /// <summary>
        ///     The property using the markup extension.
        /// </summary>
        protected DependencyProperty TargetProperty { get; private set; }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget target)
            {
                TargetObject = target.TargetObject as DependencyObject;
                TargetProperty = target.TargetProperty as DependencyProperty;
            }

            return OnProvideValue();
        }

        /// <summary>
        ///     Resets the internal color cache.
        /// </summary>
        public static void ResetColorCache()
        {
            AccentColorsCache.Reset();
        }

        /// <summary>
        ///     Provides the value for the target property.
        /// </summary>
        /// <returns></returns>
        protected abstract object OnProvideValue();

        private void OnSystemColorsChanged()
        {
            if (TargetObject != null && TargetProperty != null && !TargetObject.IsSealed)
                Refresh();
        }

        /// <summary>
        ///     Updates the color.
        /// </summary>
        protected abstract void Refresh();
    }
}
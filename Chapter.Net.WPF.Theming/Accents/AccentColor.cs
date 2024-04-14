// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentColor.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Provides an accent as a color.
    /// </summary>
    public class AccentColor : AccentMarkupExtension
    {
        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        public AccentColor()
        {
        }

        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        /// <param name="accent">The requested accent.</param>
        public AccentColor(Accent accent)
            : base(accent)
        {
        }

        /// <inheritdoc />
        protected override object OnProvideValue()
        {
            switch (Accent)
            {
                case Accent.SystemAccentDark3:
                    return UseForeground ? AccentColorsCache.SystemAccentDark3ForegroundColor.Value : AccentColorsCache.SystemAccentDark3Color.Value;
                case Accent.SystemAccentDark2:
                    return UseForeground ? AccentColorsCache.SystemAccentDark2ForegroundColor.Value : AccentColorsCache.SystemAccentDark2Color.Value;
                case Accent.SystemAccentDark1:
                    return UseForeground ? AccentColorsCache.SystemAccentDark1ForegroundColor.Value : AccentColorsCache.SystemAccentDark1Color.Value;
                case Accent.SystemAccent:
                    return UseForeground ? AccentColorsCache.SystemAccentForegroundColor.Value : AccentColorsCache.SystemAccentColor.Value;
                case Accent.SystemAccentLight1:
                    return UseForeground ? AccentColorsCache.SystemAccentLight1ForegroundColor.Value : AccentColorsCache.SystemAccentLight1Color.Value;
                case Accent.SystemAccentLight2:
                    return UseForeground ? AccentColorsCache.SystemAccentLight2ForegroundColor.Value : AccentColorsCache.SystemAccentLight2Color.Value;
                case Accent.SystemAccentLight3:
                    return UseForeground ? AccentColorsCache.SystemAccentLight3ForegroundColor.Value : AccentColorsCache.SystemAccentLight3Color.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        protected override void Refresh()
        {
            if (TargetObject != null && TargetProperty != null)
                TargetObject.SetValue(TargetProperty, OnProvideValue());
        }
    }
}
// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentBrush.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Provides an accent as a brush.
    /// </summary>
    public class AccentBrush : AccentMarkupExtension
    {
        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        public AccentBrush()
        {
        }

        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        /// <param name="accent">The requested accent.</param>
        public AccentBrush(Accent accent)
            : base(accent)
        {
        }

        /// <inheritdoc />
        protected override object OnProvideValue()
        {
            switch (Accent)
            {
                case Accent.SystemAccentDark3:
                    return UseForeground ? AccentColorsCache.SystemAccentDark3ForegroundBrush.Value : AccentColorsCache.SystemAccentDark3Brush.Value;
                case Accent.SystemAccentDark2:
                    return UseForeground ? AccentColorsCache.SystemAccentDark2ForegroundBrush.Value : AccentColorsCache.SystemAccentDark2Brush.Value;
                case Accent.SystemAccentDark1:
                    return UseForeground ? AccentColorsCache.SystemAccentDark1ForegroundBrush.Value : AccentColorsCache.SystemAccentDark1Brush.Value;
                case Accent.SystemAccent:
                    return UseForeground ? AccentColorsCache.SystemAccentForegroundBrush.Value : AccentColorsCache.SystemAccentBrush.Value;
                case Accent.SystemAccentLight1:
                    return UseForeground ? AccentColorsCache.SystemAccentLight1ForegroundBrush.Value : AccentColorsCache.SystemAccentLight1Brush.Value;
                case Accent.SystemAccentLight2:
                    return UseForeground ? AccentColorsCache.SystemAccentLight2ForegroundBrush.Value : AccentColorsCache.SystemAccentLight2Brush.Value;
                case Accent.SystemAccentLight3:
                    return UseForeground ? AccentColorsCache.SystemAccentLight3ForegroundBrush.Value : AccentColorsCache.SystemAccentLight3Brush.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void Refresh()
        {
            if (TargetObject != null && TargetProperty != null)
                TargetObject.SetValue(TargetProperty, OnProvideValue());
        }
    }
}
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
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (UseForeground)
                switch (Accent)
                {
                    case Accent.SystemAccentDark3:
                        return AccentColorsCache.SystemAccentDark3ForegroundColor.Value;
                    case Accent.SystemAccentDark2:
                        return AccentColorsCache.SystemAccentDark2ForegroundColor.Value;
                    case Accent.SystemAccentDark1:
                        return AccentColorsCache.SystemAccentDark1ForegroundColor.Value;
                    case Accent.SystemAccent:
                        return AccentColorsCache.SystemAccentForegroundColor.Value;
                    case Accent.SystemAccentLight1:
                        return AccentColorsCache.SystemAccentLight1ForegroundColor.Value;
                    case Accent.SystemAccentLight2:
                        return AccentColorsCache.SystemAccentLight2ForegroundColor.Value;
                    case Accent.SystemAccentLight3:
                        return AccentColorsCache.SystemAccentLight3ForegroundColor.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            switch (Accent)
            {
                case Accent.SystemAccentDark3:
                    return AccentColorsCache.SystemAccentDark3Color.Value;
                case Accent.SystemAccentDark2:
                    return AccentColorsCache.SystemAccentDark2Color.Value;
                case Accent.SystemAccentDark1:
                    return AccentColorsCache.SystemAccentDark1Color.Value;
                case Accent.SystemAccent:
                    return AccentColorsCache.SystemAccentColor.Value;
                case Accent.SystemAccentLight1:
                    return AccentColorsCache.SystemAccentLight1Color.Value;
                case Accent.SystemAccentLight2:
                    return AccentColorsCache.SystemAccentLight2Color.Value;
                case Accent.SystemAccentLight3:
                    return AccentColorsCache.SystemAccentLight3Color.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
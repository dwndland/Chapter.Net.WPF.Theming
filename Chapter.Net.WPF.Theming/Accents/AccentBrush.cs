// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentBrush.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net.WPF.Theming.Accents.Internal;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    public class AccentBrush : AccentMarkupExtension
    {
        public AccentBrush()
        {
        }

        public AccentBrush(Accent accent)
            : base(accent)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (UseForeground)
                switch (Accent)
                {
                    case Accent.SystemAccentDark3:
                        return AccentColorsCache.SystemAccentDark3ForegroundBrush.Value;
                    case Accent.SystemAccentDark2:
                        return AccentColorsCache.SystemAccentDark2ForegroundBrush.Value;
                    case Accent.SystemAccentDark1:
                        return AccentColorsCache.SystemAccentDark1ForegroundBrush.Value;
                    case Accent.SystemAccent:
                        return AccentColorsCache.SystemAccentForegroundBrush.Value;
                    case Accent.SystemAccentLight1:
                        return AccentColorsCache.SystemAccentLight1ForegroundBrush.Value;
                    case Accent.SystemAccentLight2:
                        return AccentColorsCache.SystemAccentLight2ForegroundBrush.Value;
                    case Accent.SystemAccentLight3:
                        return AccentColorsCache.SystemAccentLight3ForegroundBrush.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            switch (Accent)
            {
                case Accent.SystemAccentDark3:
                    return AccentColorsCache.SystemAccentDark3Brush.Value;
                case Accent.SystemAccentDark2:
                    return AccentColorsCache.SystemAccentDark2Brush.Value;
                case Accent.SystemAccentDark1:
                    return AccentColorsCache.SystemAccentDark1Brush.Value;
                case Accent.SystemAccent:
                    return AccentColorsCache.SystemAccentBrush.Value;
                case Accent.SystemAccentLight1:
                    return AccentColorsCache.SystemAccentLight1Brush.Value;
                case Accent.SystemAccentLight2:
                    return AccentColorsCache.SystemAccentLight2Brush.Value;
                case Accent.SystemAccentLight3:
                    return AccentColorsCache.SystemAccentLight3Brush.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
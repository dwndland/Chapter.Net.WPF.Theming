// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentColorsCache.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Media;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming;

/*
 * Profiling 1.000.000 calls with the same color on request:
 * AccentColorProvider: 0.5129058s
 * AccentColorsCache:   0.0025210s
 */
internal static class AccentColorsCache
{
    static AccentColorsCache()
    {
        Reset();
    }

    public static Lazy<Color> SystemAccentDark3Color { get; private set; }
    public static Lazy<Color> SystemAccentDark3ForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentDark2Color { get; private set; }
    public static Lazy<Color> SystemAccentDark2ForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentDark1Color { get; private set; }
    public static Lazy<Color> SystemAccentDark1ForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentColor { get; private set; }
    public static Lazy<Color> SystemAccentForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentLight1Color { get; private set; }
    public static Lazy<Color> SystemAccentLight1ForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentLight2Color { get; private set; }
    public static Lazy<Color> SystemAccentLight2ForegroundColor { get; private set; }

    public static Lazy<Color> SystemAccentLight3Color { get; private set; }
    public static Lazy<Color> SystemAccentLight3ForegroundColor { get; private set; }

    public static void Reset()
    {
        SystemAccentDark3Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark3));
        SystemAccentDark3ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark3Color.Value));

        SystemAccentDark2Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark2));
        SystemAccentDark2ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark2Color.Value));

        SystemAccentDark1Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark1));
        SystemAccentDark1ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark1Color.Value));

        SystemAccentColor = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccent));
        SystemAccentForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentColor.Value));

        SystemAccentLight1Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight1));
        SystemAccentLight1ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight1Color.Value));

        SystemAccentLight2Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight2));
        SystemAccentLight2ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight2Color.Value));

        SystemAccentLight3Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight3));
        SystemAccentLight3ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight3Color.Value));
    }
}
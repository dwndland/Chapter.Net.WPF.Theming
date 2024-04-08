﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentColorsCache.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Media;

namespace Chapter.Net.WPF.Theming.Accents.Internal
{
    internal static class AccentColorsCache
    {
        static AccentColorsCache()
        {
            Reset();
        }

        public static Lazy<Color> SystemAccentDark3Color { get; private set; }
        public static Lazy<Color> SystemAccentDark3ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark3Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark3ForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentDark2Color { get; private set; }
        public static Lazy<Color> SystemAccentDark2ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark2Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark2ForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentDark1Color { get; private set; }
        public static Lazy<Color> SystemAccentDark1ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark1Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentDark1ForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentColor { get; private set; }
        public static Lazy<Color> SystemAccentForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentBrush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentLight1Color { get; private set; }
        public static Lazy<Color> SystemAccentLight1ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight1Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight1ForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentLight2Color { get; private set; }
        public static Lazy<Color> SystemAccentLight2ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight2Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight2ForegroundBrush { get; private set; }

        public static Lazy<Color> SystemAccentLight3Color { get; private set; }
        public static Lazy<Color> SystemAccentLight3ForegroundColor { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight3Brush { get; private set; }
        public static Lazy<SolidColorBrush> SystemAccentLight3ForegroundBrush { get; private set; }

        public static void Reset()
        {
            SystemAccentDark3Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark3));
            SystemAccentDark3ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark3Color.Value));
            SystemAccentDark3Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark3Color.Value));
            SystemAccentDark3ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark3ForegroundColor.Value));

            SystemAccentDark2Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark2));
            SystemAccentDark2ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark2Color.Value));
            SystemAccentDark2Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark2Color.Value));
            SystemAccentDark2ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark2ForegroundColor.Value));

            SystemAccentDark1Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentDark1));
            SystemAccentDark1ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentDark1Color.Value));
            SystemAccentDark1Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark1Color.Value));
            SystemAccentDark1ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentDark1ForegroundColor.Value));

            SystemAccentColor = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccent));
            SystemAccentForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentColor.Value));
            SystemAccentBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentColor.Value));
            SystemAccentForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentForegroundColor.Value));

            SystemAccentLight1Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight1));
            SystemAccentLight1ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight1Color.Value));
            SystemAccentLight1Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight1Color.Value));
            SystemAccentLight1ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight1ForegroundColor.Value));

            SystemAccentLight2Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight2));
            SystemAccentLight2ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight2Color.Value));
            SystemAccentLight2Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight2Color.Value));
            SystemAccentLight2ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight2ForegroundColor.Value));

            SystemAccentLight3Color = new Lazy<Color>(() => AccentColorProvider.GetAccentColor(Accent.SystemAccentLight3));
            SystemAccentLight3ForegroundColor = new Lazy<Color>(() => AccentColorProvider.GetForegroundByBackground(SystemAccentLight3Color.Value));
            SystemAccentLight3Brush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight3Color.Value));
            SystemAccentLight3ForegroundBrush = new Lazy<SolidColorBrush>(() => ToBrush(SystemAccentLight3ForegroundColor.Value));
        }

        private static SolidColorBrush ToBrush(Color color)
        {
            return new SolidColorBrush { Color = color };
        }
    }
}
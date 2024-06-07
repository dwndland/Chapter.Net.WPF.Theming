// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentColorProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using System.Windows.Media;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming;

/// <summary>
///     Provides the windows accent colors.
/// </summary>
public static class AccentColorProvider
{
    /// <summary>
    ///     Gets the windows accent color.
    /// </summary>
    /// <param name="accent">The concrete accent to get.</param>
    /// <param name="alpha">The optional alpha channel.</param>
    /// <returns>The configured accent color.</returns>
    public static Color GetAccentColor(Accent accent, byte alpha = 255)
    {
        var marshalledName = Marshal.StringToHGlobalUni("Immersive" + accent);
        var colorType = UxTheme.GetImmersiveColorTypeFromName(marshalledName);
        Marshal.FreeHGlobal(marshalledName);
        var nativeColor = UxTheme.GetImmersiveColorFromColorSetEx(0, colorType, false, 0);
        return Color.FromArgb(
            alpha != 255 ? alpha : (byte)((0xFF000000 & nativeColor) >> 24),
            (byte)((0x000000FF & nativeColor) >> 0),
            (byte)((0x0000FF00 & nativeColor) >> 8),
            (byte)((0x00FF0000 & nativeColor) >> 16));
    }

    /// <summary>
    ///     Creates the foreground ready to use on the given background.
    /// </summary>
    /// <param name="backgroundColor">The used background color.</param>
    /// <returns>The foreground ready to use on the given background.</returns>
    public static Color GetForegroundByBackground(Color backgroundColor)
    {
        return IsBrightColor(backgroundColor) ? Colors.Black : Colors.White;
    }

    /// <summary>
    ///     Checks if the given color is a bright color.
    /// </summary>
    /// <param name="color">The color to check.</param>
    /// <returns>True if it is a bright color; otherwise false.</returns>
    public static bool IsBrightColor(Color color)
    {
        byte limit = 0x7F;
        return color.A < limit ||
               (color.R > limit && color.G > limit) ||
               (color.R > limit && color.B > limit) ||
               (color.G > limit && color.B > limit);
    }
}
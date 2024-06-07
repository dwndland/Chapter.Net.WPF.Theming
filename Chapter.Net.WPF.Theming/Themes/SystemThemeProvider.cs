// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SystemThemeProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Microsoft.Win32;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming;

/// <summary>
///     Provides the windows theme.
/// </summary>
public static class SystemThemeProvider
{
    /// <summary>
    ///     Gets the theme the system is configured to.
    /// </summary>
    /// <returns>WindowTheme.Light or WindowTheme.Dark depending on the system configuration.</returns>
    public static WindowTheme GetSystemTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        var registryValueObject = key?.GetValue("AppsUseLightTheme");
        if (registryValueObject == null)
            return WindowTheme.Light;

        var registryValue = (int)registryValueObject;
        return registryValue > 0 ? WindowTheme.Light : WindowTheme.Dark;
    }
}
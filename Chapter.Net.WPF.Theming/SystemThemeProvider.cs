// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SystemThemeProvider.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net.WPF.Theming.Internal;
using Microsoft.Win32;

namespace Chapter.Net.WPF.Theming
{
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
            using (var key = Registry.CurrentUser.OpenSubKey(WindowsEnvironment.ThemeRegistryKeyPath))
            {
                var registryValueObject = key?.GetValue(WindowsEnvironment.ThemeRegistryValueName);
                if (registryValueObject == null)
                    return WindowTheme.Light;

                var registryValue = (int)registryValueObject;
                return registryValue > 0 ? WindowTheme.Light : WindowTheme.Dark;
            }
        }
    }
}
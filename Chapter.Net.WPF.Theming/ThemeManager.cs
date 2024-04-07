// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeManager.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Interop;
using Chapter.Net.WinAPI;
using Chapter.Net.WinAPI.Data;
using Chapter.Net.WPF.Theming.Internal;
using Microsoft.Win32;

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Reads or sets the window theme.
    /// </summary>
    public static class ThemeManager
    {

        /// <summary>
        ///     Sets the theme to use for the given window.
        /// </summary>
        /// <param name="window">The window to modify.</param>
        /// <param name="theme">The theme to use.</param>
        /// <remarks>The window source must be initialized.</remarks>
        /// <returns>True of the theme got applied to the window; otherwise false.</returns>
        public static bool SetWindowTheme(Window window, WindowTheme theme)
        {
            if (theme == WindowTheme.System)
                theme = GetSystemTheme();

            if (WindowsEnvironment.IsWindows10OrGreater(WindowsEnvironment.Windows10BuildNumber))
            {
                var attribute = DWMWA.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (WindowsEnvironment.IsWindows10OrGreater(WindowsEnvironment.Windows10BuildNumber20H1))
                    attribute = DWMWA.USE_IMMERSIVE_DARK_MODE;

                var useDarkMode = theme == WindowTheme.Dark ? 1 : 0;
                var handle = new WindowInteropHelper(window).Handle;
                return Dwmapi.DwmSetWindowAttribute(handle, (int)attribute, ref useDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

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

        {
        }
    }
}
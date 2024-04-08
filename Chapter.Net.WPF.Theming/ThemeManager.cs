// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeManager.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Chapter.Net.WinAPI;
using Chapter.Net.WinAPI.Data;
using Chapter.Net.WPF.Theming.Internal;
using Microsoft.Win32;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Reads or sets the window theme.
    /// </summary>
    public static class ThemeManager
    {
        /// <summary>
        ///     Defines the RequestTheme attached dependency property.
        /// </summary>
        public static readonly DependencyProperty RequestThemeProperty =
            DependencyProperty.RegisterAttached("RequestTheme", typeof(WindowTheme), typeof(ThemeManager), new PropertyMetadata(OnRequestThemeChanged));

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

        /// <summary>
        ///     Gets the attached the requested theme from a window.
        /// </summary>
        /// <param name="obj">The window.</param>
        /// <returns>The attached theme.</returns>
        public static WindowTheme GetRequestTheme(DependencyObject obj)
        {
            return (WindowTheme)obj.GetValue(RequestThemeProperty);
        }

        /// <summary>
        ///     Gets the attached the requested theme from a window.
        /// </summary>
        /// <param name="obj">The window.</param>
        /// <returns>The attached theme.</returns>
        public static void SetRequestTheme(DependencyObject obj, WindowTheme value)
        {
            obj.SetValue(RequestThemeProperty, value);
        }

        private static void OnRequestThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window))
                throw new InvalidOperationException("The RequestTheme can be attached to a window only.");

            if (window.IsInitialized)
                SetTheme(window, (WindowTheme)e.NewValue);
            else
                window.SourceInitialized += OnSourceInitialized;
        }

        private static void OnSourceInitialized(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.SourceInitialized -= OnSourceInitialized;
            SetTheme(window, GetRequestTheme(window));
        }

        private static void SetTheme(Window window, WindowTheme theme)
        {
            if (theme == WindowTheme.System)
                theme = GetSystemTheme();

            SetWindowTheme(window, theme);
            switch (theme)
            {
                case WindowTheme.Light:
                    window.Background = new SolidColorBrush { Color = Color.FromRgb(243, 243, 243) };
                    break;
                case WindowTheme.Dark:
                    window.Background = new SolidColorBrush { Color = Color.FromRgb(32, 32, 32) };
                    break;
            }
        }
    }
}
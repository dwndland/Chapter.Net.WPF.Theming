// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeManager.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Chapter.Net.WinAPI;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Reads or sets the window theme.
    /// </summary>
    public static class ThemeManager
    {
        #region Common

        private static readonly List<Window> _systemThemeWindows = new List<Window>();

        static ThemeManager()
        {
            ColorSetChangeObserver.AddCallback(OnSystemColorChanged);
        }

        private static void OnSystemColorChanged()
        {
            _systemThemeWindows.ForEach(x => SetWindowTheme(x, WindowTheme.System));
        }

        #endregion

        #region SetWindowTheme

        /// <summary>
        ///     Sets the theme to use for the given window.
        /// </summary>
        /// <param name="window">The window to modify.</param>
        /// <param name="theme">The theme to use.</param>
        /// <param name="setBodyColors">Defines if the window background shall be set as well.</param>
        /// <remarks>The window source must be initialized.</remarks>
        /// <returns>True of the theme got applied to the window; otherwise false.</returns>
        public static bool SetWindowTheme(Window window, WindowTheme theme, bool setBodyColors = true)
        {
            if (theme == WindowTheme.System)
                theme = SystemThemeProvider.GetSystemTheme();

            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                    attribute = DWMWA.USE_IMMERSIVE_DARK_MODE;

                var useDarkMode = theme == WindowTheme.Dark ? 1 : 0;
                var handle = new WindowInteropHelper(window).Handle;
                if (Dwmapi.DwmSetWindowAttribute(handle, (int)attribute, ref useDarkMode, sizeof(int)) != 0)
                    return false;
            }

            if (setBodyColors)
                switch (theme)
                {
                    case WindowTheme.Light:
                        window.Background = new SolidColorBrush { Color = Color.FromRgb(243, 243, 243) };
                        window.Foreground = new SolidColorBrush { Color = Colors.Black };
                        break;
                    case WindowTheme.Dark:
                        window.Background = new SolidColorBrush { Color = Color.FromRgb(32, 32, 32) };
                        window.Foreground = new SolidColorBrush { Color = Colors.White };
                        break;
                }

            return true;
        }

        private static bool IsWindows10OrGreater(int build)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }

        #endregion

        #region RequestTheme

        /// <summary>
        ///     Defines the RequestTheme attached dependency property.
        /// </summary>
        public static readonly DependencyProperty RequestThemeProperty =
            DependencyProperty.RegisterAttached("RequestTheme", typeof(WindowTheme), typeof(ThemeManager), new PropertyMetadata(WindowTheme.System, (o, args) => { }, OnRequestThemeChanged));

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
        /// <param name="value">The requested theme.</param>
        /// <returns>The attached theme.</returns>
        public static void SetRequestTheme(DependencyObject obj, WindowTheme value)
        {
            obj.SetValue(RequestThemeProperty, value);
        }

        private static object OnRequestThemeChanged(DependencyObject d, object baseValue)
        {
            if (!(d is Window window))
                throw new InvalidOperationException("The RequestTheme can be attached to a window only.");

            if (window.IsInitialized)
            {
                var theme = (WindowTheme)baseValue;
                SetWindowTheme(window, theme, !SkipSetBodyColors);
                _systemThemeWindows.Remove(window);
                if (theme == WindowTheme.System)
                    _systemThemeWindows.Add(window);
            }
            else
                window.SourceInitialized += OnSourceInitialized;

            window.Closed += OnRequestedWindowClose;
            return baseValue;
        }

        private static void OnSourceInitialized(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.SourceInitialized -= OnSourceInitialized;
            var theme = GetRequestTheme(window);
            SetWindowTheme(window, theme, !SkipSetBodyColors);
            _systemThemeWindows.Remove(window);
            if (theme == WindowTheme.System)
                _systemThemeWindows.Add(window);
        }

        private static void OnRequestedWindowClose(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Closed -= OnRequestedWindowClose;
            _systemThemeWindows.Remove(window);
        }

        #endregion

        #region CompleteTheming

        private static WindowTheme _currentTheme = WindowTheme.System;
        private static readonly List<Window> _servantWindows = new List<Window>();

        /// <summary>
        ///     Defines the ObeyThemeManager attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ObeyThemeManagerProperty =
            DependencyProperty.RegisterAttached("ObeyThemeManager", typeof(bool), typeof(ThemeManager), new PropertyMetadata(OnObeyThemeManagerChanged));

        /// <summary>
        ///     Gets the indicator if the window is obeying the theme manager.
        /// </summary>
        /// <param name="obj">The window.</param>
        /// <returns>True if the window is obeying the theme manager; otherwise false.</returns>
        public static bool GetObeyThemeManager(DependencyObject obj)
        {
            return (bool)obj.GetValue(ObeyThemeManagerProperty);
        }

        /// <summary>
        ///     Sets the indicator if the window has to obey the theme manager.
        /// </summary>
        /// <param name="obj">The window.</param>
        /// <param name="value">The indicator.</param>
        public static void SetObeyThemeManager(DependencyObject obj, bool value)
        {
            obj.SetValue(ObeyThemeManagerProperty, value);
        }

        private static void OnObeyThemeManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window))
                throw new InvalidOperationException("Only a window can obey the theme manager.");

            if ((bool)e.OldValue)
                _servantWindows.Remove(window);
            if ((bool)e.NewValue)
                _servantWindows.Add(window);

            window.Closed += OnClosed;

            if (window.IsInitialized)
                SetWindowTheme(window, _currentTheme, !SkipSetBodyColors);
            else
                window.SourceInitialized += OnObeyedWindowSourceInitialized;
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            _servantWindows.Remove(window);
        }

        private static void OnObeyedWindowSourceInitialized(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.SourceInitialized -= OnObeyedWindowSourceInitialized;
            SetWindowTheme(window, _currentTheme, !SkipSetBodyColors);
        }

        /// <summary>
        ///     Gets or sets if the window background color shall not be set on theme change.
        /// </summary>
        /// <value>Default: false.</value>
        [DefaultValue(false)]
        public static bool SkipSetBodyColors { get; set; }

        /// <summary>
        ///     Sets the theme to all obeying windows.
        /// </summary>
        /// <param name="theme">The theme to use.</param>
        public static void SetCurrentTheme(WindowTheme theme)
        {
            _currentTheme = theme;
            _servantWindows.ForEach(x => SetWindowTheme(x, _currentTheme, !SkipSetBodyColors));
        }

        /// <summary>
        ///     Gets the current theme used on all the obeying windows.
        /// </summary>
        /// <returns></returns>
        public static WindowTheme GetCurrentTheme()
        {
            return _currentTheme;
        }

        #endregion
    }
}
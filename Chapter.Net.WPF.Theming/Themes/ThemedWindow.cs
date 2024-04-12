// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemedWindow.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     A window which provides a way to request a theme for it directly.
    /// </summary>
    public class ThemedWindow : Window
    {
        /// <summary>
        ///     Defines the RequestTheme dependency property.
        /// </summary>
        public static readonly DependencyProperty RequestThemeProperty =
            DependencyProperty.Register(nameof(RequestTheme), typeof(WindowTheme), typeof(ThemedWindow), new PropertyMetadata(OnWindowThemeChanged));

        /// <summary>
        ///     Defines the ObeyThemeManager dependency property.
        /// </summary>
        public static readonly DependencyProperty ObeyThemeManagerProperty =
            DependencyProperty.Register(nameof(ObeyThemeManager), typeof(bool), typeof(ThemedWindow), new PropertyMetadata(OnObeyThemeManagerChanged));

        /// <summary>
        ///     Gets or sets the theme the window shall have.
        /// </summary>
        /// <value>Default: WindowTheme.System.</value>
        [DefaultValue(WindowTheme.System)]
        public WindowTheme RequestTheme
        {
            get => (WindowTheme)GetValue(RequestThemeProperty);
            set => SetValue(RequestThemeProperty, value);
        }

        /// <summary>
        ///     Gets or sets if the window has to obey the theme manager.
        /// </summary>
        /// <value>Default: false.</value>
        [DefaultValue(false)]
        public bool ObeyThemeManager
        {
            get => (bool)GetValue(ObeyThemeManagerProperty);
            set => SetValue(ObeyThemeManagerProperty, value);
        }

        private static void OnWindowThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (Window)d;
            ThemeManager.SetRequestTheme(window, (WindowTheme)e.NewValue);
        }

        private static void OnObeyThemeManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (Window)d;
            ThemeManager.SetObeyThemeManager(window, (bool)e.NewValue);
        }
    }
}
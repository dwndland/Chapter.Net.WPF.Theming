// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemedWindow.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

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
            DependencyProperty.Register(nameof(RequestTheme), typeof(WindowTheme), typeof(ThemedWindow), new PropertyMetadata(WindowTheme.System));

        /// <summary>
        ///     Creates a new instance of ThemedWindow.
        /// </summary>
        public ThemedWindow()
        {
            Loaded += OnLoaded;
        }

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

        /// <inheritdoc />
        protected override void OnSourceInitialized(EventArgs e)
        {
            ChangeTheme();

            base.OnSourceInitialized(e);
        }

        /// <summary>
        ///     Callback when the window got loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        ///     Changes the current window theme to the one requested.
        /// </summary>
        protected virtual void ChangeTheme()
        {
            var theme = RequestTheme;
            if (theme == WindowTheme.System)
                theme = ThemeManager.GetSystemTheme();

            ThemeManager.SetWindowTheme(this, theme);
            switch (theme)
            {
                case WindowTheme.Light:
                    Background = new SolidColorBrush { Color = Color.FromRgb(243, 243, 243) };
                    break;
                case WindowTheme.Dark:
                    Background = new SolidColorBrush { Color = Color.FromRgb(32, 32, 32) };
                    break;
            }
        }
    }
}
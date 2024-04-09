// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using System.Windows.Media;
using Chapter.Net.WPF.Theming;

namespace Demo1;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        var systemTheme = SystemThemeProvider.GetSystemTheme();
        ThemeManager.SetWindowTheme(this, systemTheme);
        SetBackgroundColor(systemTheme);

        base.OnSourceInitialized(e);
    }

    private void ThemeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (IsLoaded)
        {
            var comboBox = (ComboBox)sender;
            var selectionBox = (ComboBoxItem)comboBox.SelectedItem;
            var theme = (WindowTheme)selectionBox.Tag;
            ThemeManager.SetWindowTheme(this, theme);
            SetBackgroundColor(theme);
        }
    }

    private void SetBackgroundColor(WindowTheme theme)
    {
        if (theme == WindowTheme.System)
        {
            theme = SystemThemeProvider.GetSystemTheme();
        }

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
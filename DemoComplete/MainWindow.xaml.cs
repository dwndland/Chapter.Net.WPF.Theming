// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using Chapter.Net.WPF.Theming;

namespace DemoComplete;

public partial class MainWindow
{
    private static readonly Uri _dark = new("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
    private static readonly Uri _light = new("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute);

    public MainWindow()
    {
        InitializeComponent();

        // Support auto update of all windows when they are set to system.
        ColorSetChangeObserver.StartListenForColorChanges(this);

        // All windows obey the ThemeManager, so this is the init for all windows.
        ThemeManager.SetCurrentTheme(WindowTheme.System);

        SetBrushes();
    }

    private void ThemeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (IsLoaded)
        {
            var comboBox = (ComboBox)sender;
            var selectionBox = (ComboBoxItem)comboBox.SelectedItem;
            var theme = (WindowTheme)selectionBox.Tag;

            ThemeManager.SetCurrentTheme(theme);
            SetBrushes();
        }
    }

    private void SetBrushes()
    {
        var dic = Application.Current.Resources.MergedDictionaries;
        dic.Clear();

        var current = ThemeManager.GetCurrentTheme();
        if (current == WindowTheme.System)
            current = SystemThemeProvider.GetSystemTheme();

        switch (current)
        {
            case WindowTheme.Light:
                dic.Add(new ResourceDictionary { Source = _light });
                break;
            case WindowTheme.Dark:
                dic.Add(new ResourceDictionary { Source = _dark });
                break;
        }
    }

    private void ShowAttachedWindow(object sender, RoutedEventArgs e)
    {
        new AttachedWindow().Show();
    }

    private void ShowThemedWindow(object sender, RoutedEventArgs e)
    {
        new ControlWindow().Show();
    }
}
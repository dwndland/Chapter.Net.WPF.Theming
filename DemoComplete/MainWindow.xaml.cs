// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using Chapter.Net.WPF.Theming;

namespace DemoComplete;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        // Support auto update of all windows when they are set to system.
        ColorSetChangeObserver.StartListenForColorChanges(this);

        // All windows obey the ThemeManager, so this is the init for all windows.
        ThemeManager.SetCurrentTheme(WindowTheme.System);
    }

    private void ThemeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (IsLoaded)
        {
            var comboBox = (ComboBox)sender;
            var selectionBox = (ComboBoxItem)comboBox.SelectedItem;
            var theme = (WindowTheme)selectionBox.Tag;

            ThemeManager.SetCurrentTheme(theme);
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
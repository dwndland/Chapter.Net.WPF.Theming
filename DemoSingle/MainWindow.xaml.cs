﻿// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Chapter.Net.WPF.Theming;

namespace DemoSingle;

public partial class MainWindow : INotifyPropertyChanged
{
    private string _accentColor;
    private string _currentSystemTheme;
    private readonly Uri _dark = new("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
    private string _lastColorSetChange;
    private readonly Uri _light = new("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute);

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        CurrentSystemTheme = SystemThemeProvider.GetSystemTheme().ToString();

        AccentColor = AccentColorProvider.GetAccentColor(Accent.SystemAccent).ToString();

        LastColorSetChange = "-";
        ColorSetChangeObserver.AddCallback(OnColorSetChanged);
        ColorSetChangeObserver.StartListenForColorChanges(this);
    }

    public string CurrentSystemTheme
    {
        get => _currentSystemTheme;
        set
        {
            _currentSystemTheme = value;
            SetField(ref _currentSystemTheme, value);
        }
    }

    public string AccentColor
    {
        get => _accentColor;
        set
        {
            _accentColor = value;
            SetField(ref _accentColor, value);
        }
    }

    public string LastColorSetChange
    {
        get => _lastColorSetChange;
        set
        {
            _lastColorSetChange = value;
            SetField(ref _lastColorSetChange, value);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ShowRequestThemeAttachedWindow(object sender, RoutedEventArgs e)
    {
        new AttachedWindow().Show();
    }

    private void ShowThemedWindow(object sender, RoutedEventArgs e)
    {
        new ControlWindow().Show();
    }

    private void ShowSetWindowThemeWindow(object sender, RoutedEventArgs e)
    {
        new SetWindowThemeWindow().Show();
    }

    private void OnColorSetChanged()
    {
        LastColorSetChange = DateTime.Now.ToString();
    }

    private void SwitchToLightResources(object sender, RoutedEventArgs e)
    {
        ResourcesManager.RemoveResources(Application.Current, _dark);
        ResourcesManager.LoadResources(Application.Current, ResourceLocation.End, _light);
    }

    private void SwitchToDarkResources(object sender, RoutedEventArgs e)
    {
        ResourcesManager.RemoveResources(Application.Current, _light);
        ResourcesManager.LoadResources(Application.Current, ResourceLocation.End, _dark);
    }
}
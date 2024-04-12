// -----------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;
using Chapter.Net.WPF.Theming;

namespace DemoComplete;

public partial class App
{
    private static readonly Uri _dark = new("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
    private static readonly Uri _light = new("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute);

    protected override void OnStartup(StartupEventArgs e)
    {
        ShutdownMode = ShutdownMode.OnMainWindowClose;

        ThemeManager.SetCurrentTheme(WindowTheme.Light);
        SetBrushes();

        base.OnStartup(e);
    }

    private void SetBrushes()
    {
        var dic = Resources.MergedDictionaries;
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
}
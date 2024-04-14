// -----------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net.WPF.Theming;

namespace DemoSingle;

public partial class App
{
    public App()
    {
        SetBrushes();
    }

    private void SetBrushes()
    {
        switch (SystemThemeProvider.GetSystemTheme())
        {
            case WindowTheme.Light:
                ResourcesManager.LoadResources(this, ResourceLocation.End, new Uri("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute));
                break;
            case WindowTheme.Dark:
                ResourcesManager.LoadResources(this, ResourceLocation.End, new Uri("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute));
                break;
        }
    }
}
// -----------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net.WPF.Theming;
using System;

namespace DemoComplete;

public partial class App
{
    public App()
    {
        ResourcesManager.RegisterResources(this, ResourceLocation.End, WindowTheme.Light, new Uri("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute));
        ResourcesManager.RegisterResources(this, ResourceLocation.End, WindowTheme.Dark, new Uri("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute));
    }
}
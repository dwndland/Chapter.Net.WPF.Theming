// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SetWindowThemeWindow.xaml.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using Chapter.Net.WPF.Theming;

namespace DemoSingle;

public partial class SetWindowThemeWindow
{
    public SetWindowThemeWindow()
    {
        ThemeManager.SetWindowTheme(this, WindowTheme.Dark);

        InitializeComponent();
    }
}
![Chapter](https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF.Theming/master/Icon.png)

# Chapter.Net.WPF.Theming Library

## Overview
Chapter.Net.WPF.Theming is a library that simplifies working with themes and accent colors, enabling easy customization of the visual style in WPF applications.

## Features (Single Operations)
- **SystemThemeProvider:** Read the current theme from windows. Light or Dark.
- **AccentColorProvider:** Read all the current accent colors from windows.
- **AccentBrush:** Set a particular grade from the current accent color as a brush to a control in xaml.
- **AccentColor:** Set a particular grade from the current accent to a color to a brush in xaml.
- **ThemeManager.RequestTheme:** Use xaml attached property to set a specific theme on a window.
- **ThemeManager.SetWindowTheme:** Use code to set a specific theme on a window.
- **ThemedWindow:** Use the ThemedWindow to create a window that has a build in functionality to set a particular theme.
- **ColorSetChangeObserver:** Get informed when on windows the theme or accent color
- **Add/Remove Theme Resources:** Use the ResourcesManager to add and remove theme resources.

## Features (Complete Operations)
- **Complete Theming:** Configure the theme globally for all windows by attached properties or the ThemedWindow with possibility for auto theme by windows or manual override.

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.WPF.Theming library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.WPF.Theming
    ```

2. **SystemThemeProvider:**
    - Read the current theme from windows. Light or Dark.
    ```csharp
    var theme = SystemThemeProvider.GetSystemTheme();
    ```

3. **AccentColorProvider:**
    - Read all the current accent colors from windows.
    ```csharp
    var accent = AccentColorProvider.GetAccentColor(Accent.SystemAccent);
    var accentLight2 = AccentColorProvider.GetAccentColor(Accent.SystemAccentLight2);
    ```

4. **AccentBrush:**
    - Set a particular grade from the current accent color as a brush to a control in xaml.
    ```xaml
    <TextBlock Background="{theming:AccentBrush SystemAccentLight2}"
               Foreground="{theming:AccentBrush SystemAccentLight2, UseForeground=True}"
               Text="SystemAccentLight2" />
    ```

5. **AccentColor:**
    - Set a particular grade from the current accent to a color to a brush in xaml.
    ```xaml
    <TextBlock Background="{StaticResource background}"
               Foreground="{StaticResource foreground}"
               Text="SystemAccentLight2">
        <TextBlock.Resources>
            <SolidColorBrush x:Key="background" Color="{theming:AccentColor SystemAccentLight2}" />
            <SolidColorBrush x:Key="foreground" Color="{theming:AccentColor SystemAccentLight2, UseForeground=True}" />
        </TextBlock.Resources>
    </TextBlock>
    ```

6. **ThemeManager.RequestTheme:**
    - Use xaml attached property to set a specific theme on a window.
    ```xaml
    <Window xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
            theming:ThemeManager.RequestTheme="System" />

    <Window xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
            theming:ThemeManager.RequestTheme="Light" />

    <Window xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
            theming:ThemeManager.RequestTheme="Dark" />
    ```

7. **ThemeManager.SetWindowTheme:**
    - Use code to set a specific theme on a window.
    ```csharp
    public partial class MyWindow
    {
        public MyWindow()
        {
            ThemeManager.SetWindowTheme(this, WindowTheme.System);
            InitializeComponent();
        }
    }
    ```

8. **ThemedWindow:**
    - Use the ThemedWindow to create a window that has a build in functionality to set a particular theme.
    ```xaml
    <theming:ThemedWindow xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
                          RequestTheme="System" />

    <theming:ThemedWindow xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
                          RequestTheme="Light" />

    <theming:ThemedWindow xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
                          RequestTheme="Dark" />
    ```

9. **ColorSetChangeObserver:**
    - Get informed when on windows the theme or accent color
    ```csharp
    ColorSetChangeObserver.AddCallback(OnSystemColorChanged);

    private void OnSystemColorChanged()
    {
        // Window theme or accent color changed.
    }
    ```

10. **Add/Remove Theme Resources:**
    - Use the ResourcesManager to add and remove theme resources.
    ```csharp
    private Uri _light = new("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute);
    private Uri _dark = new("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);

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
    ```

11. **Complete Theming:**
    - Configure the theme globally for all windows by attached properties or the ThemedWindow with possibility for auto theme by windows or manual override.

    Register theme resources to use when switch the theme.
    ```csharp
    public partial class App
    {
        public App()
        {
            ResourcesManager.RegisterResources(this, ResourceLocation.End, WindowTheme.Light, new Uri("/DemoControls;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute));
            ResourcesManager.RegisterResources(this, ResourceLocation.End, WindowTheme.Dark, new Uri("/DemoControls;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute));
        }
    }
    ```
    Configure all the windows to obey the theme manager.
    ```xaml
    <Window xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
            theming:ThemeManager.ObeyThemeManager="True" />

    <theming:ThemedWindow xmlns:theming="clr-namespace:Chapter.Net.WPF.Theming;assembly=Chapter.Net.WPF.Theming"
                          ObeyThemeManager="True" />
    ```
    Init the theme manager
    ```csharp
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Support for update of all windows when they are set to system.
            ColorSetChangeObserver.StartListenForColorChanges(this);

            // All windows obey the ThemeManager, so this is the init for all windows.
            ThemeManager.SetCurrentTheme(WindowTheme.System);
        }
    }
    ```
    Set new theme at runtime
    ```csharp
    private static void OnNewThemeSaved(WindowTheme theme)
    {
        ThemeManager.SetCurrentTheme(theme);
    }
    ```

## Notes

- The accent colors (AccentBrush, AccentColor) use a color cache internally for best performance. To reset the cache call AccentBrush.ResetColorCache or AccentColor.ResetColorCache.
- The color cache gets reset automatically when the system color changes are observed using the ColorSetChangeObserver with the main window.
- If any theme gets applied to a window, it sets also the window body color. To avoid that set ThemeManager.SkipSetBodyColors to true.
- Any request of a theme is on System, it updated automatically if the theme got changed on windows at runtime.

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF.Theming)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF.Theming)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.

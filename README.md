<img src="https://raw.githubusercontent.com/dwndland/Chapter.Net.WPF.Theming/master/Icon.png" alt="logo" width="64"/>

# Chapter.Net.WPF.Theming Library

## Overview
Brings features to work with the windows themes and accent colors under WPF.

## Features (Single Operations)
- **SystemThemeProvider**
Read the current theme from windows. Light or Dark.
- **AccentColorProvider**
Read all the current accent colors from windows.
- **AccentBrush**
Set a particular grade from the current accent color as a brush to a control in xaml.
    AccentBrush.ResetColorCache
- **AccentColor**
Set a particular grade from the current accent to a color to a brush in xaml.        
    AccentColor.ResetColorCache
- **ThemeManager.RequestTheme**
Use xaml attached property to set a specific theme on a window.
- **ThemeManager.SetWindowTheme**
Use code to set a specific theme on a window.
- **ThemedWindow**
Use the ThemedWindow to create a window that has a build in functionality to set a particular theme.
- **ColorSetChangeObserver**
Get informed when on windows the theme or accent color

## Features (Complete Operations)
- **ThemeManager.ObeyThemeManager + ThemeManager.SetCurrentTheme()**
Configure the theme globally for all windows by attached properties with possibility for auto theme by windows or manual override using attached properties
- **ThemedWindow.ObeyThemeManager + ThemeManager.SetCurrentTheme()**
Configure the theme globally for all windows by attached properties with possibility for auto theme by windows or manual override using ThemedWindows

ThemeManager.SkipSetBodyColors = true;

## Getting Started

1. **Installation:**
    - Install the Chapter.Net.WPF.Theming library via NuGet Package Manager:
    ```bash
    dotnet add package Chapter.Net.WPF.Theming
    ```

## Links
* [NuGet](https://www.nuget.org/packages/Chapter.Net.WPF.Theming)
* [GitHub](https://github.com/dwndland/Chapter.Net.WPF.Theming)

## License
Copyright (c) David Wendland. All rights reserved.
Licensed under the MIT License. See LICENSE file in the project root for full license information.

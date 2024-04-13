# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
### Added
- Added SystemThemeProvider to read the current windows theme.
- Added AccentColorProvider to read the current windows accent colors.
- Added theme manager to set the window theme either explicit or by system.
- Added the possibility to attach the requested theme to each window to have it applied automatically.
- Added AccentBrush markup extension to use the accent colors on UI controls directly.
- Added AccentColor markup extension to use the accent colors within color brushes.
- Added color set change observer to get informed when the theme or accent color on windows got changed.
- Added ThemedWindow object for more easy usage of themes.
- Create attached properties for Windows to obey the ThemeManager
- Allow ThemedWindow object obey the ThemeManager.

### Supported .Net Versions
- .Net Core 3.0
- .Net Framework 4.5
- .Net 5 (Windows)
- .Net 6 (Windows)
- .Net 7 (Windows)
- .Net 8 (Windows)

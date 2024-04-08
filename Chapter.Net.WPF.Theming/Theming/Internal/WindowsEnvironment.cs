// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsEnvironment.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

namespace Chapter.Net.WPF.Theming.Theming.Internal
{
    internal static class WindowsEnvironment
    {
        public const string ThemeRegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        public const string ThemeRegistryValueName = "AppsUseLightTheme";

        public const int Windows10BuildNumber = 17763;
        public const int Windows10BuildNumber20H1 = 18985;

        public static bool IsWindows10OrGreater(int build)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
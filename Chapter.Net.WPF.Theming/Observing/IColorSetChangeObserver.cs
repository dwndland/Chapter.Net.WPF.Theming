// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IColorSetChangeObserver.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Observes when windows is changing its theme or accent color.
    /// </summary>
    public interface IColorSetChangeObserver : IDisposable
    {
        /// <summary>
        ///     Raised when on windows the theme or accent color got changed.
        /// </summary>
        event EventHandler SystemColorsChanged;

        /// <summary>
        ///     Starts listen for theme or color changes on windows.
        /// </summary>
        /// <remarks>When observed, the SystemColorsChanged is raised and the AccentColorsCache gets reset.</remarks>
        /// <param name="window">The application window.</param>
        /// <exception cref="ArgumentNullException">The window cannot be null.</exception>
        void StartListenForColorChanges(Window window);

        /// <summary>
        ///     Stops listen for theme or color changes on windows.
        /// </summary>
        void StopListenForColorChanges();
    }
}
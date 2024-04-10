// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSetChangeObserver.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Observes when windows is changing its theme or accent color.
    /// </summary>
    public static class ColorSetChangeObserver
    {
        private static WindowObserver _observer;

        /// <summary>
        ///     Raised when on windows the theme or accent color got changed.
        /// </summary>
        public static event EventHandler SystemColorsChanged;

        /// <summary>
        ///     Starts listen for theme or color changes on windows.
        /// </summary>
        /// <remarks>When observed, the SystemColorsChanged is raised and the AccentColorsCache gets reset.</remarks>
        /// <param name="window">The application window.</param>
        /// <exception cref="ArgumentNullException">The window cannot be null.</exception>
        public static void StartListenForColorChanges(Window window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            if (_observer == null)
            {
                _observer = new WindowObserver(window);
                _observer.AddCallbackFor(WM.WININICHANGE, OnWindowSettingChanged);
            }
        }

        /// <summary>
        ///     Stops listen for theme or color changes on windows.
        /// </summary>
        public static void StopListenForColorChanges()
        {
            _observer?.ClearCallbacks();
            _observer = null;
        }

        private static void OnWindowSettingChanged(NotifyEventArgs obj)
        {
            if (obj.MessageId == WM.WININICHANGE)
            {
                var paramName = Marshal.PtrToStringAuto(obj.LParam);
                if (paramName == "ImmersiveColorSet")
                {
                    AccentColorsCache.Reset();
                    SystemColorsChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
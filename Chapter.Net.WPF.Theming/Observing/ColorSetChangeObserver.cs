// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSetChangeObserver.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows;
using Chapter.Net.WinAPI.Data;
using Chapter.Net.WPF.Theming.Accents.Internal;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <inheritdoc />
    public class ColorSetChangeObserver : IColorSetChangeObserver
    {
        private WindowObserver _observer;

        /// <inheritdoc />
        public event EventHandler SystemColorsChanged;

        /// <inheritdoc />
        public void StartListenForColorChanges(Window window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            if (_observer == null)
            {
                _observer = new WindowObserver(window);
                _observer.AddCallbackFor(WM.WININICHANGE, OnWindowSettingChanged);
            }
        }

        /// <inheritdoc />
        public void StopListenForColorChanges()
        {
            _observer?.ClearCallbacks();
            _observer = null;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            StopListenForColorChanges();
        }

        private void OnWindowSettingChanged(NotifyEventArgs obj)
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
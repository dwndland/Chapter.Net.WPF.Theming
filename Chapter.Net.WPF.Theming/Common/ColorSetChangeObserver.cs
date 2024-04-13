// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSetChangeObserver.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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
        private static readonly List<WeakReference<Action>> _callbacks = new List<WeakReference<Action>>();

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

        /// <summary>
        ///     Adds a (weak) callback to get informed when the system color changed;
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void AddCallback(Action callback)
        {
            _callbacks.Add(new WeakReference<Action>(callback));
        }

        /// <summary>
        ///     Removes a callback if exists.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public static void RemoveCallback(Action callback)
        {
            _callbacks.RemoveAll(x =>
            {
                var taken = x.TryGetTarget(out var action);
                return !taken || action == callback;
            });
        }

        /// <summary>
        ///     Removes all callbacks.
        /// </summary>
        public static void ResetCallbacks()
        {
            _callbacks.Clear();
        }

        private static void OnWindowSettingChanged(NotifyEventArgs obj)
        {
            if (obj.MessageId == WM.WININICHANGE)
            {
                var paramName = Marshal.PtrToStringAuto(obj.LParam);
                if (paramName == "ImmersiveColorSet")
                {
                    AccentColorsCache.Reset();
                    InvokeCallbacks();
                }
            }
        }

        private static void InvokeCallbacks()
        {
            for (var index = 0; index < _callbacks.Count; index++)
            {
                var weakRef = _callbacks[index];
                if (weakRef.TryGetTarget(out var callback))
                    callback();
                else
                    _callbacks.RemoveAt(index--);
            }
        }
    }
}
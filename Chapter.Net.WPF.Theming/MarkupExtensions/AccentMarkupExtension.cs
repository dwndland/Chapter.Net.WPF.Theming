// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentMarkupExtension.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Markup;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Base for the both accent markup extensions.
    /// </summary>
    public abstract class AccentMarkupExtension : MarkupExtension
    {
        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        protected AccentMarkupExtension()
        {
        }

        /// <summary>
        ///     Creates a new AccentMarkupExtension.
        /// </summary>
        /// <param name="accent">The requested accent.</param>
        protected AccentMarkupExtension(Accent accent)
        {
            Accent = accent;
        }

        /// <summary>
        ///     The requested accent.
        /// </summary>
        public Accent Accent { get; set; }

        /// <summary>
        ///     Defines if the color shall be the foreground.
        /// </summary>
        public bool UseForeground { get; set; }

        /// <summary>
        ///     Resets the internal color cache.
        /// </summary>
        public static void ResetColorCache()
        {
            AccentColorsCache.Reset();
        }
    }
}
// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentMarkupExtension.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Markup;
using Chapter.Net.WPF.Theming.Accents.Internal;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    public abstract class AccentMarkupExtension : MarkupExtension
    {
        protected AccentMarkupExtension()
        {
        }

        protected AccentMarkupExtension(Accent accent)
        {
            Accent = accent;
        }

        public Accent Accent { get; set; }

        public bool UseForeground { get; set; }

        public void ResetColorCache()
        {
            AccentColorsCache.Reset();
        }
    }
}
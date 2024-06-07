// -----------------------------------------------------------------------------------------------------------------
// <copyright file="AccentBrush.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Media;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming;

/// <summary>
///     Provides an accent as a brush.
/// </summary>
public class AccentBrush : AccentColor
{
    /// <summary>
    ///     Creates a new AccentMarkupExtension.
    /// </summary>
    public AccentBrush()
    {
    }

    /// <summary>
    ///     Creates a new AccentMarkupExtension.
    /// </summary>
    /// <param name="accent">The requested accent.</param>
    public AccentBrush(Accent accent)
        : base(accent)
    {
    }

    /// <inheritdoc />
    protected override object OnProvideValue()
    {
        var color = (Color)base.OnProvideValue();
        return new SolidColorBrush(color);
    }

    /// <inheritdoc />
    protected override void Refresh()
    {
        if (TargetObject != null && TargetProperty != null)
            TargetObject.SetValue(TargetProperty, OnProvideValue());
    }
}
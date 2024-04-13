// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ChapterButton.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace DemoControls;

public class ChapterButton : Button
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ChapterButton), new PropertyMetadata(default(CornerRadius)));

    static ChapterButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ChapterButton), new FrameworkPropertyMetadata(typeof(ChapterButton)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}
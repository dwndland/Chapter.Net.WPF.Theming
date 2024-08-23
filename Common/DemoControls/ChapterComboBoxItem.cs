// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ChapterComboBoxItem.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoControls;

public class ChapterComboBoxItem : ComboBoxItem
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ChapterComboBoxItem), new PropertyMetadata(default(CornerRadius)));

    public static readonly DependencyProperty IsPressedProperty =
        DependencyProperty.Register(nameof(IsPressed), typeof(bool), typeof(ChapterComboBoxItem), new PropertyMetadata(false));

    static ChapterComboBoxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ChapterComboBoxItem), new FrameworkPropertyMetadata(typeof(ChapterComboBoxItem)));
    }

    public ChapterComboBoxItem()
    {
        Loaded += OnLoaded;
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        set => SetValue(IsPressedProperty, value);
    }

    protected virtual void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        IsPressed = true;
        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        IsPressed = false;
        base.OnMouseLeftButtonUp(e);
    }

    protected override void OnMouseLeave(MouseEventArgs e)
    {
        IsPressed = false;
        base.OnMouseLeave(e);
    }

    protected override void OnMouseEnter(MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            IsPressed = true;
        base.OnMouseEnter(e);
    }
}
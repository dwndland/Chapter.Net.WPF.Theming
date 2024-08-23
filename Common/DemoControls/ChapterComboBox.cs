// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ChapterComboBox.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoControls;

public class ChapterComboBox : ComboBox
{
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ChapterComboBox), new PropertyMetadata(default(CornerRadius)));

    public static readonly DependencyProperty IsPressedProperty =
        DependencyProperty.Register(nameof(IsPressed), typeof(bool), typeof(ChapterComboBox), new PropertyMetadata(false));

    public static readonly DependencyProperty VerticalPopupOffsetProperty =
        DependencyProperty.Register(nameof(VerticalPopupOffset), typeof(double), typeof(ChapterComboBox), new PropertyMetadata(0d));

    static ChapterComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ChapterComboBox), new FrameworkPropertyMetadata(typeof(ChapterComboBox)));
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public double VerticalPopupOffset
    {
        get => (double)GetValue(VerticalPopupOffsetProperty);
        set => SetValue(VerticalPopupOffsetProperty, value);
    }

    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        set => SetValue(IsPressedProperty, value);
    }

    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is ChapterComboBoxItem;
    }

    protected override DependencyObject GetContainerForItemOverride()
    {
        return new ChapterComboBoxItem();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        VerticalPopupOffset = -(sizeInfo.NewSize.Height + 4);
        base.OnRenderSizeChanged(sizeInfo);
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
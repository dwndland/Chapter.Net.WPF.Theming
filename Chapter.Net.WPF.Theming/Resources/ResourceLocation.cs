// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceLocation.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming;

/// <summary>
///     Defines where to add the resources at.
/// </summary>
public enum ResourceLocation
{
    /// <summary>
    ///     Resources will be added to the beginning on a resource collection.
    /// </summary>
    Begin,

    /// <summary>
    ///     Resources will be added to the end on a resource collection.
    /// </summary>
    End,

    /// <summary>
    ///     Resources will be added after the ResourcesPosition.xaml position in theef resource collection.
    /// </summary>
    Position
}
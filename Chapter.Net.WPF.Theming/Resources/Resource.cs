// -----------------------------------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    public class Resource
    {
        public Resource(ResourceLocation location, Uri uri)
        {
            Location = location;
            Uri = uri;
        }

        public ResourceLocation Location { get; }
        public Uri Uri { get; }
    }
}
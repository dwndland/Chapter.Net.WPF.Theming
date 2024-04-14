// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesSwitcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.Theming
{
    /// <summary>
    ///     Maintains theme resources.
    /// </summary>
    public static class ResourcesManager
    {
        #region Remove Resources

        /// <summary>
        ///     Removes the given resources from the target application.
        /// </summary>
        /// <param name="target">The target application.</param>
        /// <param name="resources">The resources to remove</param>
        public static void RemoveResources(Application target, params Uri[] resources)
        {
            RemoveResources(target.Resources, resources);
        }

        /// <summary>
        ///     Removes the given resources from the target dictionary.
        /// </summary>
        /// <param name="target">The target dictionary.</param>
        /// <param name="resources">The resources to remove</param>
        public static void RemoveResources(ResourceDictionary target, params Uri[] resources)
        {
            RemoveResources(target.MergedDictionaries, resources);
        }

        /// <summary>
        ///     Removes the given resources from the target.
        /// </summary>
        /// <param name="target">The target collection.</param>
        /// <param name="resources">The resources to remove</param>
        public static void RemoveResources(Collection<ResourceDictionary> target, params Uri[] resources)
        {
            var resourcesToRemove = target.Where(x => resources.Contains(x.Source)).Reverse().ToList();
            foreach (var resourceToRemove in resourcesToRemove)
                target.Remove(resourceToRemove);
        }

        #endregion

        #region Load Resources

        /// <summary>
        ///     Loads the given resources into the given application resources.
        /// </summary>
        /// <param name="target">The target application.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="resources">The resources to load.</param>
        public static void LoadResources(Application target, ResourceLocation location, params Uri[] resources)
        {
            LoadResources(target.Resources, location, resources);
        }

        /// <summary>
        ///     Loads the given resources into the given dictionary.
        /// </summary>
        /// <param name="target">The target dictionary.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="resources">The resources to load.</param>
        public static void LoadResources(ResourceDictionary target, ResourceLocation location, params Uri[] resources)
        {
            LoadResources(target.MergedDictionaries, location, resources);
        }

        /// <summary>
        ///     Loads the given resources into the target.
        /// </summary>
        /// <param name="target">The target collection.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="resources">The resources to load.</param>
        public static void LoadResources(Collection<ResourceDictionary> target, ResourceLocation location, params Uri[] resources)
        {
            var dictionaries = resources.Select(x => new ResourceDictionary { Source = x }).ToList();
            switch (location)
            {
                case ResourceLocation.Begin:
                    for (var i = 0; i < dictionaries.Count; ++i) target.Insert(i, dictionaries[i]);

                    break;
                case ResourceLocation.End:
                    foreach (var dictionary in dictionaries) target.Add(dictionary);

                    break;
                case ResourceLocation.Position:
                    var position = GetPosition(target);
                    for (var i = position; i < dictionaries.Count; ++i) target.Insert(i, dictionaries[i]);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }

        private static int GetPosition(Collection<ResourceDictionary> target)
        {
            var positionIndicator = target.FirstOrDefault(x => x.Source.OriginalString.EndsWith("ResourcesPosition.xaml"));
            return positionIndicator == null ? target.Count : target.IndexOf(positionIndicator) + 1;
        }

        #endregion
    }
}
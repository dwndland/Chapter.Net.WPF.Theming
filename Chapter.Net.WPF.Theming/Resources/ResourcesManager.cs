// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesSwitcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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

        #region SwitchTheme

        private static Collection<ResourceDictionary> _target;
        private static ResourceLocation _location;
        private static WindowTheme? _currentTheme;
        private static readonly Dictionary<object, List<Uri>> _resources = new Dictionary<object, List<Uri>>();

        /// <summary>
        ///     Registers resources to use when switch to the theme.
        /// </summary>
        /// <param name="target">The target application.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="theme">The theme the resources belong to.</param>
        /// <param name="resources">The resources to load.</param>
        public static void RegisterResources(Application target, ResourceLocation location, WindowTheme theme, params Uri[] resources)
        {
            RegisterResources(target.Resources, location, theme, resources);
        }

        /// <summary>
        ///     Registers resources to use when switch to the theme.
        /// </summary>
        /// <param name="target">The target dictionary.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="theme">The theme the resources belong to.</param>
        /// <param name="resources">The resources to load.</param>
        public static void RegisterResources(ResourceDictionary target, ResourceLocation location, WindowTheme theme, params Uri[] resources)
        {
            RegisterResources(target.MergedDictionaries, location, theme, resources);
        }

        /// <summary>
        ///     Registers resources to use when switch to the theme.
        /// </summary>
        /// <param name="target">The target collection.</param>
        /// <param name="location">The position where to add the resources.</param>
        /// <param name="theme">The theme the resources belong to.</param>
        /// <param name="resources">The resources to load.</param>
        public static void RegisterResources(Collection<ResourceDictionary> target, ResourceLocation location, WindowTheme theme, params Uri[] resources)
        {
            _target = target;
            _location = location;
            _resources[theme] = resources.ToList();
        }

        /// <summary>
        ///     Removes all registered resources by the theme.
        /// </summary>
        /// <param name="theme">The theme the resources belong to.</param>
        public static void UnregisterResources(WindowTheme theme)
        {
            _resources.Remove(theme);
        }

        /// <summary>
        ///     Removes all registered resources.
        /// </summary>
        public static void ClearResources()
        {
            _resources.Clear();
        }

        /// <summary>
        ///     Switches the resources to those know by the theme.
        /// </summary>
        /// <param name="newTheme"></param>
        public static void SwitchResources(WindowTheme newTheme)
        {
            if (_resources.TryGetValue(newTheme, out var newResources))
                LoadResources(_target, _location, newResources.ToArray());
            _currentTheme = newTheme;
        }

        /// <summary>
        ///     Switches the resources from one to another theme.
        /// </summary>
        /// <param name="oldTheme">The (optional) old theme which resources to remove.</param>
        /// <param name="newTheme">The new theme which resources to load.</param>
        public static void SwitchResources(WindowTheme oldTheme, WindowTheme newTheme)
        {
            if (_resources.TryGetValue(oldTheme, out var oldResources))
                RemoveResources(_target, oldResources.ToArray());

            SwitchResources(newTheme);
        }

        #endregion
    }
}
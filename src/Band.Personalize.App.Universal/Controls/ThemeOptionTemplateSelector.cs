// Copyright 2016 Nicholas Butcher
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Band.Personalize.App.Universal.Controls
{
    using ViewModels;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Enables custom template selection logic at the application level.
    /// </summary>
    public class ThemeOptionTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the template for persisted, titled themes.
        /// </summary>
        public DataTemplate PersistedTitled { get; set; }

        /// <summary>
        /// Gets or sets the template for titled themes.
        /// </summary>
        public DataTemplate Titled { get; set; }

        /// <inheritdoc/>
        protected override DataTemplate SelectTemplateCore(object item)
        {
            return base.SelectTemplateCore(item);
        }

        /// <inheritdoc/>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return container is ListViewItem
                ? this.SelectTemplateForListViewItem(item) ?? base.SelectTemplateCore(item, container)
                : base.SelectTemplateCore(item, container);
        }

        /// <summary>
        /// Returns a specific <see cref="DataTemplate"/> for a given <paramref name="item"/>, assuming the container is a <see cref="ListView"/>.
        /// </summary>
        /// <param name="item">The item to return a template for.</param>
        /// <returns>The template to use for the given <paramref name="item"/> regardless of container.</returns>
        private DataTemplate SelectTemplateForListViewItem(object item)
        {
            var persistedThemeViewModel = item as PersistedTitledThemeViewModel;
            var themeViewModel = item as TitledThemeViewModel;

            if (persistedThemeViewModel != null)
            {
                return this.PersistedTitled;
            }
            else if (themeViewModel != null)
            {
                return this.Titled;
            }
            else
            {
                return null;
            }
        }
    }
}
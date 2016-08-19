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

namespace Band.Personalize.App.Universal.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Model.Library.Theme;

    /// <summary>
    /// Extension methods for converting between several types of objects that can represent a theme.
    /// </summary>
    public static class ThemeConversionExtensions
    {
        /// <summary>
        /// Convert a <see cref="RgbColorTheme"/> to a <see cref="TitledThemeViewModel"/> with the same data.
        /// </summary>
        /// <param name="model">The <see cref="RgbColorTheme"/> to convert.</param>
        /// <param name="title">The title of the <paramref name="model"/>.</param>
        /// <returns>An instance of <see cref="TitledThemeViewModel"/> with the same data as the <paramref name="model"/> with the specified <paramref name="title"/>.</returns>
        public static TitledThemeViewModel ToViewModel(this RgbColorTheme model, string title)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new TitledThemeViewModel
            {
                Title = title,
                Base = model.Base,
                HighContrast = model.HighContrast,
                Lowlight = model.Lowlight,
                Highlight = model.Highlight,
                Muted = model.Muted,
                SecondaryText = model.SecondaryText,
            };
        }

        /// <summary>
        /// Convert a <see cref="TitledThemeViewModel"/> to a <see cref="TitledRgbColorTheme"/> with the same data.
        /// </summary>
        /// <param name="viewModel">The <see cref="TitledThemeViewModel"/> to convert.</param>
        /// <returns>An instance of <see cref="TitledRgbColorTheme"/> with the same data as the <paramref name="viewModel"/>.</returns>
        public static TitledRgbColorTheme ToModel(this TitledThemeViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new TitledRgbColorTheme
            {
                Title = viewModel.Title,
                Base = viewModel.Base,
                HighContrast = viewModel.HighContrast,
                Lowlight = viewModel.Lowlight,
                Highlight = viewModel.Highlight,
                Muted = viewModel.Muted,
                SecondaryText = viewModel.SecondaryText,
            };
        }
    }
}
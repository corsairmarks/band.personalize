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

namespace Band.Personalize.App.Universal.Converters
{
    using System;
    using System.Reflection;
    using Model.Library.Color;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
    /// <summary>
    /// Value converter that translates a <see cref="RgbColor"/> to a <see cref="Windows.UI.Color"/>.
    /// </summary>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
    public class RgbColorToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Convert the <paramref name="value"/> to an equivalent representation of the <paramref name="targetType"/>,
        /// with an optional <paramref name="parameter"/> and <paramref name="language"/> for localization.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target <see cref="Type"/> to which to convert the <paramref name="value"/>.</param>
        /// <param name="parameter">A custom parameter for the conversion operation.</param>
        /// <param name="language">The language to use for localization.</param>
        /// <returns>A <paramref name="targetType"/> representation of the <paramref name="value"/> if conversion succeeded; otwerwise <see cref="DependencyProperty.UnsetValue"/>.</returns>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rgbColor = value as RgbColor;
            if (rgbColor != null && (targetType == typeof(object) || targetType == typeof(Color)))
            {
                return rgbColor.ToColor();
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Convert the <paramref name="value"/> back to an equivalent representation of the <paramref name="targetType"/>,
        /// with an optional <paramref name="parameter"/> and <paramref name="language"/> for localization.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target <see cref="Type"/> back to which to convert the <paramref name="value"/>.</param>
        /// <param name="parameter">A custom parameter for the conversion back operation.</param>
        /// <param name="language">The language to use for localization.</param>
        /// <returns>A <paramref name="targetType"/> representation of the <paramref name="value"/> if conversion succeeded; otwerwise <see cref="DependencyProperty.UnsetValue"/>.</returns>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.GetType() == typeof(Color) && (targetType == typeof(object) || targetType == typeof(RgbColor)))
            {
                var color = (Color)value;
                return color.ToRgbColor();
            }

            return DependencyProperty.UnsetValue;
        }

        #endregion
    }
}
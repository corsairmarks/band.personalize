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
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Value converter that translates a <see cref="bool"/> to a <see cref="Visibility"/>.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
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
        /// <returns>A <paramref name="targetType"/> representation of the <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="targetType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not an instance of <see cref="bool"/> or <paramref name="targetType"/> is not <see cref="Visibility"/>.</exception>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (value.GetType() != typeof(bool))
            {
                throw new ArgumentException($"Can only convert from {typeof(bool)}", nameof(value));
            }
            else if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            else if (targetType != typeof(Visibility))
            {
                throw new ArgumentException($"Can only convert to {typeof(Visibility)}", nameof(targetType));
            }

            var boolean = (bool)value;
            return boolean
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert the <paramref name="value"/> back to an equivalent representation of the <paramref name="targetType"/>,
        /// with an optional <paramref name="parameter"/> and <paramref name="language"/> for localization.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target <see cref="Type"/> back to which to convert the <paramref name="value"/>.</param>
        /// <param name="parameter">A custom parameter for the conversion back operation.</param>
        /// <param name="language">The language to use for localization.</param>
        /// <returns>A <paramref name="targetType"/> representation of the <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="targetType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not an instance of <see cref="Visibility"/> or <paramref name="targetType"/> is not <see cref="bool"/>.</exception>
        /// <exception cref="NotImplementedException"><paramref name="value"/> is not a handled value of <see cref="Visibility"/>.</exception>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (value.GetType() != typeof(Visibility))
            {
                throw new ArgumentException($"Can only convert from {typeof(Visibility)}", nameof(value));
            }
            else if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            else if (targetType != typeof(bool))
            {
                throw new ArgumentException($"Can only convert to {typeof(bool)}", nameof(targetType));
            }

            var visibility = (Visibility)value;
            switch (visibility)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Collapsed:
                    return false;
                default:
                    throw new NotImplementedException($"Unhandled {value.GetType()}: \"{value}\"");
            }
        }

        #endregion
    }
}
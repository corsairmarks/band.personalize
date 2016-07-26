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
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Value converter that translates a <see cref="byte"/> to a <see cref="double"/>.
    /// </summary>
    public class ByteToDoubleConverter : IValueConverter
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
        /// <exception cref="ArgumentException"><paramref name="value"/> is not an instance of <see cref="byte"/> or <paramref name="targetType"/> is not <see cref="double"/>.</exception>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (value.GetType() != typeof(byte))
            {
                throw new ArgumentException($"Can only convert from {typeof(byte)}", nameof(value));
            }
            else if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            else if (targetType != typeof(double))
            {
                throw new ArgumentException($"Can only convert to {typeof(double)}", nameof(targetType));
            }

            return System.Convert.ToDouble((byte)value);
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
        /// <exception cref="ArgumentOutOfRangeException">The <see cref="double"/> <paramref name="value"/> is less than <see cref="byte.MinValue"/> or greater than <see cref="byte.MaxValue"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not an instance of <see cref="double"/> or <paramref name="targetType"/> is not <see cref="byte"/>.</exception>
        /// <remarks><paramref name="parameter"/> and <paramref name="language"/> are ignored.</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (value.GetType() != typeof(double))
            {
                throw new ArgumentException($"Can only convert from {typeof(double)}", nameof(value));
            }
            else if ((double)value < byte.MinValue || (double)value > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Must be between {byte.MinValue} and {byte.MaxValue}, inclusive");
            }
            else if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            else if (targetType != typeof(byte))
            {
                throw new ArgumentException($"Can only convert to {typeof(byte)}", nameof(targetType));
            }

            return System.Convert.ToByte((double)value);
        }

        #endregion
    }
}
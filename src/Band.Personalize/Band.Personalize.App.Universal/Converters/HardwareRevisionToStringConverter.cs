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
    using Model.Library.Band;
    using Prism.Windows.AppModel;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Value converter that translates a Band connection type to a character.
    /// </summary>
    public class HardwareRevisionToStringConverter : IValueConverter
    {
        /// <summary>
        /// The resource loader.
        /// </summary>
        private readonly IResourceLoader resourceLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="HardwareRevisionToStringConverter"/> class.
        /// </summary>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resourceLoader"/> is <c>null</c>.</exception>
        public HardwareRevisionToStringConverter(IResourceLoader resourceLoader)
        {
            if (resourceLoader == null)
            {
                throw new ArgumentNullException(nameof(resourceLoader));
            }

            this.resourceLoader = resourceLoader;
        }

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
        /// <exception cref="ArgumentException"><paramref name="value"/> is not an instance of <see cref="HardwareRevision"/> or <paramref name="targetType"/> is not <see cref="string"/>.</exception>
        /// <remarks><paramref name="language"/> and <paramref name="parameter"/> are ignored.</remarks>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (!Enum.IsDefined(typeof(HardwareRevision), value))
            {
                throw new ArgumentException($"Must be a valid member of {typeof(HardwareRevision)}", nameof(value));
            }
            else if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }
            else if (targetType != typeof(string))
            {
                throw new ArgumentException($"Can only convert to {typeof(string)}", nameof(targetType));
            }

            return this.resourceLoader.GetString($"HardwareRevision.{value}");
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
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
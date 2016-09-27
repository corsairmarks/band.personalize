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

namespace Band.Personalize.Model.Library.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Band;
    using Theme;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// A facade that limits available band operations to personalization.
    /// </summary>
    public interface IBandPersonalizer
    {
        /// <summary>
        /// Sets the <paramref name="theme"/> of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band for which to set the theme.</param>
        /// <param name="theme">The theme to set.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> or <paramref name="theme"/> is <c>null</c>.</exception>
        Task SetThemeAsync(IBand band, RgbColorTheme theme, CancellationToken token);

        /// <summary>
        /// Gets the current color theme of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the theme.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current color theme when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        Task<RgbColorTheme> GetThemeAsync(IBand band, CancellationToken token);

        /// <summary>
        /// Sets the Me Tile image to the image contained in the <paramref name="bitmap"/>, which is assumed to be sized for the specified Band hardware.
        /// </summary>
        /// <param name="band">The band for which to set the Me Tile image.</param>
        /// <param name="bitmap">A bitmap that contains the image to set, with the correct dimensions.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> or <paramref name="bitmap"/> is <c>null</c>.</exception>
        Task SetMeTileImageAsync(IBand band, WriteableBitmap bitmap, CancellationToken token);

        /// <summary>
        /// Gets the current Me Tile image of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the Me Tile Image.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current Me Tile image when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        Task<WriteableBitmap> GetMeTileImageAsync(IBand band, CancellationToken token);
    }
}
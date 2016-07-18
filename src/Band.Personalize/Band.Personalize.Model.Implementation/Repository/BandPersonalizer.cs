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

namespace Band.Personalize.Model.Implementation.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Library.Band;
    using Library.Color;
    using Library.Repository;
    using Library.Theme;
    using Microsoft.Band;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// A facade that limits available band operations to personalization.
    /// </summary>
    public class BandPersonalizer : BaseBandConnectionRepository, IBandPersonalizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BandPersonalizer"/> class.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        public BandPersonalizer(IBandClientManager bandClientManager)
            : base(bandClientManager)
        {
        }

        /// <summary>
        /// Sets the <paramref name="theme"/> of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band for which to set the theme.</param>
        /// <param name="theme">The theme to set.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> or <paramref name="theme"/> is <c>null</c>.</exception>
        public async Task SetTheme(IBand band, RgbColorTheme theme, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }
            else if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            var bandTheme = new BandTheme
            {
                Base = theme.Base.ToBandColor(),
                HighContrast = theme.Base.ToBandColor(),
                Highlight = theme.Base.ToBandColor(),
                Lowlight = theme.Base.ToBandColor(),
                Muted = theme.Base.ToBandColor(),
                SecondaryText = theme.Base.ToBandColor(),
            };

            await this.ConnectAndPerformAction(band.BandInfo, token, async (bc, t) => await bc.PersonalizationManager.SetThemeAsync(bandTheme, t));
        }

        /// <summary>
        /// Gets the current color theme of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the theme.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current color theme when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        public async Task<RgbColorTheme> GetTheme(IBand band, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            var bandTheme = await this.ConnectAndPerformFunction(band.BandInfo, token, async (bc, t) => await bc.PersonalizationManager.GetThemeAsync(t));

            return bandTheme != null
                ? new RgbColorTheme
                {
                    Base = bandTheme.Base.ToRgbColor(),
                    HighContrast = bandTheme.HighContrast.ToRgbColor(),
                    Highlight = bandTheme.Highlight.ToRgbColor(),
                    Lowlight = bandTheme.Lowlight.ToRgbColor(),
                    Muted = bandTheme.Muted.ToRgbColor(),
                    SecondaryText = bandTheme.SecondaryText.ToRgbColor(),
                }
                : null;
        }

        /// <summary>
        /// Sets the Me Tile image to the image contained in the <paramref name="bitmap"/>, which is assumed to be sized for the specified Band hardware.
        /// </summary>
        /// <param name="band">The band for which to set the Me Tile image.</param>
        /// <param name="bitmap">A bitmap that contains the image to set, with the correct dimensions.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> or <paramref name="bitmap"/> is <c>null</c>.</exception>
        public async Task SetMeTileImage(IBand band, WriteableBitmap bitmap, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            await this.ConnectAndPerformAction(band.BandInfo, token, async (bc, t) => await bc.PersonalizationManager.SetMeTileImageAsync(bitmap.ToBandImage(), t));
        }

        /// <summary>
        /// Gets the current Me Tile image of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the Me Tile Image.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current Me Tile image when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        public async Task<WriteableBitmap> GetMeTileImage(IBand band, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            var bandImage = await this.ConnectAndPerformFunction(band.BandInfo, token, async (bc, t) => await bc.PersonalizationManager.GetMeTileImageAsync(t));
            return bandImage != null ? bandImage.ToWriteableBitmap() : null;
        }
    }
}
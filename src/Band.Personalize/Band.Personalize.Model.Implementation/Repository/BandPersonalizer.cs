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
        /// A function to get the current Band.
        /// </summary>
        private readonly Func<IBandInfo> getCurrentBand;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPersonalizer"/> class.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        /// <param name="getCurrentBand">A function to get the current Band.</param>
        public BandPersonalizer(IBandClientManager bandClientManager, Func<IBandInfo> getCurrentBand)
            : base(bandClientManager)
        {
            if (getCurrentBand == null)
            {
                throw new ArgumentNullException(nameof(getCurrentBand));
            }

            this.getCurrentBand = getCurrentBand;
        }

        /// <summary>
        /// Sets the <paramref name="theme"/> of the current Band.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task SetTheme(RgbColorTheme theme)
        {
            var bandTheme = new BandTheme
            {
                Base = theme.Base.ToBandColor(),
                HighContrast = theme.Base.ToBandColor(),
                Highlight = theme.Base.ToBandColor(),
                Lowlight = theme.Base.ToBandColor(),
                Muted = theme.Base.ToBandColor(),
                SecondaryText = theme.Base.ToBandColor(),
            };

            await this.ConnectAndPerformAction(this.getCurrentBand(), async bc => await bc.PersonalizationManager.SetThemeAsync(bandTheme));
        }

        /// <summary>
        /// Gets the current color theme of the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns the current color theme when it completes.</returns>
        public async Task<RgbColorTheme> GetTheme()
        {
            var bandTheme = await this.ConnectAndPerformFunction(this.getCurrentBand(), async bc => await bc.PersonalizationManager.GetThemeAsync());

            return new RgbColorTheme
            {
                Base = bandTheme.Base.ToRgbColor(),
                HighContrast = bandTheme.HighContrast.ToRgbColor(),
                Highlight = bandTheme.Highlight.ToRgbColor(),
                Lowlight = bandTheme.Lowlight.ToRgbColor(),
                Muted = bandTheme.Muted.ToRgbColor(),
                SecondaryText = bandTheme.SecondaryText.ToRgbColor(),
            };
        }

        /// <summary>
        /// Sets the Me Tile image to the image contained in the <paramref name="stream"/>, sizing it for the specified Band hardware.
        /// </summary>
        /// <param name="stream">A stream that contains the image to set.</param>
        /// <param name="hardwareSizingFor">The band version to determine the allowable Me Tile image dimensions.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task SetMeTileImage(IRandomAccessStream stream, HardwareRevision hardwareSizingFor)
        {
            var dimensions = hardwareSizingFor.GetDefaultMeTileDimensions();
            var bitmap = new WriteableBitmap(dimensions.Width, dimensions.Height);
            await bitmap.SetSourceAsync(stream);
            var meTileImage = bitmap.ToBandImage();

            await this.ConnectAndPerformAction(this.getCurrentBand(), async bc => await bc.PersonalizationManager.SetMeTileImageAsync(meTileImage));
        }

        /// <summary>
        /// Gets the current Me Tile image of the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns the current Me Tile image when it completes.</returns>
        public async Task<BitmapSource> GetMeTileImage()
        {
            return (await this.ConnectAndPerformFunction(this.getCurrentBand(), async bc => await bc.PersonalizationManager.GetMeTileImageAsync())).ToWriteableBitmap();
        }
    }
}
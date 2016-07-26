﻿// Copyright 2016 Nicholas Butcher
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

namespace Band.Personalize.App.Universal.ViewModels.Design
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// A stub for a fake <see cref="IBandPersonalizer"/>.
    /// </summary>
    internal class BandPersonalizerStub : IBandPersonalizer
    {
        /// <summary>
        /// The lazy-initialized singleton instanxe of <see cref="BandPersonalizerStub"/>.
        /// </summary>
        private static readonly Lazy<BandPersonalizerStub> LazyInstance = new Lazy<BandPersonalizerStub>(() => new BandPersonalizerStub());

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPersonalizerStub"/> class.
        /// </summary>
        private BandPersonalizerStub()
        {
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="BandPersonalizerStub"/>.
        /// </summary>
        public static IBandPersonalizer Instance
        {
            get { return LazyInstance.Value; }
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

            await Task.CompletedTask;
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

            return await Task.FromResult(band.HardwareRevision == HardwareRevision.Band
                ? DefaultThemes.Band.Blue
                : DefaultThemes.Band2.Electric);
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

            await Task.CompletedTask;
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

            var dimensions = band.HardwareRevision.GetDefaultMeTileDimensions() ?? HardwareRevision.Band.GetDefaultMeTileDimensions();
            var uri = new Uri(band.HardwareRevision == HardwareRevision.Band
                ? "ms-appx:///Assets/band.png"
                : "ms-appx:///Assets/band2.png");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var bitmap = new WriteableBitmap(dimensions.Width, dimensions.Height);
            using (var stream = await storageFile.OpenReadAsync())
            {
                await bitmap.SetSourceAsync(stream);
            }

            return bitmap;
        }
    }
}
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

namespace Band.Personalize.App.Universal.ViewModels.Design
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Model.Library.Threading;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Core;
    using Windows.Storage;
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
        public async Task SetThemeAsync(IBand band, RgbColorTheme theme, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }
            else if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            if (!DesignMode.DesignModeEnabled)
            {
                await Task.Delay(StubConstants.DefaultAsyncDelayMilliseconds, token);
            }
        }

        /// <summary>
        /// Gets the current color theme of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the theme.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current color theme when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        public async Task<RgbColorTheme> GetThemeAsync(IBand band, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            var result = band.HardwareRevision == HardwareRevision.Band
                ? DefaultThemes.Band.Blue
                : DefaultThemes.Band2.Electric;

            if (DesignMode.DesignModeEnabled)
            {
                return result;
            }
            else
            {
                return await Task.Delay(StubConstants.DefaultAsyncDelayMilliseconds, token).ContinueWith(t => result, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
        }

        /// <summary>
        /// Sets the Me Tile image to the image contained in the <paramref name="bitmap"/>, which is assumed to be sized for the specified Band hardware.
        /// </summary>
        /// <param name="band">The band for which to set the Me Tile image.</param>
        /// <param name="bitmap">A bitmap that contains the image to set, with the correct dimensions.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> or <paramref name="bitmap"/> is <c>null</c>.</exception>
        public async Task SetMeTileImageAsync(IBand band, WriteableBitmap bitmap, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            if (!DesignMode.DesignModeEnabled)
            {
                await Task.Delay(StubConstants.DefaultAsyncDelayMilliseconds, token);
            }
        }

        /// <summary>
        /// Gets the current Me Tile image of the <paramref name="band"/>.
        /// </summary>
        /// <param name="band">The band from which to get the Me Tile Image.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the current Me Tile image when it completes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="band"/> is <c>null</c>.</exception>
        public async Task<WriteableBitmap> GetMeTileImageAsync(IBand band, CancellationToken token)
        {
            if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            var dimensions = band.HardwareRevision.GetDefaultMeTileDimensions() ?? HardwareRevision.Band.GetDefaultMeTileDimensions();
            var uri = new Uri(band.HardwareRevision == HardwareRevision.Band
                ? "ms-appx:///Assets/band.png"
                : "ms-appx:///Assets/band2.png");
            var meTileBitmap = await Task
                .Delay(!DesignMode.DesignModeEnabled ? StubConstants.DefaultAsyncDelayMilliseconds : 0, token)
                .ContinueWith(
                    async t => await CoreApplication.MainView.CoreWindow.Dispatcher.WaitForRunAsync(async () =>
                    {
                        var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
                        var bitmap = new WriteableBitmap(dimensions.Width, dimensions.Height);
                        using (var stream = await storageFile.OpenReadAsync())
                        {
                            await bitmap.SetSourceAsync(stream);
                        }

                        return bitmap;
                    }),
                    TaskContinuationOptions.OnlyOnRanToCompletion)
                .Unwrap();

            return meTileBitmap;
        }
    }
}
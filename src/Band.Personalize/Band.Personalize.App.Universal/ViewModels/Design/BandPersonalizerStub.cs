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
        /// Sets the <paramref name="theme"/> of the current Band.
        /// </summary>
        /// <param name="theme">The theme to set.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task SetTheme(RgbColorTheme theme)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets the current color theme of the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns the current color theme when it completes.</returns>
        public async Task<RgbColorTheme> GetTheme()
        {
            return await Task.FromResult(DefaultThemes.Band2.Electric);
        }

        /// <summary>
        /// Sets the Me Tile image to the image contained in the <paramref name="stream"/>, sizing it for the specified Band hardware.
        /// </summary>
        /// <param name="stream">A stream that contains the image to set.</param>
        /// <param name="hardwareSizingFor">The band version to determine the allowable Me Tile image dimensions.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task SetMeTileImage(IRandomAccessStream stream, HardwareRevision hardwareSizingFor)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets the current Me Tile image of the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns the current Me Tile image when it completes.</returns>
        public async Task<BitmapSource> GetMeTileImage()
        {
            return await Task.FromResult(new BitmapImage(new Uri("ms-appx:///Assets/band2.png")));
        }
    }
}
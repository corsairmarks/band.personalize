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

namespace Band.Fake
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Band;
    using Microsoft.Band.Personalization;
    using Personalize.Model.Library.Color;
    using Personalize.Model.Library.Theme;
    using Personalize.Model.Library.Threading;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// A fake implementation of <see cref="IBandPersonalizationManager"/>.
    /// </summary>
    public class FakePersonalizationManager : IBandPersonalizationManager
    {
        /// <summary>
        /// The fake <see cref="IBandInfo"/> associated that is being personalized.
        /// </summary>
        private readonly FakeBandInfo fakeBandInfo;

        /// <summary>
        /// The delay added to all Band operations.
        /// </summary>
        private readonly int delay;

        /// <summary>
        /// An instance of <see cref="Random"/> to use for choosing fake Band data.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePersonalizationManager"/> class.
        /// </summary>
        /// <param name="fakeBandInfo">The fake <see cref="IBandInfo"/> associated that is being personalized.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fakeBandInfo"/> is <c>null</c>.</exception>
        public FakePersonalizationManager(FakeBandInfo fakeBandInfo)
            : this(fakeBandInfo, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakePersonalizationManager"/> class.
        /// </summary>
        /// <param name="fakeBandInfo">The fake <see cref="IBandInfo"/> associated that is being personalized.</param>
        /// <param name="delay">The delay in milliseconds to be added to all Band operations.  Automatically sets values less than 0 to 0.</param>
        /// <param name="random">An instance of <see cref="Random"/> to use for choosing fake Band data.  Passing null results in a new instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fakeBandInfo"/> is <c>null</c>.</exception>
        public FakePersonalizationManager(FakeBandInfo fakeBandInfo, int delay, Random random)
        {
            if (fakeBandInfo == null)
            {
                throw new ArgumentNullException(nameof(fakeBandInfo));
            }

            this.delay = delay >= 0 ? delay : 0;
            this.random = random ?? new Random();
            this.fakeBandInfo = fakeBandInfo;
        }

        /// <summary>
        /// Get the Me Tile image.
        /// </summary>
        /// <returns>An asynchronous task that returns the Me Tile image when it completes.</returns>
        public Task<BandImage> GetMeTileImageAsync()
        {
            return this.GetMeTileImageAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Me Tile image.
        /// </summary>
        /// <param name="cancel">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the Me Tile image when it completes.</returns>
        public async Task<BandImage> GetMeTileImageAsync(CancellationToken cancel)
        {
            return await Task
                .Delay(this.delay, cancel)
                .ContinueWith(
                    async t => await CoreApplication.MainView.CoreWindow.Dispatcher.WaitForRunAsync(async () =>
                    {
                        if (this.fakeBandInfo.MeTile == null)
                        {
                            string imagePath;
                            int imageHeight;
                            if (this.IsOriginalBand())
                            {
                                imagePath = "Band.Fake.Assets.band.png";
                                imageHeight = 102;
                            }
                            else
                            {
                                imagePath = "Band.Fake.Assets.band2.png";
                                imageHeight = 128;
                            }

                            var bitmap = new WriteableBitmap(310, imageHeight);
                            using (var s = this.GetType().GetTypeInfo().Assembly.GetManifestResourceStream(imagePath))
                            {
                                await bitmap.SetSourceAsync(s.AsRandomAccessStream());
                            }

                            this.fakeBandInfo.MeTile = bitmap.ToBandImage();
                        }

                        return this.fakeBandInfo.MeTile;
                    }),
                    cancel,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext())
                .Unwrap();
        }

        /// <summary>
        /// Get the theme.
        /// </summary>
        /// <returns>An asynchronous task that returns the Band theme image when it completes.</returns>
        public Task<BandTheme> GetThemeAsync()
        {
            return this.GetThemeAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the theme.
        /// </summary>
        /// <param name="cancel">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the Band theme image when it completes.</returns>
        public async Task<BandTheme> GetThemeAsync(CancellationToken cancel)
        {
            return await Task
                .Delay(this.delay, cancel)
                .ContinueWith(
                    t =>
                    {
                        if (this.fakeBandInfo.Theme == null)
                        {
                            var defaults = this.IsOriginalBand()
                                ? DefaultThemes.Band.DefaultThemes
                                : DefaultThemes.Band2.DefaultThemes;
                            var randTheme = defaults[this.random.Next(0, defaults.Count)];

                            this.fakeBandInfo.Theme = new BandTheme
                            {
                                Base = randTheme.Base.ToBandColor(),
                                HighContrast = randTheme.HighContrast.ToBandColor(),
                                Lowlight = randTheme.Lowlight.ToBandColor(),
                                Highlight = randTheme.Highlight.ToBandColor(),
                                Muted = randTheme.Muted.ToBandColor(),
                                SecondaryText = randTheme.SecondaryText.ToBandColor(),
                            };
                        }

                        return this.fakeBandInfo.Theme;
                    },
                    cancel,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Set the Me Tile image.
        /// </summary>
        /// <param name="image">The Me Tile image to "apply" to the Band.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public Task SetMeTileImageAsync(BandImage image)
        {
            return this.SetMeTileImageAsync(image, CancellationToken.None);
        }

        /// <summary>
        /// Set the Me Tile image.
        /// </summary>
        /// <param name="image">The Me Tile image to "apply" to the Band.</param>
        /// <param name="cancel">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel)
        {
            return Task.Delay(this.delay, cancel).ContinueWith(t => this.fakeBandInfo.MeTile = image, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        /// <summary>
        /// Set the theme.
        /// </summary>
        /// <param name="theme">The theme to "apply" to the Band.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public Task SetThemeAsync(BandTheme theme)
        {
            return this.SetThemeAsync(theme, CancellationToken.None);
        }

        /// <summary>
        /// Set the theme.
        /// </summary>
        /// <param name="theme">The theme to "apply" to the Band.</param>
        /// <param name="cancel">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public Task SetThemeAsync(BandTheme theme, CancellationToken cancel)
        {
            return Task.Delay(this.delay, cancel).ContinueWith(t => this.fakeBandInfo.Theme = theme, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        /// <summary>
        /// Determines whether the <see cref="fakeBandInfo"/> is a Band or Band 2.
        /// </summary>
        /// <returns><c>true</c> if the <see cref="fakeBandInfo"/> is considered an original Band; otherwise, <c>false</c>.</returns>
        private bool IsOriginalBand()
        {
            int version;
            return int.TryParse(this.fakeBandInfo.HardwareVersion, out version) && version < 20;
        }
    }
}
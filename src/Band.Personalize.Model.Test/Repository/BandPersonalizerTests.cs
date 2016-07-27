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

namespace Band.Personalize.Model.Test.Repository
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Implementation.Repository;
    using Library.Band;
    using Library.Theme;
    using Library.Threading;
    using Microsoft.Band;
    using Microsoft.Band.Personalization;
    using Moq;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml.Media.Imaging;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandPersonalizer"/> class.
    /// </summary>
    public class BandPersonalizerTests
    {
        /// <summary>
        /// The <see cref="Mock"/> repository.
        /// </summary>
        private static readonly MockRepository MockRepository = new MockRepository(MockBehavior.Strict);

        /// <summary>
        /// Verify the <see cref="BandPersonalizer(IBandClientManager)"/> constructor throws an
        /// <see cref="ArgumentNullException"/> when the <see cref="IBandClientManager"/> parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void Ctor_NullBandClientManager_Throws()
        {
            // Arrange
            IBandClientManager bandClientManager = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => new BandPersonalizer(bandClientManager));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandClientManager", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer(IBandClientManager)"/> constructor creates an instance when provided valid parameter(s).
        /// </summary>
        [Fact]
        public void Ctor_CreatesInstance()
        {
            // Arrange
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();

            // Act
            var result = new BandPersonalizer(bandClientManager);

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetTheme(IBand, RgbColorTheme, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="IBand"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetTheme_NullBand_Throws()
        {
            // Arrange
            IBand band = null;
            var theme = DefaultThemes.Band2.Electric;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.SetTheme(band, theme, token));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("band", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetTheme(IBand, RgbColorTheme, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="RgbColorTheme"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetTheme_NullTheme_Throws()
        {
            // Arrange
            var band = MockRepository.OneOf<IBand>();
            RgbColorTheme theme = null;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.SetTheme(band, theme, token));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("theme", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetTheme(IBand, RgbColorTheme, CancellationToken)"/> method calls the associated
        /// <see cref="IBandPersonalizationManager"/> method with passed-through parameters.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetTheme_CallsSetThemeAsyncWithConvertedTheme()
        {
            // Arrange
            var bandInfo = MockRepository.OneOf<IBandInfo>();
            var mockBand = MockRepository.Create<IBand>();
            mockBand.SetupGet(b => b.BandInfo).Returns(bandInfo);
            var band = mockBand.Object;
            var theme = DefaultThemes.Band2.Electric;
            var token = new CancellationToken(false);
            var target = new BandPersonalizer(GetMockedBandClientManagerAsync(pm => pm.SetThemeAsync(It.IsNotNull<BandTheme>(), token)));

            // Act
            await target.SetTheme(band, theme, token);

            // Assert: passes as long as an exception is not thrown
            // TODO: Verify the expected method was called? with the equivalent theme?
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.GetTheme(IBand, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="IBand"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task GetTheme_NullBand_Throws()
        {
            // Arrange
            IBand band = null;
            var theme = DefaultThemes.Band2.Electric;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.GetTheme(band, token));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("band", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.GetTheme(IBand, CancellationToken)"/> method calls the associated
        /// <see cref="IBandPersonalizationManager"/> method with passed-through parameters.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task GetTheme_CallsGetThemeAsync_ReturnsConvertedTheme()
        {
            // Arrange
            var bandInfo = MockRepository.OneOf<IBandInfo>();
            var mockBand = MockRepository.Create<IBand>();
            mockBand.SetupGet(b => b.BandInfo).Returns(bandInfo);
            var band = mockBand.Object;
            var theme = DefaultThemes.Band2.Electric;
            var token = new CancellationToken(false);
            var expectedBandTheme = new BandTheme();
            var target = new BandPersonalizer(GetMockedBandClientManagerAsync(pm => pm.GetThemeAsync(token), expectedBandTheme));

            // Act
            var result = await target.GetTheme(band, token);

            // Assert
            // TODO: Verify the expected method was called?
            Assert.NotNull(result);

            // TODO: Assert result equals expectedBandTheme
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetMeTileImage(IBand, WriteableBitmap, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="IBand"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetMeTileImage_NullBand_Throws()
        {
            // Arrange
            IBand band = null;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);
            await CoreApplication.MainView.CoreWindow.Dispatcher.WaitForRunAsync(async () =>
            {
                var bitmap = new WriteableBitmap(1, 1);

                // Act
                var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.SetMeTileImage(band, bitmap, token));

                // Assert
                Assert.NotNull(expected);
                Assert.Equal("band", expected.ParamName);
            });
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetMeTileImage(IBand, WriteableBitmap, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="WriteableBitmap"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetMeTileImage_NullBitmap_Throws()
        {
            // Arrange
            var band = MockRepository.OneOf<IBand>();
            WriteableBitmap bitmap = null;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.SetMeTileImage(band, bitmap, token));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bitmap", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.SetMeTileImage(IBand, WriteableBitmap, CancellationToken)"/> method calls the associated
        /// <see cref="IBandPersonalizationManager"/> method with passed-through parameters.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task SetMeTileImage_CallsSetMeTileImageAsyncWithConvertedTheme()
        {
            // Arrange
            var bandInfo = MockRepository.OneOf<IBandInfo>();
            var mockBand = MockRepository.Create<IBand>();
            mockBand.SetupGet(b => b.BandInfo).Returns(bandInfo);
            var band = mockBand.Object;
            var token = new CancellationToken(false);
            var target = new BandPersonalizer(GetMockedBandClientManagerAsync(pm => pm.SetMeTileImageAsync(It.IsNotNull<BandImage>(), token)));
            await CoreApplication.MainView.CoreWindow.Dispatcher.WaitForRunAsync(async () =>
            {
                var bitmap = new WriteableBitmap(1, 1);

                // Act
                await target.SetMeTileImage(band, bitmap, token);
            });

            // Assert: passes as long as an exception is not thrown
            // TODO: Verify the expected method was called? with the equivalent bitmap?
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.GetMeTileImage(IBand, CancellationToken)"/> method throws an <see cref="ArgumentNullException"/>
        /// when the <see cref="IBand"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task GetMeTileImage_NullBand_Throws()
        {
            // Arrange
            IBand band = null;
            var token = new CancellationToken(false);
            var bandClientManager = MockRepository.OneOf<IBandClientManager>();
            var target = new BandPersonalizer(bandClientManager);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(async () => await target.GetMeTileImage(band, token));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("band", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandPersonalizer.GetMeTileImage(IBand, CancellationToken)"/> method calls the associated
        /// <see cref="IBandPersonalizationManager"/> method with passed-through parameters.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task GetMeTileImage_CallsGetMeTileImageAsync_ReturnsWriteableBitmap()
        {
            // Arrange
            var bandInfo = MockRepository.OneOf<IBandInfo>();
            var mockBand = MockRepository.Create<IBand>();
            mockBand.SetupGet(b => b.BandInfo).Returns(bandInfo);
            var band = mockBand.Object;
            var token = new CancellationToken(false);
            await CoreApplication.MainView.CoreWindow.Dispatcher.WaitForRunAsync(async () =>
            {
                var expectedBandImage = new WriteableBitmap(1, 1).ToBandImage();
                var target = new BandPersonalizer(GetMockedBandClientManagerAsync(pm => pm.GetMeTileImageAsync(token), expectedBandImage));

                // Act
                var result = await target.GetMeTileImage(band, token);

                // Assert
                // TODO: Verify the expected method was called?
                Assert.NotNull(result);
                TestHelper.AssertWriteableBitmapPixelBuffersEqual(expectedBandImage.ToWriteableBitmap(), result);
            });
        }

        /// <summary>
        /// Creates a mock <see cref="IBandClientManager"/> that will return a mock <see cref="IBandClient"/> with a mock
        /// <see cref="IBandPersonalizationManager"/> that has been configured with <paramref name="expectedPersonalizationManagerAction"/>.
        /// </summary>
        /// <param name="expectedPersonalizationManagerAction">An action to setup on the <see cref="IBandPersonalizationManager"/>.</param>
        /// <returns>A mock <see cref="IBandClientManager"/>.</returns>
        private static IBandClientManager GetMockedBandClientManagerAsync(Expression<Func<IBandPersonalizationManager, Task>> expectedPersonalizationManagerAction)
        {
            return GetMockedBandClientManager(mpm => mpm.Setup(expectedPersonalizationManagerAction).Returns(Task.CompletedTask));
        }

        /// <summary>
        /// Creates a mock <see cref="IBandClientManager"/> that will return a mock <see cref="IBandClient"/> with a mock
        /// <see cref="IBandPersonalizationManager"/> that has been configured with <paramref name="expectedPersonalizationManagerFunction"/>.
        /// </summary>
        /// <typeparam name="T">The return <see cref="Type"/> of <paramref name="expectedPersonalizationManagerFunction"/>.</typeparam>
        /// <param name="expectedPersonalizationManagerFunction">A function to setup on the <see cref="IBandPersonalizationManager"/>.</param>
        /// <param name="bandClientReturnValue">The value to return when the <paramref name="expectedPersonalizationManagerFunction"/> is called.</param>
        /// <returns>A mock <see cref="IBandClientManager"/>.</returns>
        private static IBandClientManager GetMockedBandClientManagerAsync<T>(Expression<Func<IBandPersonalizationManager, Task<T>>> expectedPersonalizationManagerFunction, T bandClientReturnValue)
        {
            return GetMockedBandClientManager(mpm => mpm.Setup(expectedPersonalizationManagerFunction).Returns(Task.FromResult(bandClientReturnValue)));
        }

        /// <summary>
        /// Creates a mock <see cref="IBandClientManager"/> that will return a mock <see cref="IBandClient"/> with a mock
        /// <see cref="IBandPersonalizationManager"/> that has been configured with <paramref name="configure"/>.
        /// </summary>
        /// <param name="configure">An action to configure the <see cref="IBandPersonalizationManager"/>.</param>
        /// <returns>A mock <see cref="IBandClientManager"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="configure"/> is <c>null</c>.</exception>
        private static IBandClientManager GetMockedBandClientManager(Action<Mock<IBandPersonalizationManager>> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var mockPersonalizationManager = MockRepository.Create<IBandPersonalizationManager>();
            configure(mockPersonalizationManager);

            var mockBandClient = MockRepository.Create<IBandClient>();
            mockBandClient.SetupGet(bc => bc.PersonalizationManager).Returns(mockPersonalizationManager.Object);
            mockBandClient.Setup(bc => bc.Dispose());

            var mockBandClientManager = MockRepository.Create<IBandClientManager>();
            mockBandClientManager
                .Setup(bcm => bcm.ConnectAsync(It.IsNotNull<IBandInfo>()))
                .Returns(Task.FromResult(mockBandClient.Object));
            return mockBandClientManager.Object;
        }
    }
}
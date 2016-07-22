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
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Implementation.Repository;
    using Library.Band;
    using Microsoft.Band;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandRepository"/> class.
    /// </summary>
    public class BandRepositoryTests
    {
        /// <summary>
        /// The <see cref="Mock"/> repository.
        /// </summary>
        private static readonly MockRepository MockRepository = new MockRepository(MockBehavior.Strict);

        /// <summary>
        /// Gets sample Band version strings mapped to the expected <see cref="Nullable{Int32}"/> representation.
        /// </summary>
        public static IEnumerable<object[]> BandHardwareVersions
        {
            get
            {
                return new[]
                {
                    new object[] { "1", 1, },
                    new object[] { "foo", null, },
                    new object[] { null, null, },
                };
            }
        }

        /// <summary>
        /// Verify the <see cref="BandRepository(IBandClientManager)"/> constructor throws an
        /// <see cref="ArgumentNullException"/> when the <see cref="IBandClientManager"/> parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void Ctor_NullBandClientManager_Throws()
        {
            // Arrange
            IBandClientManager bandClientManager = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => new BandRepository(bandClientManager));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandClientManager", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandRepository(IBandClientManager)"/> constructor creates an instance when provided valid parameter(s).
        /// </summary>
        [Fact]
        public void Ctor_CreatesInstance()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;

            // Act
            var result = new BandRepository(bandClientManager);

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Verify the <see cref="BandRepository.GetBands(CancellationToken)"/> method finds all connected Bands and returns
        /// information about then, including the hardware revision.
        /// </summary>
        /// <param name="hardwareVersion">The string hardware representation to return as sample data.</param>
        /// <param name="expectedHardwareVersion">The expected <see cref="Nullable{Int32}"/> of <paramref name="hardwareVersion"/>.</param>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Theory]
        [MemberData(nameof(BandHardwareVersions))]
        public async Task GetBands_ReturnsBands(string hardwareVersion, int? expectedHardwareVersion)
        {
            // Arrange
            var mockBandInfo = MockRepository.Create<IBandInfo>();
            mockBandInfo.SetupGet(bi => bi.Name).Returns("Name");
            mockBandInfo.SetupGet(bi => bi.ConnectionType).Returns(BandConnectionType.Bluetooth);
            var bandInfo = mockBandInfo.Object;
            var token = new CancellationToken(false);
            var mockBandClient = MockRepository.Create<IBandClient>();
            mockBandClient.Setup(bc => bc.GetHardwareVersionAsync(It.IsIn(token))).Returns(Task.FromResult(hardwareVersion));
            mockBandClient.Setup(bc => bc.Dispose());
            var bandClient = mockBandClient.Object;
            var mockBandClientManager = MockRepository.Create<IBandClientManager>();
            mockBandClientManager.Setup(bcm => bcm.GetBandsAsync()).Returns(Task.FromResult(new[] { bandInfo, }));
            mockBandClientManager.Setup(bcm => bcm.ConnectAsync(bandInfo)).Returns(Task.FromResult(bandClient));
            var bandClientManager = mockBandClientManager.Object;
            var target = new BandRepository(bandClientManager);

            // Act
            var results = await target.GetBands(token);

            // Assert
            Assert.NotNull(results);
            Assert.Collection(results, b =>
            {
                Assert.StrictEqual(bandInfo, b.BandInfo);
                Assert.Equal(bandInfo.Name, b.Name);
                Assert.Equal(bandInfo.ConnectionType.ToConnectionType(), b.ConnectionType);
                Assert.Equal(expectedHardwareVersion, b.HardwareVersion);
                Assert.Equal(expectedHardwareVersion.ToHardwareRevision(), b.HardwareRevision);
            });
        }
    }
}
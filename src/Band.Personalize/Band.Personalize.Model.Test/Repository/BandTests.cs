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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.Band;
    using Microsoft.Band;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Implementation.Repository.Band"/> class.
    /// </summary>
    public class BandTests
    {
        /// <summary>
        /// The <see cref="Mock"/> repository.
        /// </summary>
        private static readonly MockRepository MockRepository = new MockRepository(MockBehavior.Strict);

        /// <summary>
        /// Verify the <see cref="Implementation.Repository.Band(IBandInfo, int?)"/> constructor throws an
        /// <see cref="ArgumentNullException"/> when the <see cref="IBandInfo"/> parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void Ctor_NullBandClientManager_Throws()
        {
            // Arrange
            IBandInfo bandInfo = null;
            int? hardwareVersion = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => new Implementation.Repository.Band(bandInfo, hardwareVersion));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandInfo", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="Implementation.Repository.Band(IBandInfo, int?)"/> constructor creates an
        /// instance when provided valid parameter(s).  <c>null</c> is a vlaid parameter for the hardware version.
        /// </summary>
        [Fact]
        public void Ctor_NullHardwareVersion_CreatesInstance()
        {
            // Arrange
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            int? hardwareVersion = null;

            // Act
            var result = new Implementation.Repository.Band(bandInfo, hardwareVersion);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.HardwareVersion);
            Assert.Equal(hardwareVersion.ToHardwareRevision(), result.HardwareRevision);
        }

        /// <summary>
        /// Verify the <see cref="Implementation.Repository.Band(IBandInfo, int?)"/> constructor creates an
        /// instance when provided valid parameter(s).
        /// </summary>
        [Fact]
        public void Ctor_CreatesInstance()
        {
            // Arrange
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var hardwareVersion = 1;

            // Act
            var result = new Implementation.Repository.Band(bandInfo, hardwareVersion);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hardwareVersion, result.HardwareVersion);
            Assert.Equal(hardwareVersion.ToHardwareRevision(), result.HardwareRevision);
        }

        /// <summary>
        /// Verify the <see cref="Implementation.Repository.Band.BandInfo"/>, <see cref="Implementation.Repository.Band.Name"/>,
        /// and <see cref="Implementation.Repository.Band.ConnectionType"/> properties return the expected data.
        /// </summary>
        [Fact]
        public void Properties_ReturnValuesFromIBandInfo()
        {
            // Arrange
            var mockBandInfo = MockRepository.Create<IBandInfo>();
            mockBandInfo.SetupGet(bi => bi.Name).Returns("Name");
            mockBandInfo.SetupGet(bi => bi.ConnectionType).Returns(BandConnectionType.Bluetooth);
            var bandInfo = mockBandInfo.Object;
            var hardwareVersion = 1;

            // Act
            var result = new Implementation.Repository.Band(bandInfo, hardwareVersion);

            // Assert
            Assert.NotNull(result);
            Assert.StrictEqual(bandInfo, result.BandInfo);
            Assert.Equal(bandInfo.Name, result.Name);
            Assert.Equal(bandInfo.ConnectionType.ToConnectionType(), result.ConnectionType);
        }
    }
}
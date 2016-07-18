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

namespace Band.Personalize.Model.Test.Band
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="HardwareRevisionExtensions"/> class.
    /// </summary>
    public class HardwareRevisionExtensionsTests
    {
        /// <summary>
        /// A map of hardware version to major hardware revision.
        /// </summary>
        public static readonly IEnumerable<object[]> HardwareVersionToHardwareRevisionMap = Enumerable.Range(-1, 21).Select(i => new object[] { i, HardwareRevision.Band, }).Concat(Enumerable.Range(20, 10).Select(i => new object[] { i, HardwareRevision.Band2, }));

        /// <summary>
        /// A map of hardware version to major hardware revision, including <c>null</c>.
        /// </summary>
        public static readonly IEnumerable<object[]> NullableHardwareVersionToHardwareRevisionMap = HardwareVersionToHardwareRevisionMap.Concat(new[] { new object[] { null as int?, HardwareRevision.Unknown, }, });

        /// <summary>
        /// A map of hardware revision to allowed Me Tile image dimensions.
        /// </summary>
        public static readonly IEnumerable<object[]> BandHardwareRevisionToAllowedImageDimensionsMap = new[]
        {
            new object[] { HardwareRevision.Unknown, new Dimension2D[0], },
            new object[] { HardwareRevision.Band, new[] { new Dimension2D(310, 102), } },
            new object[] { HardwareRevision.Band2, new[] { new Dimension2D(310, 102), new Dimension2D(310, 128), } },
        };

        /// <summary>
        /// A map of hardware revision to allowed Me Tile image dimensions.
        /// </summary>
        public static readonly IEnumerable<object[]> BandHardwareRevisionToDefaultImageDimensionsMap = new[]
        {
            new object[] { HardwareRevision.Unknown, null, },
            new object[] { HardwareRevision.Band, new Dimension2D(310, 102), },
            new object[] { HardwareRevision.Band2, new Dimension2D(310, 128), },
        };

        /// <summary>
        /// Verify the <see cref="HardwareRevisionExtensions.ToHardwareRevision(int)"/> method converts values below 20
        /// to <see cref="HardwareRevision.Band"/> and values 20 and above to <see cref="HardwareRevision.Band2"/>.
        /// </summary>
        /// <param name="hardwareVersion">The specific hardware version.</param>
        /// <param name="expectedHardwareRevision">The expected result.</param>
        [Theory]
        [MemberData(nameof(HardwareVersionToHardwareRevisionMap))]
        public void ToHardwareRevision_ConvertsToCorrectValue(int hardwareVersion, HardwareRevision expectedHardwareRevision)
        {
            // Act
            var result = hardwareVersion.ToHardwareRevision();

            // Assert
            Assert.Equal(expectedHardwareRevision, result);
        }

        /// <summary>
        /// Verify the <see cref="HardwareRevisionExtensions.ToHardwareRevision(int)"/> method converts values below 20
        /// to <see cref="HardwareRevision.Band"/> and values 20 and above to <see cref="HardwareRevision.Band2"/>.
        /// </summary>
        /// <param name="hardwareVersion">The specific hardware version.</param>
        /// <param name="expectedHardwareRevision">The expected result.</param>
        [Theory]
        [MemberData(nameof(NullableHardwareVersionToHardwareRevisionMap))]
        public void NullableToHardwareRevision_ConvertsToCorrectValue(int? hardwareVersion, HardwareRevision expectedHardwareRevision)
        {
            // Act
            var result = hardwareVersion.ToHardwareRevision();

            // Assert
            Assert.Equal(expectedHardwareRevision, result);
        }

        /// <summary>
        /// Verify the <see cref="HardwareRevisionExtensions.GetAllowedMeTileDimensions(HardwareRevision)"/> method throws a
        /// <see cref="NotImplementedException"/> when the <see cref="HardwareRevision"/> is not a defined value.
        /// </summary>
        [Fact]
        public void GetAllowedMeTileDimensions_UnmappedHardwareRevision_Throws()
        {
            // Arrange
            var target = (HardwareRevision)13;

            // Act/Assert
            var expected = Assert.Throws<NotImplementedException>(() => target.GetAllowedMeTileDimensions());
        }

        /// <summary>
        /// Verfiy the <see cref="HardwareRevisionExtensions.GetAllowedMeTileDimensions(HardwareRevision)"/> method returns the expected dimension options.
        /// </summary>
        /// <param name="hardwareRevision">The <see cref="HardwareRevision"/> to test.</param>
        /// <param name="expectedDimensions">The expected result.</param>
        [Theory]
        [MemberData(nameof(BandHardwareRevisionToAllowedImageDimensionsMap))]
        public void GetAllowedMeTileDimensions_ReturnsCorrectOptions(HardwareRevision hardwareRevision, IEnumerable<Dimension2D> expectedDimensions)
        {
            // Act
            var result = hardwareRevision.GetAllowedMeTileDimensions();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDimensions, result);
        }

        /// <summary>
        /// Verify the <see cref="HardwareRevisionExtensions.GetDefaultMeTileDimensions(HardwareRevision)"/> method throws a
        /// <see cref="NotImplementedException"/> when the <see cref="HardwareRevision"/> is not a defined value.
        /// </summary>
        [Fact]
        public void GetDefaultMeTileDimensions_UnmappedHardwareRevision_Throws()
        {
            // Arrange
            var target = (HardwareRevision)13;

            // Act/Assert
            var expected = Assert.Throws<NotImplementedException>(() => target.GetDefaultMeTileDimensions());
        }

        /// <summary>
        /// Verfiy the <see cref="HardwareRevisionExtensions.GetDefaultMeTileDimensions(HardwareRevision)"/> method returns the expected dimensions.
        /// </summary>
        /// <param name="hardwareRevision">The <see cref="HardwareRevision"/> to test.</param>
        /// <param name="expectedDimensions">The expected result.</param>
        [Theory]
        [MemberData(nameof(BandHardwareRevisionToDefaultImageDimensionsMap))]
        public void GetDefaultMeTileDimensions_ReturnsCorrectDefault(HardwareRevision hardwareRevision, Dimension2D expectedDimensions)
        {
            // Act
            var result = hardwareRevision.GetDefaultMeTileDimensions();

            // Assert
            Assert.Equal(expectedDimensions, result);
        }
    }
}
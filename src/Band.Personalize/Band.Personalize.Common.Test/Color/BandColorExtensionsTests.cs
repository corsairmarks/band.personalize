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

namespace Band.Personalize.Common.Test.Color
{
    using Common.Color;
    using Data;
    using Microsoft.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandColorExtensions"/> class.
    /// </summary>
    public class BandColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="BandColorExtensions.ToHexadecimalColor(BandColor)"/> method maps
        /// the correct fields from an instance of <see cref="BandColor"/> to an instance of
        /// <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToHexadecimalColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new BandColor(red, green, blue);

            // Act
            var result = target.ToHexadecimalColor();

            // Assert
            Assert.Equal(target.R, result.Red);
            Assert.Equal(target.G, result.Green);
            Assert.Equal(target.B, result.Blue);
        }
    }
}
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

namespace Band.Personalize.Model.Test.Color
{
    using System;
    using Data;
    using Library.Color;
    using Microsoft.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="RgbColorExtensions"/> class.
    /// </summary>
    public class RgbColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToBandColor(RgbColor)"/> method maps
        /// the correct fields from an instance of <see cref="RgbColor"/> to an instance of
        /// <see cref="BandColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToBandColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.ToBandColor();

            // Assert
            Assert.Equal(target.Red, result.R);
            Assert.Equal(target.Green, result.G);
            Assert.Equal(target.Blue, result.B);
        }

        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToBandColor(RgbColor)"/> method throws
        /// an <see cref="ArgumentNullException"/> when the color parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void ToBandColor_NullColor_Throws()
        {
            // Arrange
            RgbColor target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => target.ToBandColor());

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("color", expected.ParamName);
        }

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToColor(RgbColor)"/> method maps
        /// the correct fields from an instance of <see cref="RgbColor"/> to an instance of
        /// <see cref="Windows.UI.Color"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.ToColor();

            // Assert
            Assert.Equal(byte.MaxValue, result.A);
            Assert.Equal(target.Red, result.R);
            Assert.Equal(target.Green, result.G);
            Assert.Equal(target.Blue, result.B);
        }

        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToArgbColor(RgbColor)"/> method maps
        /// the correct fields from an instance of <see cref="RgbColor"/> to an instance of
        /// <see cref="ArgbColor"/>.  The <see cref="ArgbColor.Alpha"/> channel is set to
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToArgbColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.ToArgbColor();

            // Assert
            Assert.Equal(byte.MaxValue, result.Alpha);
            Assert.Equal(target.Red, result.Red);
            Assert.Equal(target.Green, result.Green);
            Assert.Equal(target.Blue, result.Blue);
        }

        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToColor(RgbColor)"/> method throws
        /// an <see cref="ArgumentNullException"/> when the color parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void ToColor_NullColor_Throws()
        {
            // Arrange
            RgbColor target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => target.ToColor());

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("color", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="RgbColorExtensions.ToArgbColor(RgbColor)"/> method throws
        /// an <see cref="ArgumentNullException"/> when the color parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void ToArgbColor_NullColor_Throws()
        {
            // Arrange
            RgbColor target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => target.ToArgbColor());

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("color", expected.ParamName);
        }
    }
}
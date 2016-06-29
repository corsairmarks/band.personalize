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
    using Data;
    using Library.Color;
    using Windows.UI;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ColorExtensions"/> class.
    /// </summary>
    public class ColorExtensionsTests
    {
#pragma warning disable CS0419 // Ambiguous reference in cref attribute
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#pragma warning disable CS1580 // Invalid type for parameter in XML comment cref attribute
        /// <summary>
        /// Verify the <see cref="ColorExtensions.ToRgbColor(Windows.UI.Color)"/> method maps
        /// the correct fields from an instance of <see cref="Windows.UI.Color"/> to an instance of
        /// <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
#pragma warning restore CS1580 // Invalid type for parameter in XML comment cref attribute
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToRgbColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new Color
            {
                R = red,
                G = green,
                B = blue,
            };

            // Act
            var result = target.ToRgbColor();

            // Assert
            Assert.Equal(target.R, result.Red);
            Assert.Equal(target.G, result.Green);
            Assert.Equal(target.B, result.Blue);
        }
    }
}
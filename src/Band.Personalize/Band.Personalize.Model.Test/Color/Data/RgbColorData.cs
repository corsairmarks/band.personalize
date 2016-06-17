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

namespace Band.Personalize.Model.Test.Color.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.Color;

    /// <summary>
    /// Test data for use with <see cref="RgbColorTests"/>.
    /// </summary>
    public static class RgbColorData
    {
        /// <summary>
        /// The default red channel saturation used for testing.
        /// </summary>
        public const byte DefaultRed = 0x6A;

        /// <summary>
        /// The default green channel saturation used for testing.
        /// </summary>
        public const byte DefaultGreen = 0x00;

        /// <summary>
        /// The default blue channel saturation used for testing.
        /// </summary>
        public const byte DefaultBlue = 0xFF;

        /// <summary>
        /// Gets the default color, constructed with <see cref="DefaultRed"/>, <see cref="DefaultGreen"/>, and <see cref="DefaultBlue"/>.
        /// </summary>
        public static RgbColor DefaultColor { get; } = new RgbColor(DefaultRed, DefaultGreen, DefaultBlue);

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances that are not equal to <see cref="DefaultColor"/>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NotEqualRgbColors { get; } = new object[][]
        {
            new object[] { null, },
            new object[] { new RgbColor(0xDE, 0xAD, 0xBE), }, // none equal
            new object[] { new RgbColor(DefaultRed, 0xAD, 0xBE), }, // R equal
            new object[] { new RgbColor(0xDE, DefaultGreen, 0xBE), }, // G equal
            new object[] { new RgbColor(0xDE, 0xAD, DefaultBlue), }, // B equal
            new object[] { new RgbColor(DefaultRed, DefaultGreen, 0xBE), }, // RG equal
            new object[] { new RgbColor(DefaultRed, 0xAD, DefaultBlue), }, // RB equal
            new object[] { new RgbColor(0xDE, DefaultGreen, DefaultBlue), }, // BG equal
        };

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="NotEqualRgbColors"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NonNullNotEqualRgbColors => NotEqualRgbColors.Where(o => o.Single() != null);

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="NotEqualRgbColors"/> and <see cref="DefaultColor"/>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> RgbColorsWithDefault => NotEqualRgbColors.Concat(new[]
        {
            new object[] { DefaultColor, }, // equal
        });

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="RgbColorsWithDefault"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NonNullRgbColorsWithDefault => RgbColorsWithDefault.Where(o => o.Single() != null);

        /// <summary>
        /// Gets a collection of <see cref="object"/> instances including <see cref="NotEqualRgbColors"/> and an object of another type.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NotEqualObjects => NotEqualRgbColors.Concat(new[]
        {
            new[] { new object(), },
        });

        /// <summary>
        /// Gets collection of invalid hexadecimal strings.
        /// </summary>
        public static IEnumerable<IEnumerable<string>> InvalidHexadecimalColorStrings
        {
            get
            {
                yield return new[] { string.Empty, };
                yield return new[] { "#", };
                yield return new[] { "#0", };
                yield return new[] { "#00", };
                yield return new[] { "#0000", };
                yield return new[] { "#00000", };
                yield return new[] { "#0000000", };
                yield return new[] { "#ABG", };
                yield return new[] { "#ABCDEG", };
                yield return new[] { "0", };
                yield return new[] { "00", };
                yield return new[] { "0000", };
                yield return new[] { "00000", };
                yield return new[] { "0000000", };
                yield return new[] { "ABG", };
                yield return new[] { "ABCDEG", };
                yield return new[] { "#***", };
                yield return new[] { "#******", };
                yield return new[] { "#ABC 123", };
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="RgbColorTests.Parse_ValidFormat_ReturnsEquivalentRgbColor(string, RgbColor)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> ValidHexadecimalColorStrings
        {
            get
            {
                yield return new object[] { "#000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000000", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000 ", new RgbColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000000 ", new RgbColor(0x00, 0x00, 0x00), };
            }
        }
    }
}
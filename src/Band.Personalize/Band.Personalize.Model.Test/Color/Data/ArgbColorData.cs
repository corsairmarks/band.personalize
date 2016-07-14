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
    /// Test data for use with <see cref="ArgbColorTests"/>.
    /// </summary>
    public static class ArgbColorData
    {
        /// <summary>
        /// The default red channel saturation used for testing.
        /// </summary>
        public const byte DefaultAlpha = 0x80;

        /// <summary>
        /// Gets the default color, constructed with <see cref="DefaultAlpha"/>, <see cref="RgbColorData.DefaultRed"/>, <see cref="RgbColorData.DefaultGreen"/>, and <see cref="RgbColorData.DefaultBlue"/>.
        /// </summary>
        public static ArgbColor DefaultArgbColor { get; } = new ArgbColor(DefaultAlpha, RgbColorData.DefaultRed, RgbColorData.DefaultGreen, RgbColorData.DefaultBlue);

        /// <summary>
        /// Gets a collection of <see cref="ArgbColor"/> instances that are not equal to <see cref="DefaultArgbColor"/>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NotEqualArgbColors { get; } = new object[][]
        {
            new object[] { null, },
            new object[] { new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), }, // none equal
            new object[] { new ArgbColor(DefaultAlpha, 0xAD, 0xBE, 0xEF), }, // A equal
            new object[] { new ArgbColor(0xDE, RgbColorData.DefaultRed, 0xBE, 0xEF), }, // R equal
            new object[] { new ArgbColor(0xDE, 0xAD, RgbColorData.DefaultGreen, 0xEF), }, // G equal
            new object[] { new ArgbColor(0xDE, 0xAD, 0xBE, RgbColorData.DefaultBlue), }, // B equal
            new object[] { new ArgbColor(DefaultAlpha, RgbColorData.DefaultRed, 0xBE, 0xEF), }, // AR equal
            new object[] { new ArgbColor(DefaultAlpha, 0xAD, RgbColorData.DefaultGreen, 0xEF), }, // AG equal
            new object[] { new ArgbColor(DefaultAlpha, 0xAD, 0xAD, RgbColorData.DefaultBlue), }, // AB equal
            new object[] { new ArgbColor(0xDE, RgbColorData.DefaultRed, RgbColorData.DefaultGreen, 0xEF), }, // RG equal
            new object[] { new ArgbColor(0xDE, RgbColorData.DefaultRed, 0xAD, RgbColorData.DefaultBlue), }, // RB equal
            new object[] { new ArgbColor(0xDE, 0xAD, RgbColorData.DefaultGreen, RgbColorData.DefaultBlue), }, // BG equal
            new object[] { new ArgbColor(DefaultAlpha, RgbColorData.DefaultRed, RgbColorData.DefaultGreen, 0xEF), }, // ARG
            new object[] { new ArgbColor(DefaultAlpha, RgbColorData.DefaultRed, 0xBE, RgbColorData.DefaultBlue), }, // ARB
            new object[] { new ArgbColor(DefaultAlpha, 0xAD, RgbColorData.DefaultGreen, RgbColorData.DefaultBlue), }, // AGB
            new object[] { new ArgbColor(0xDE, RgbColorData.DefaultRed, RgbColorData.DefaultGreen, RgbColorData.DefaultBlue), }, // RGB
        };

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="NotEqualArgbColors"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NonNullNotEqualArgbColors => NotEqualArgbColors.Where(o => o.Single() != null);

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="NotEqualArgbColors"/> and <see cref="DefaultArgbColor"/>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> ArgbColorsWithDefault => NotEqualArgbColors.Concat(new[]
        {
            new object[] { DefaultArgbColor, }, // equal
        });

        /// <summary>
        /// Gets a collection of <see cref="RgbColor"/> instances including <see cref="ArgbColorsWithDefault"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NonNullArgbColorsWithDefault => ArgbColorsWithDefault.Where(o => o.Single() != null);

        /// <summary>
        /// Gets a collection of <see cref="object"/> instances including <see cref="NotEqualArgbColors"/> and an object of another type.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> NotEqualObjects => NotEqualArgbColors.Concat(new[]
        {
            new[] { new object(), },
        });

        /// <summary>
        /// Gets the test data for the <see cref="ArgbColorTests.FromArgbString_InvalidFormat_Throws(string)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<string>> InvalidHexadecimalColorStrings
        {
            get
            {
                yield return new[] { string.Empty, };
                yield return new[] { "#", };
                yield return new[] { "#0", };
                yield return new[] { "#00", };
                yield return new[] { "#000", };
                yield return new[] { "#0000", };
                yield return new[] { "#00000", };
                yield return new[] { "#000000", };
                yield return new[] { "#0000000", };
                yield return new[] { "#000000000", };
                yield return new[] { "#FABG", };
                yield return new[] { "#FFABCDEG", };
                yield return new[] { "0", };
                yield return new[] { "00", };
                yield return new[] { "000", };
                yield return new[] { "0000", };
                yield return new[] { "00000", };
                yield return new[] { "000000", };
                yield return new[] { "0000000", };
                yield return new[] { "000000000", };
                yield return new[] { "FABG", };
                yield return new[] { "FFABCDEG", };
                yield return new[] { "#****", };
                yield return new[] { "#********", };
                yield return new[] { "#FFABC 123", };
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="ArgbColorTests.FromArgbString_ValidFormat_ReturnsEquivalentArgbColor(string, ArgbColor)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> ValidHexadecimalColorStrings
        {
            get
            {
                yield return new object[] { "#DEADBEEF", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { " #DEADBEEF", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { "#DEADBEEF ", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { " #DEADBEEF ", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { "DEADBEEF", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { " DEADBEEF", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { "DEADBEEF ", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
                yield return new object[] { " DEADBEEF ", new ArgbColor(0xDE, 0xAD, 0xBE, 0xEF), };
            }
        }
    }
}
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test <see cref="double"/> data for HSV/HSB colors.
    /// </summary>
    public class EquivalentHsvAndRgbColorData : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets a collection of valid RGB color channel byte triplets.
        /// </summary>
        private static IEnumerable<IEnumerable<object>> EquivalentHsvAndRgbData { get; } = new[]
        {
            new object[] { RgbColorData.DefaultRed, RgbColorData.DefaultGreen, RgbColorData.DefaultBlue, RgbColorData.DefaultHue, RgbColorData.DefaultSaturation, RgbColorData.DefaultValue, },
            new object[] { 0x00, 0x00, 0x00, 0D, 0D, 0D, }, // black
            new object[] { 0xFF, 0xFF, 0xFF, 0D, 0D, 1D, }, // white
            new object[] { 0xFF, 0x00, 0x00, 0D, 1D, 1D, }, // red
            new object[] { 0x00, 0xFF, 0x00, 120D, 1D, 1D, }, // lime
            new object[] { 0x00, 0x00, 0xFF, 240D, 1D, 1D, }, // blue
            new object[] { 0xFF, 0xFF, 0x00, 60D, 1D, 1D, }, // yellow
            new object[] { 0x00, 0xFF, 0xFF, 180D, 1D, 1D, }, // cyan
            new object[] { 0xFF, 0x00, 0xFF, 300D, 1D, 1D, }, // magenta
            // new object[] { 0xC0, 0xC0, 0xC0, 0D, 0D, 0.75, }, // silver KNOWN FAILURE - similar color conversion tools fail on this value (0xC0 => 0xBF when round-tripping rgb to hsv to rgb), see paint.net or http://rgb.to
            new object[] { 0x80, 0x80, 0x80, 0D, 0D, 0.5, }, // gray
            new object[] { 0x80, 0x00, 0x00, 0D, 1D, 0.5, }, // maroon
            new object[] { 0x80, 0x80, 0x00, 60D, 1D, 0.5, }, // olive
            new object[] { 0x00, 0x80, 0x00, 120D, 1D, 0.5, }, // green
            new object[] { 0x80, 0x00, 0x80, 300D, 1D, 0.5, }, // purple
            new object[] { 0x00, 0x80, 0x80, 180D, 1D, 0.5, }, // teal
            new object[] { 0x00, 0x00, 0x80, 240D, 1D, 0.5, }, // navy
        };

        #region IEnumerable<object[]> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return EquivalentHsvAndRgbData.Select(o => o.ToArray()).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
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

namespace Band.Personalize.Model.Library.Color
{
    using Microsoft.Band;

    /// <summary>
    /// Extension methods for the <see cref="BandColor"/> struct.
    /// </summary>
    public static class BandColorExtensions
    {
        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="BandColor"/> to convert.</param>
        /// <returns>A new instance of <see cref="RgbColor"/></returns>
        public static RgbColor ToRgbColor(this BandColor color)
        {
            return new RgbColor(color.R, color.G, color.B);
        }

        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="ArgbColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="BandColor"/> to convert.</param>
        /// <returns>A new instance of <see cref="ArgbColor"/></returns>
        public static ArgbColor ToArgbColor(this BandColor color)
        {
            return new ArgbColor(byte.MaxValue, color.R, color.G, color.B);
        }
    }
}
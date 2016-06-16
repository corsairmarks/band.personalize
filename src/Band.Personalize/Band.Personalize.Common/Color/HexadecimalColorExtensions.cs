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

namespace Band.Personalize.Common.Color
{
    using System;
    using Microsoft.Band;

    /// <summary>
    /// Extension methods for the <see cref="HexadecimalColor"/> class.
    /// </summary>
    public static class HexadecimalColorExtensions
    {
        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="BandColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to convert.</param>
        /// <returns>A new instance of <see cref="BandColor"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="color"/> is <c>null</c>.</exception>
        public static BandColor ToBandColor(this HexadecimalColor color)
        {
            if (color == null)
            {
                throw new ArgumentNullException(nameof(color));
            }

            return new BandColor(color.Red, color.Green, color.Blue);
        }
    }
}
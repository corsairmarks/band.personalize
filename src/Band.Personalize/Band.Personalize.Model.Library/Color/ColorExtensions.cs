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
    using Windows.UI;

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
    /// <summary>
    /// Extension methods for the <see cref="Windows.UI.Color"/> struct.
    /// </summary>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
    public static class ColorExtensions
    {
#pragma warning disable CS0419 // Ambiguous reference in cref attribute
        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Windows.UI.Color"/> to convert.</param>
        /// <returns>A new instance of <see cref="RgbColor"/></returns>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
        public static RgbColor ToRgbColor(this Color color)
        {
            return new RgbColor(color.R, color.G, color.B);
        }

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="ArgbColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="Windows.UI.Color"/> to convert.</param>
        /// <returns>A new instance of <see cref="ArgbColor"/></returns>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
        public static ArgbColor ToArgbColor(this Color color)
        {
            return new ArgbColor(color.A, color.R, color.G, color.B);
        }
    }
}
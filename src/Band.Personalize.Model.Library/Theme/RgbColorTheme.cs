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

namespace Band.Personalize.Model.Library.Theme
{
    using Color;

    /// <summary>
    /// A six-color theme for use on the Microsoft Band.
    /// </summary>
    public class RgbColorTheme
    {
        /// <summary>
        /// Gets or sets the base Start Strip color, used as the default for tiles.
        /// </summary>
        public RgbColor Base { get; set; }

        /// <summary>
        /// Gets or sets the high contrast Start Strip color, used for highlighted tiles (i.e., new content alerts).
        /// </summary>
        public RgbColor HighContrast { get; set; }

        /// <summary>
        /// Gets or sets the lowlight Start Strip color, used for "pressed" tiles.
        /// </summary>
        public RgbColor Lowlight { get; set; }

        /// <summary>
        /// Gets or sets the in-tile header color.
        /// </summary>
        public RgbColor Highlight { get; set; }

        /// <summary>
        /// Gets or sets the in-tile muted color, used for the achievement marker background.
        /// </summary>
        public RgbColor Muted { get; set; }

        /// <summary>
        /// Gets or sets the system-wide secondary text color.
        /// </summary>
        public RgbColor SecondaryText { get; set; }
    }
}
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
        /// Gets or sets the in-tile/in-app header color.
        /// </summary>
        public RgbColor Highlight { get; set; }

        /// <summary>
        /// Gets or sets the in-tile/in-app muted color, used for the achievement marker background.
        /// </summary>
        public RgbColor Muted { get; set; }

        /// <summary>
        /// Gets or sets a color that is either the in-tile/in-app secondary header text color (original Band) or the
        /// toggle button "On" state (Band 2).  The Band 2 refers to this color as "Medium" instead of "Secondary Text,"
        /// which itself is a static, predefined color.
        /// </summary>
        public RgbColor SecondaryText { get; set; }
    }
}
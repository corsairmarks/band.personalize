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
    /// Default themes for the Band and Band 2.
    /// </summary>
    public static class DefaultThemes
    {
        /// <summary>
        /// Default themes for the Microsoft Band, taken from: <c>http://developer.microsoftband.com/content/docs/microsoftbandexperiencedesignguidelines.pdf</c>.
        /// </summary>
        public static class Band
        {
            /// <summary>
            /// Gets the Essentials blue theme.
            /// </summary>
            public static RgbColorTheme Blue
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x3A, 0x78, 0xDD),
                        Base = new RgbColor(0x33, 0x66, 0xCC),
                        Lowlight = new RgbColor(0x31, 0x65, 0xBA),
                        SecondaryText = new RgbColor(0x89, 0x97, 0xAB),
                        Highlight = new RgbColor(0x3A, 0x78, 0xDD),
                        Muted = new RgbColor(0x2B, 0x5A, 0xA5),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet blue theme.
            /// </summary>
            public static RgbColorTheme DiscreetBlue
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x30, 0x30, 0x30),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        SecondaryText = new RgbColor(0x79, 0x7E, 0x7F),
                        Highlight = new RgbColor(0x3B, 0xDA, 0xFF),
                        Muted = new RgbColor(0x00, 0x86, 0xA5),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet grey theme.
            /// </summary>
            public static RgbColorTheme DiscreetGrey
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x30, 0x30, 0x30),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        SecondaryText = new RgbColor(0x79, 0x7E, 0x7F),
                        Highlight = new RgbColor(0xB7, 0xB7, 0xB7),
                        Muted = new RgbColor(0x45, 0x45, 0x45),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet yellow theme.
            /// </summary>
            public static RgbColorTheme DiscreetYellow
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x30, 0x30, 0x30),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        SecondaryText = new RgbColor(0x79, 0x7E, 0x7F),
                        Highlight = new RgbColor(0xFF, 0xAF, 0x00),
                        Muted = new RgbColor(0xBC, 0x8B, 0x00),
                    };
                }
            }
        }

        /// <summary>
        /// Default themes for the Microsoft Band 2, taken from: <c>https://developer.microsoftband.com/Content/docs/MicrosoftBandExperienceDesignGuidelines2.pdf</c>.
        /// </summary>
        public static class Band2
        {
            /// <summary>
            /// Gets the Essentials "Electric" theme.
            /// </summary>
            public static RgbColorTheme Electric
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x0D, 0xD1, 0xFF),
                        Base = new RgbColor(0x00, 0xB9, 0xF2),
                        Lowlight = new RgbColor(0x00, 0x9D, 0xCE),
                        SecondaryText = new RgbColor(0x96, 0x96, 0x96),
                        Highlight = new RgbColor(0x5A, 0xE0, 0xFF),
                        Muted = new RgbColor(0x00, 0x4A, 0x64),
                    };
                }
            }
        }
    }
}
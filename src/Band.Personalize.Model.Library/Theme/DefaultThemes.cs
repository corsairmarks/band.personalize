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
            /// Gets the Essentials purple theme.
            /// </summary>
            public static RgbColorTheme Purple
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x88, 0x5A, 0xF9),
                        Base = new RgbColor(0x78, 0x42, 0xCF),
                        Lowlight = new RgbColor(0x69, 0x3F, 0xBC),
                        SecondaryText = new RgbColor(0x97, 0x94, 0xAB),
                        Highlight = new RgbColor(0x8B, 0x61, 0xF2),
                        Muted = new RgbColor(0x5E, 0x38, 0xA8),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials pink theme.
            /// </summary>
            public static RgbColorTheme Pink
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0xBF, 0x45, 0x5F),
                        Base = new RgbColor(0xD9, 0x4C, 0x66),
                        Lowlight = new RgbColor(0xC6, 0x47, 0x63),
                        SecondaryText = new RgbColor(0xA3, 0x91, 0x9C),
                        Highlight = new RgbColor(0x41, 0xCE, 0x7A),
                        Muted = new RgbColor(0x99, 0x33, 0x44),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials green theme.
            /// </summary>
            public static RgbColorTheme Green
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x33, 0xA3, 0x61),
                        Base = new RgbColor(0x39, 0xBF, 0x6F),
                        Lowlight = new RgbColor(0x35, 0xAA, 0x65),
                        SecondaryText = new RgbColor(0x93, 0x99, 0x82),
                        Highlight = new RgbColor(0x41, 0xCE, 0x7A),
                        Muted = new RgbColor(0x2C, 0x84, 0x54),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials yellow theme.
            /// </summary>
            public static RgbColorTheme Yellow
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0xFF, 0xA5, 0x00),
                        Base = new RgbColor(0xFF, 0xAF, 0x00),
                        Lowlight = new RgbColor(0xF9, 0x9A, 0x03),
                        SecondaryText = new RgbColor(0x9E, 0x96, 0x78),
                        Highlight = new RgbColor(0xFF, 0xAF, 0x00),
                        Muted = new RgbColor(0xBC, 0x8B, 0x00),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials light purple theme.
            /// </summary>
            public static RgbColorTheme LightPurple
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0xB7, 0xA5, 0xD3),
                        Base = new RgbColor(0x97, 0x87, 0xAF),
                        Lowlight = new RgbColor(0x7E, 0x76, 0x8E),
                        SecondaryText = new RgbColor(0x95, 0x95, 0x9E),
                        Highlight = new RgbColor(0xAC, 0x9F, 0xC1),
                        Muted = new RgbColor(0x68, 0x61, 0x72),
                    };
                }
            }

            /// <summary>
            /// Gets the Active blue theme.
            /// </summary>
            public static RgbColorTheme ActiveBlue
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x0D, 0xD1, 0xFF),
                        Base = new RgbColor(0x00, 0xB9, 0xF2),
                        Lowlight = new RgbColor(0x00, 0xB2, 0xDB),
                        SecondaryText = new RgbColor(0x86, 0x9A, 0x9C),
                        Highlight = new RgbColor(0x5A, 0xE0, 0xFF),
                        Muted = new RgbColor(0x00, 0x86, 0xA5),
                    };
                }
            }

            /// <summary>
            /// Gets the Active orange theme.
            /// </summary>
            public static RgbColorTheme ActiveOrange
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0xFF, 0x6F, 0x48),
                        Base = new RgbColor(0xF0, 0x53, 0x0E),
                        Lowlight = new RgbColor(0xDD, 0x44, 0x0E),
                        SecondaryText = new RgbColor(0xA3, 0x91, 0x9C),
                        Highlight = new RgbColor(0xFC, 0x66, 0x3D),
                        Muted = new RgbColor(0xC9, 0x3D, 0x0D),
                    };
                }
            }

            /// <summary>
            /// Gets the Active fuschia theme.
            /// </summary>
            public static RgbColorTheme ActiveFuschia
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0xF0, 0x4B, 0xF9),
                        Base = new RgbColor(0xD9, 0x36, 0xD9),
                        Lowlight = new RgbColor(0xC2, 0x34, 0xC6),
                        SecondaryText = new RgbColor(0xA3, 0x91, 0x9C),
                        Highlight = new RgbColor(0xF4, 0x2E, 0xFF),
                        Muted = new RgbColor(0xAF, 0x2F, 0xB2),
                    };
                }
            }

            /// <summary>
            /// Gets the Active lime theme.
            /// </summary>
            public static RgbColorTheme ActiveLime
            {
                get
                {
                    return new RgbColorTheme
                    {
                        HighContrast = new RgbColor(0x97, 0xDB, 0x40),
                        Base = new RgbColor(0x99, 0xC8, 0x14),
                        Lowlight = new RgbColor(0x79, 0xA8, 0x2F),
                        SecondaryText = new RgbColor(0x93, 0x99, 0x82),
                        Highlight = new RgbColor(0xB1, 0xDB, 0x16),
                        Muted = new RgbColor(0x61, 0x8E, 0x13),
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
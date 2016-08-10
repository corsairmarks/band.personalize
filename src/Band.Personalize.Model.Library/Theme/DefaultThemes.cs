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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
            /// Gets a read-only collection of the default themes for the Microsoft Band.
            /// </summary>
            public static IReadOnlyList<TitledRgbColorTheme> DefaultThemes
            {
                get
                {
                    return new ReadOnlyCollection<TitledRgbColorTheme>(new[]
                    {
                        Blue,
                        Purple,
                        Pink,
                        Green,
                        Yellow,
                        LightPurple,
                        ActiveBlue,
                        ActiveOrange,
                        ActiveFuschia,
                        ActiveLime,
                        DiscreetBlue,
                        DiscreetGrey,
                        DiscreetYellow,
                    });
                }
            }

            /// <summary>
            /// Gets the Essentials blue theme.
            /// </summary>
            public static TitledRgbColorTheme Blue
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Blue",
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
            public static TitledRgbColorTheme Purple
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Purple",
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
            public static TitledRgbColorTheme Pink
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Pink",
                        HighContrast = new RgbColor(0xBF, 0x45, 0x5F),
                        Base = new RgbColor(0xD9, 0x4C, 0x66),
                        Lowlight = new RgbColor(0xC6, 0x47, 0x63),
                        SecondaryText = new RgbColor(0xA3, 0x91, 0x9C),
                        Highlight = new RgbColor(0xEA, 0x54, 0x75),
                        Muted = new RgbColor(0x99, 0x33, 0x44),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials green theme.
            /// </summary>
            public static TitledRgbColorTheme Green
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Green",
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
            public static TitledRgbColorTheme Yellow
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Yellow",
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
            public static TitledRgbColorTheme LightPurple
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Light Purple",
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
            public static TitledRgbColorTheme ActiveBlue
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Active: Blue",
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
            public static TitledRgbColorTheme ActiveOrange
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Active: Orange",
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
            public static TitledRgbColorTheme ActiveFuschia
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Active: Fuschia",
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
            public static TitledRgbColorTheme ActiveLime
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Active: Lime",
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
            public static TitledRgbColorTheme DiscreetBlue
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Blue",
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
            public static TitledRgbColorTheme DiscreetGrey
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Grey",
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
            public static TitledRgbColorTheme DiscreetYellow
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Yellow",
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
            /// Gets a read-only collection of the default themes for the Microsoft Band 2.
            /// </summary>
            public static IReadOnlyList<TitledRgbColorTheme> DefaultThemes
            {
                get
                {
                    return new ReadOnlyCollection<TitledRgbColorTheme>(new[]
                    {
                        Electric,
                        Skyline,
                        Kale,
                        Cyber,
                        Lime,
                        Tangerine,
                        Tang,
                        Coral,
                        KoolAid,
                        Berry,
                        Cargo,
                        Tuxedo,
                        Storm,
                        Dj,
                        California,
                        KillaBee,
                        Pizza,
                        Lasertag,
                    });
                }
            }

            /// <summary>
            /// Gets the Essentials "Electric" theme.
            /// </summary>
            public static TitledRgbColorTheme Electric
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Electric",
                        HighContrast = new RgbColor(0x0D, 0xD1, 0xFF),
                        Base = new RgbColor(0x00, 0xB9, 0xF2),
                        Lowlight = new RgbColor(0x00, 0x9D, 0xCE),
                        Highlight = new RgbColor(0x5A, 0xE0, 0xFF),
                        SecondaryText = new RgbColor(0x00, 0x9D, 0xCE),
                        Muted = new RgbColor(0x00, 0x4A, 0x64),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Skyline" theme.
            /// </summary>
            public static TitledRgbColorTheme Skyline
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Skyline",
                        HighContrast = new RgbColor(0x1A, 0x68, 0x84),
                        Base = new RgbColor(0x00, 0x57, 0x76),
                        Lowlight = new RgbColor(0x00, 0x4A, 0x64),
                        Highlight = new RgbColor(0x09, 0xB9, 0xD6),
                        SecondaryText = new RgbColor(0x04, 0x98, 0xB4),
                        Muted = new RgbColor(0x00, 0x57, 0x76),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Kale" theme.
            /// </summary>
            public static TitledRgbColorTheme Kale
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Kale",
                        HighContrast = new RgbColor(0x1D, 0x89, 0x89),
                        Base = new RgbColor(0x03, 0x7C, 0x7C),
                        Lowlight = new RgbColor(0x05, 0x70, 0x70),
                        Highlight = new RgbColor(0x48, 0xFF, 0xDC),
                        SecondaryText = new RgbColor(0x0D, 0xBA, 0xB1),
                        Muted = new RgbColor(0x03, 0x68, 0x61),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Cyber" theme.
            /// </summary>
            public static TitledRgbColorTheme Cyber
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Cyber",
                        HighContrast = new RgbColor(0x37, 0xE2, 0x7C),
                        Base = new RgbColor(0x39, 0xBF, 0x6F),
                        Lowlight = new RgbColor(0x31, 0xA3, 0x5E),
                        Highlight = new RgbColor(0x1C, 0xF7, 0x7F),
                        SecondaryText = new RgbColor(0x19, 0xCE, 0x78),
                        Muted = new RgbColor(0x18, 0x66, 0x37),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Lime" theme.
            /// </summary>
            public static TitledRgbColorTheme Lime
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Lime",
                        HighContrast = new RgbColor(0xBF, 0xE5, 0x1F),
                        Base = new RgbColor(0xA3, 0xCE, 0x19),
                        Lowlight = new RgbColor(0x99, 0xB7, 0x1C),
                        Highlight = new RgbColor(0xB8, 0xFF, 0x1D),
                        SecondaryText = new RgbColor(0x8A, 0xAC, 0x0D),
                        Muted = new RgbColor(0x54, 0x68, 0x0A),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Tangerine" theme.
            /// </summary>
            public static TitledRgbColorTheme Tangerine
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Tangerine",
                        HighContrast = new RgbColor(0xF8, 0xA5, 0x2E),
                        Base = new RgbColor(0xF7, 0x9B, 0x16),
                        Lowlight = new RgbColor(0xED, 0x8E, 0x1D),
                        Highlight = new RgbColor(0xFF, 0xBC, 0x00),
                        SecondaryText = new RgbColor(0xEA, 0x98, 0x23),
                        Muted = new RgbColor(0xAD, 0x69, 0x1F),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Tang" theme.
            /// </summary>
            public static TitledRgbColorTheme Tang
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Tang",
                        HighContrast = new RgbColor(0xED, 0x76, 0x3B),
                        Base = new RgbColor(0xF1, 0x64, 0x22),
                        Lowlight = new RgbColor(0xD6, 0x54, 0x21),
                        Highlight = new RgbColor(0xFF, 0x6C, 0x45),
                        SecondaryText = new RgbColor(0xCC, 0x4B, 0x19),
                        Muted = new RgbColor(0x87, 0x33, 0x12),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Coral" theme.
            /// </summary>
            public static TitledRgbColorTheme Coral
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Coral",
                        HighContrast = new RgbColor(0xFF, 0x52, 0x67),
                        Base = new RgbColor(0xE7, 0x48, 0x56),
                        Lowlight = new RgbColor(0xC5, 0x3D, 0x49),
                        Highlight = new RgbColor(0xFF, 0x52, 0x52),
                        SecondaryText = new RgbColor(0xE7, 0x48, 0x56),
                        Muted = new RgbColor(0x99, 0x33, 0x44),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Kool-Aid" theme.
            /// </summary>
            public static TitledRgbColorTheme KoolAid
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Kool-Aid",
                        HighContrast = new RgbColor(0xD8, 0x31, 0x90),
                        Base = new RgbColor(0xD1, 0x0D, 0x7D),
                        Lowlight = new RgbColor(0xB2, 0x0B, 0x64),
                        Highlight = new RgbColor(0xFF, 0x43, 0xB6),
                        SecondaryText = new RgbColor(0xD6, 0x1D, 0x91),
                        Muted = new RgbColor(0x89, 0x10, 0x54),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Berry" theme.
            /// </summary>
            public static TitledRgbColorTheme Berry
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Berry",
                        HighContrast = new RgbColor(0x8B, 0x25, 0x93),
                        Base = new RgbColor(0x77, 0x1E, 0x7C),
                        Lowlight = new RgbColor(0x5B, 0x18, 0x60),
                        Highlight = new RgbColor(0xE8, 0x45, 0xFF),
                        SecondaryText = new RgbColor(0xAE, 0x3C, 0xC6),
                        Muted = new RgbColor(0x43, 0x24, 0x74),
                    };
                }
            }

            /// <summary>
            /// Gets the Essentials "Cargo" theme.
            /// </summary>
            public static TitledRgbColorTheme Cargo
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Essentials: Cargo",
                        HighContrast = new RgbColor(0x8A, 0x5F, 0xCE),
                        Base = new RgbColor(0x78, 0x42, 0xCF),
                        Lowlight = new RgbColor(0x66, 0x38, 0xB0),
                        Highlight = new RgbColor(0xA8, 0x69, 0xF9),
                        SecondaryText = new RgbColor(0x90, 0x4B, 0xF2),
                        Muted = new RgbColor(0x43, 0x24, 0x74),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "Tuxedo" theme.
            /// </summary>
            public static TitledRgbColorTheme Tuxedo
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Tuxedo",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0xF2, 0xF2, 0xF2),
                        SecondaryText = new RgbColor(0x7C, 0x7C, 0x7C),
                        Muted = new RgbColor(0x33, 0x33, 0x33),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "Storm" theme.
            /// </summary>
            public static TitledRgbColorTheme Storm
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Storm",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0x5A, 0xE0, 0xFF),
                        SecondaryText = new RgbColor(0x00, 0x9D, 0xCE),
                        Muted = new RgbColor(0x00, 0x4A, 0x64),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "DJ" theme.
            /// </summary>
            public static TitledRgbColorTheme Dj
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Dj",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0x1C, 0xF7, 0x7F),
                        SecondaryText = new RgbColor(0x19, 0xCE, 0x78),
                        Muted = new RgbColor(0x18, 0x66, 0x37),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "California" theme.
            /// </summary>
            public static TitledRgbColorTheme California
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: California",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0xB8, 0xFF, 0x1D),
                        SecondaryText = new RgbColor(0x8A, 0xAC, 0x0D),
                        Muted = new RgbColor(0x54, 0x68, 0x0A),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "Killa Bee" theme.
            /// </summary>
            public static TitledRgbColorTheme KillaBee
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Killa Bee",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0xFF, 0xBC, 0x00),
                        SecondaryText = new RgbColor(0xEA, 0x98, 0x23),
                        Muted = new RgbColor(0xAD, 0x69, 0x1F),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "Pizza" theme.
            /// </summary>
            public static TitledRgbColorTheme Pizza
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Pizza",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0xFF, 0x52, 0x52),
                        SecondaryText = new RgbColor(0xE7, 0x48, 0x56),
                        Muted = new RgbColor(0x99, 0x33, 0x44),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet "Lasertag" theme.
            /// </summary>
            public static TitledRgbColorTheme Lasertag
            {
                get
                {
                    return new TitledRgbColorTheme
                    {
                        Title = "Discreet: Lasertag",
                        HighContrast = new RgbColor(0x25, 0x25, 0x25),
                        Base = new RgbColor(0x15, 0x15, 0x15),
                        Lowlight = new RgbColor(0x11, 0x11, 0x11),
                        Highlight = new RgbColor(0xFF, 0x43, 0xB6),
                        SecondaryText = new RgbColor(0xD6, 0x1D, 0x91),
                        Muted = new RgbColor(0x89, 0x10, 0x54),
                    };
                }
            }
        }
    }
}
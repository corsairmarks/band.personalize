namespace Band.Personalize.Common.Theme
{
    using Microsoft.Band;

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
            public static BandTheme Blue
            {
                get
                {
                    return new BandTheme
                    {
                        HighContrast = new BandColor(0x3A, 0x78, 0xDD),
                        Base = new BandColor(0x33, 0x66, 0xCC),
                        Lowlight = new BandColor(0x31, 0x65, 0xBA),
                        SecondaryText = new BandColor(0x89, 0x97, 0xAB),
                        Highlight = new BandColor(0x3A, 0x78, 0xDD),
                        Muted = new BandColor(0x2B, 0x5A, 0xA5),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet blue theme.
            /// </summary>
            public static BandTheme DiscreetBlue
            {
                get
                {
                    return new BandTheme
                    {
                        HighContrast = new BandColor(0x30, 0x30, 0x30),
                        Base = new BandColor(0x15, 0x15, 0x15),
                        Lowlight = new BandColor(0x11, 0x11, 0x11),
                        SecondaryText = new BandColor(0x79, 0x7E, 0x7F),
                        Highlight = new BandColor(0x3B, 0xDA, 0xFF),
                        Muted = new BandColor(0x00, 0x86, 0xA5),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet grey theme.
            /// </summary>
            public static BandTheme DiscreetGrey
            {
                get
                {
                    return new BandTheme
                    {
                        HighContrast = new BandColor(0x30, 0x30, 0x30),
                        Base = new BandColor(0x15, 0x15, 0x15),
                        Lowlight = new BandColor(0x11, 0x11, 0x11),
                        SecondaryText = new BandColor(0x79, 0x7E, 0x7F),
                        Highlight = new BandColor(0xB7, 0xB7, 0xB7),
                        Muted = new BandColor(0x45, 0x45, 0x45),
                    };
                }
            }

            /// <summary>
            /// Gets the Discreet yellow theme.
            /// </summary>
            public static BandTheme DiscreetYellow
            {
                get
                {
                    return new BandTheme
                    {
                        HighContrast = new BandColor(0x30, 0x30, 0x30),
                        Base = new BandColor(0x15, 0x15, 0x15),
                        Lowlight = new BandColor(0x11, 0x11, 0x11),
                        SecondaryText = new BandColor(0x79, 0x7E, 0x7F),
                        Highlight = new BandColor(0xFF, 0xAF, 0x00),
                        Muted = new BandColor(0xBC, 0x8B, 0x00),
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
            public static BandTheme Electric
            {
                get
                {
                    return new BandTheme
                    {
                        HighContrast = new BandColor(0x0D, 0xD1, 0xFF),
                        Base = new BandColor(0x00, 0xB9, 0xF2),
                        Lowlight = new BandColor(0x00, 0x9D, 0xCE),
                        SecondaryText = new BandColor(0x96, 0x96, 0x96),
                        Highlight = new BandColor(0x5A, 0xE0, 0xFF),
                        Muted = new BandColor(0x00, 0x4A, 0x64),
                    };
                }
            }
        }
    }
}
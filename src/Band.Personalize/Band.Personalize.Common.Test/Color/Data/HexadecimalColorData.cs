namespace Band.Personalize.Common.Test.Color.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Color;

    /// <summary>
    /// Test data for use with <see cref="HexadecimalColorTests"/>.
    /// </summary>
    public static class HexadecimalColorData
    {
        /// <summary>
        /// The default red channel saturation used for testing.
        /// </summary>
        public const byte DefaultRed = 0x6A;

        /// <summary>
        /// The default green channel saturation used for testing.
        /// </summary>
        public const byte DefaultGreen = 0x00;

        /// <summary>
        /// The default blue channel saturation used for testing.
        /// </summary>
        public const byte DefaultBlue = 0xFF;

        /// <summary>
        /// Gets the default color, constructed with <see cref="DefaultRed"/>, <see cref="DefaultGreen"/>, and <see cref="DefaultBlue"/>.
        /// </summary>
        public static HexadecimalColor DefaultColor { get; } = new HexadecimalColor(DefaultRed, DefaultGreen, DefaultBlue);

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances that are not equal to <see cref="DefaultColor"/>.
        /// </summary>
        public static IEnumerable<HexadecimalColor> NotEqualHexadecimalColors
        {
            get
            {
                yield return null;
                yield return new HexadecimalColor(0xDE, 0xAD, 0xBE); // none equal
                yield return new HexadecimalColor(DefaultRed, 0xAD, 0xBE); // R equal
                yield return new HexadecimalColor(0xDE, DefaultGreen, 0xBE); // G equal
                yield return new HexadecimalColor(0xDE, 0xAD, DefaultBlue); // B equal
                yield return new HexadecimalColor(DefaultRed, DefaultGreen, 0xBE); // RG equal
                yield return new HexadecimalColor(DefaultRed, 0xAD, DefaultBlue); // RB equal
                yield return new HexadecimalColor(0xDE, DefaultGreen, DefaultBlue); // BG equal
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances including <see cref="NotEqualHexadecimalColors"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<HexadecimalColor> NonNullNotEqualHexadecimalColors => NotEqualHexadecimalColors.Where(hc => hc != null);

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances including <see cref="NotEqualHexadecimalColors"/> and <see cref="DefaultColor"/>.
        /// </summary>
        public static IEnumerable<HexadecimalColor> HexadecimalColorsWithDefault => NotEqualHexadecimalColors.Concat(new[]
        {
            DefaultColor, // equal
        });

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances including <see cref="HexadecimalColorsWithDefault"/> and excluding <c>null</c>.
        /// </summary>
        public static IEnumerable<HexadecimalColor> NonNullHexadecimalColorsWithDefault => HexadecimalColorsWithDefault.Where(hc => hc != null);

        /// <summary>
        /// Gets a collection of <see cref="object"/> instances including <see cref="NotEqualHexadecimalColors"/> and an object of another type.
        /// </summary>
        public static IEnumerable<object> NotEqualObjects => NotEqualHexadecimalColors.Concat(new[]
        {
            new object(),
        });

        /// <summary>
        /// Gets collection of invalid hexadecimal strings.
        /// </summary>
        public static IEnumerable<string> InvalidHexadecimalColorStrings
        {
            get
            {
                yield return string.Empty;
                yield return "#";
                yield return "#0";
                yield return "#00";
                yield return "#0000";
                yield return "#00000";
                yield return "#0000000";
                yield return "#ABG";
                yield return "#ABCDEG";
                yield return "0";
                yield return "00";
                yield return "0000";
                yield return "00000";
                yield return "0000000";
                yield return "ABG";
                yield return "ABCDEG";
                yield return "#***";
                yield return "#******";
                yield return "#ABC 123";
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="HexadecimalColorTests.Parse_ValidFormat_ReturnsEquivalentHexadecimalColor(string)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> ValidHexadecimalColorStrings
        {
            get
            {
                yield return new object[] { "#000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "#000000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " #000000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000000", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { "000000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
                yield return new object[] { " 000000 ", new HexadecimalColor(0x00, 0x00, 0x00), };
            }
        }
    }
}
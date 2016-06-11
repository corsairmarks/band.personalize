namespace Band.Personalize.Common.Color
{
    using Microsoft.Band;

    /// <summary>
    /// Extension methods for the <see cref="BandColor"/> struct.
    /// </summary>
    public static class BandColorExtensions
    {
        /// <summary>
        /// Convert the <paramref name="color"/> to a new instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="color">The <see cref="BandColor"/> to convert.</param>
        /// <returns>A new instance of <see cref="HexadecimalColor"/></returns>
        public static HexadecimalColor ToHexadecimalColor(this BandColor color)
        {
            return new HexadecimalColor(color.R, color.G, color.B);
        }
    }
}
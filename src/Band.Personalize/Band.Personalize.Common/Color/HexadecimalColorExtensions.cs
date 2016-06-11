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
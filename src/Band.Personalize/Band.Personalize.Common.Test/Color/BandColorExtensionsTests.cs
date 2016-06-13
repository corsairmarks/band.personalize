namespace Band.Personalize.Common.Test.Color
{
    using Common.Color;
    using Data;
    using Microsoft.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandColorExtensions"/> class.
    /// </summary>
    public class BandColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="BandColorExtensions.ToHexadecimalColor(BandColor)"/> method maps
        /// the correct fields from an instance of <see cref="BandColor"/> to an instance of
        /// <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToHexadecimalColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new BandColor(red, green, blue);

            // Act
            var result = target.ToHexadecimalColor();

            // Assert
            Assert.Equal(target.R, result.Red);
            Assert.Equal(target.G, result.Green);
            Assert.Equal(target.B, result.Blue);
        }
    }
}
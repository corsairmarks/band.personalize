namespace Band.Personalize.Common.Test.Color
{
    using System;
    using Common.Color;
    using Data;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="HexadecimalColorExtensions"/> class.
    /// </summary>
    public class HexadecimalColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="HexadecimalColorExtensions.ToBandColor(HexadecimalColor)"/> method maps
        /// the correct fields from an instance of <see cref="HexadecimalColor"/> to an instance of
        /// <see cref="BandColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToBandColor_CreatesInstanceWithSameValues(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target.ToBandColor();

            // Assert
            Assert.Equal(target.Red, result.R);
            Assert.Equal(target.Green, result.G);
            Assert.Equal(target.Blue, result.B);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColorExtensions.ToBandColor(HexadecimalColor)"/> method throws
        /// an <see cref="ArgumentNullException"/> when the color parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void ToBandColor_NullColor_Throws()
        {
            // Arrange
            HexadecimalColor target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => target.ToBandColor());

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("color", expected.ParamName);
        }
    }
}
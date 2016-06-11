namespace Band.Personalize.Common.Test.Color
{
    using System;
    using Common.Color;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="HexadecimalColorExtensions"/> class.
    /// </summary>
    [TestFixture(TestOf = typeof(HexadecimalColorExtensions))]
    public class HexadecimalColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="HexadecimalColorExtensions.ToBandColor(HexadecimalColor)"/> method maps
        /// the correct fields from an instance of <see cref="HexadecimalColor"/> to an instance of
        /// <see cref="BandColor"/>.
        /// </summary>
        [Test]
        public void ToBandColor_CreatesInstanceWithSameValues()
        {
            // Arrange
            var target = new HexadecimalColor(0x6A, 0x00, 0xFF);

            // Act
            var result = target.ToBandColor();

            // Assert
            Assert.That(result.R, Is.EqualTo(target.Red));
            Assert.That(result.G, Is.EqualTo(target.Green));
            Assert.That(result.B, Is.EqualTo(target.Blue));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColorExtensions.ToBandColor(HexadecimalColor)"/> method throws
        /// an <see cref="ArgumentNullException"/> when the color parameter is <c>null</c>.
        /// </summary>
        [Test]
        public void ToBandColor_NullColor_Throws()
        {
            // Arrange
            HexadecimalColor target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => target.ToBandColor());

            // Assert
            Assert.That(expected, Is.Not.Null);
            Assert.That(expected.ParamName, Is.EqualTo("color"));
        }
    }
}
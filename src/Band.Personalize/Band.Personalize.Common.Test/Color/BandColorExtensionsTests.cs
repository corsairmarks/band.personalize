namespace Band.Personalize.Common.Test.Color
{
    using Common.Color;
    using Microsoft.Band;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="BandColorExtensions"/> class.
    /// </summary>
    [TestFixture(TestOf = typeof(BandColorExtensions))]
    public class BandColorExtensionsTests
    {
        /// <summary>
        /// Verify the <see cref="BandColorExtensions.ToHexadecimalColor(BandColor)"/> method maps
        /// the correct fields from an instance of <see cref="BandColor"/> to an instance of
        /// <see cref="HexadecimalColor"/>.
        /// </summary>
        [Test]
        public void ToHexadecimalColor_CreatesInstanceWithSameValues()
        {
            // Arrange
            var target = new BandColor(0x6A, 0x00, 0xFF);

            // Act
            var result = target.ToHexadecimalColor();

            // Assert
            Assert.That(result.Red, Is.EqualTo(target.R));
            Assert.That(result.Green, Is.EqualTo(target.G));
            Assert.That(result.Blue, Is.EqualTo(target.B));
        }
    }
}
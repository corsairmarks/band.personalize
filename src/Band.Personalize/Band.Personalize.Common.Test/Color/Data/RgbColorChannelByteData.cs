namespace Band.Personalize.Common.Test.Color.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test data for use with <see cref="HexadecimalColorTests"/> and <see cref="HexadecimalColorExtensionsTests"/> and <see cref="BandColorExtensionsTests"/>.
    /// </summary>
    public class RgbColorChannelByteData : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets a collection of valid RGB hexadecimal color channel bytes.
        /// </summary>
        private static IEnumerable<byte> AllBytes { get; } = Enumerable.Range(byte.MinValue, byte.MaxValue + 1).Select(i => (byte)i);

        #region IEnumerable<object[]> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return AllBytes.Select(b => new object[] { b, }).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
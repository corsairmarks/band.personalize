namespace Band.Personalize.Common.Test.Color.Data
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Test byte data for hexadecimal colors.
    /// </summary>
    public class RgbColorByteData : IEnumerable<IEnumerable<byte>>
    {
        /// <summary>
        /// A collection of valid RGB hexadecimal color channel byte triplets.
        /// </summary>
        private static IEnumerable<IEnumerable<byte>> HexadecimalByteTriplets { get; } = new[]
        {
            new byte[] { HexadecimalColorData.DefaultRed, HexadecimalColorData.DefaultGreen, HexadecimalColorData.DefaultBlue, }
        };

        #region IEnumerable<IEnumerable<byte>> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<IEnumerable<byte>> GetEnumerator()
        {
            return HexadecimalByteTriplets.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)HexadecimalByteTriplets).GetEnumerator();
        }

        #endregion
    }
}
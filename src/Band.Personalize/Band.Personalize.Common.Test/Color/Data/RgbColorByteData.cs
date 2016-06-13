namespace Band.Personalize.Common.Test.Color.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test byte data for hexadecimal colors.
    /// </summary>
    public class RgbColorByteData : IEnumerable<object[]>
    {
        /// <summary>
        /// A collection of valid RGB hexadecimal color channel byte triplets.
        /// </summary>
        private static IEnumerable<IEnumerable<byte>> HexadecimalByteTriplets { get; } = new[]
        {
            new byte[] { HexadecimalColorData.DefaultRed, HexadecimalColorData.DefaultGreen, HexadecimalColorData.DefaultBlue, }
        };

        #region IEnumerable<object[]> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return HexadecimalByteTriplets.Select(o => o.Cast<object>().ToArray()).GetEnumerator();
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
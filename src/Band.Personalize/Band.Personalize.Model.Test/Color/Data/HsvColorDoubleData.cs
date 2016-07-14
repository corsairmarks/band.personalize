// Copyright 2016 Nicholas Butcher
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Band.Personalize.Model.Test.Color.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Test <see cref="double"/> data for HSV/HSB colors.
    /// </summary>
    public class HsvColorDoubleData : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets a collection of valid HSV/HSB double triplets.
        /// </summary>
        private static IEnumerable<IEnumerable<double>> HsvDoubleTriplets { get; } = new[]
        {
            new double[] { RgbColorData.DefaultHue, RgbColorData.DefaultSaturation, RgbColorData.DefaultValue, }
        };

        #region IEnumerable<object[]> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return HsvDoubleTriplets.Select(o => o.Cast<object>().ToArray()).GetEnumerator();
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
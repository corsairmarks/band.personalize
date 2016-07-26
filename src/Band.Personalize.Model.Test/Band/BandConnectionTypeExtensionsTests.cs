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

namespace Band.Personalize.Model.Test.Band
{
    using System.Collections.Generic;
    using Library.Band;
    using Microsoft.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandConnectionTypeExtensions"/> class.
    /// </summary>
    public class BandConnectionTypeExtensionsTests
    {
        /// <summary>
        /// The expected mappings of <see cref="BandConnectionType"/> to <see cref="ConnectionType"/>.
        /// </summary>
        public static readonly IEnumerable<object[]> BandConnectionTypeToConnectionTypeMap = new[]
        {
            new object[] { BandConnectionType.Bluetooth, ConnectionType.Bluetooth, },
            new object[] { BandConnectionType.Usb, ConnectionType.Usb, },
            new object[] { (BandConnectionType)13, ConnectionType.Unknown, },
        };

        /// <summary>
        /// Verify the <see cref="BandConnectionTypeExtensions.ToConnectionType(BandConnectionType)"/> method maps the
        /// correct <see cref="ConnectionType"/> to a <see cref="BandConnectionType"/>.
        /// </summary>
        /// <param name="bandConnectionType">The type to test mapping.</param>
        /// <param name="expectedConnectionType">The expected result.</param>
        [Theory]
        [MemberData(nameof(BandConnectionTypeToConnectionTypeMap))]
        public void ToConnectionType_ConvertsToCorrectValue(BandConnectionType bandConnectionType, ConnectionType expectedConnectionType)
        {
            // Act
            var result = bandConnectionType.ToConnectionType();

            // Assert
            Assert.Equal(expectedConnectionType, result);
        }
    }
}
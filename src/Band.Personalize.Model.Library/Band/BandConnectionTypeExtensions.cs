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

namespace Band.Personalize.Model.Library.Band
{
    using Microsoft.Band;

    /// <summary>
    /// Extension methods for the <see cref="BandConnectionType"/> enumeration.
    /// </summary>
    public static class BandConnectionTypeExtensions
    {
        /// <summary>
        /// Convert the <paramref name="bandConnectionType"/> to its equivalent representation in <see cref="ConnectionType"/>.
        /// </summary>
        /// <param name="bandConnectionType">The <see cref="BandConnectionType"/> to convert.</param>
        /// <returns>The <see cref="ConnectionType"/> value equivalent to <paramref name="bandConnectionType"/>, or <see cref="ConnectionType.Unknown"/> if there is no equivalent.</returns>
        public static ConnectionType ToConnectionType(this BandConnectionType bandConnectionType)
        {
            switch (bandConnectionType)
            {
                case BandConnectionType.Bluetooth:
                    return ConnectionType.Bluetooth;
                case BandConnectionType.Usb:
                    return ConnectionType.Usb;
                default:
                    return ConnectionType.Unknown;
            }
        }
    }
}
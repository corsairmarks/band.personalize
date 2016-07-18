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

namespace Band.Personalize.Model.Implementation.Repository
{
    using Library.Band;
    using Microsoft.Band;

    /// <summary>
    /// Information about a Microsoft Band.
    /// </summary>
    internal class Band : IBand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Band"/> class.
        /// </summary>
        /// <param name="bandInfo">Band information from the Band SDK.</param>
        /// <param name="hardwareVersion">The hardware verision, which requires a second connection.</param>
        public Band(IBandInfo bandInfo, int hardwareVersion)
        {
            this.BandInfo = bandInfo;
            this.HardwareVersion = hardwareVersion;
            this.HardwareRevision = hardwareVersion.ToHardwareRevision();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this.BandInfo.Name; }
        }

        /// <summary>
        /// Gets the hardware major revision level.
        /// </summary>
        public HardwareRevision HardwareRevision { get; }

        /// <summary>
        /// Gets the specific hardware version.
        /// </summary>
        public int HardwareVersion { get; }

        /// <summary>
        /// Gets the connection type between the application host and the Microsoft Band.
        /// </summary>
        public ConnectionType ConnectionType
        {
            get { return this.BandInfo.ConnectionType.ToConnectionType(); }
        }

        /// <summary>
        /// Gets the SDK <see cref="IBandInfo"/> for this Band.
        /// </summary>
        public IBandInfo BandInfo { get; }
    }
}
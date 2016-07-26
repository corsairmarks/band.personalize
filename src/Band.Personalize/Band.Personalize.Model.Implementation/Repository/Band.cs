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
    using System;
    using Library.Band;
    using Microsoft.Band;
    using Microsoft.Band.Store;

    /// <summary>
    /// Information about a Microsoft Band.
    /// </summary>
    internal class Band : IBand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Band"/> class.
        /// </summary>
        /// <param name="bandInfo">Band information from the Band SDK.</param>
        /// <param name="isConnected">Whether this Band is currently connected.</param>
        /// <param name="hardwareVersion">The hardware version, which is only available when the Band is connected.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bandInfo"/> is <c>null</c>.</exception>
        public Band(IBandInfo bandInfo, bool isConnected, int? hardwareVersion)
        {
            if (bandInfo == null)
            {
                throw new ArgumentNullException(nameof(bandInfo));
            }

            this.BandInfo = bandInfo;
            this.IsConnected = isConnected;
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
        /// Gets the connection type between the application host and the Microsoft Band.
        /// </summary>
        public ConnectionType ConnectionType
        {
            get { return this.BandInfo.ConnectionType.ToConnectionType(); }
        }

        /// <summary>
        /// Gets a value indicating whether this Band is connected.
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        /// Gets the hardware major revision level.
        /// </summary>
        public HardwareRevision HardwareRevision { get; }

        /// <summary>
        /// Gets the specific hardware version.
        /// </summary>
        public int? HardwareVersion { get; }

        /// <summary>
        /// Gets the SDK <see cref="IBandInfo"/> for this Band.
        /// </summary>
        /// <remarks>
        /// This is exposed because the <see cref="BandClientManager.ConnectAsync(IBandInfo)"/> method in
        /// <c>Microsoft.Band.Phone_UAP</c> breaks Liskov substitution and requires an instance of
        /// <see cref="BluetoothDeviceInfo"/> or it will throw an <see cref="ArgumentException"/>.
        /// </remarks>
        public IBandInfo BandInfo { get; }
    }
}
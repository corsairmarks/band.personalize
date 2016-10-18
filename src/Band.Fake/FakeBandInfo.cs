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

namespace Band.Fake
{
    using Microsoft.Band;
    using Microsoft.Band.Personalization;

    /// <summary>
    /// A fake implementation of <see cref="IBandInfo"/>.  Stores a variety of fields not accessible on <see cref="IBandInfo"/> to promote consistency when reusing instances for multiple connections.
    /// </summary>
    public class FakeBandInfo : IBandInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandInfo"/> class.
        /// </summary>
        public FakeBandInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandInfo"/> class.
        /// </summary>
        /// <param name="bandInfo">The source Band information to copy as a fake.</param>
        public FakeBandInfo(IBandInfo bandInfo)
        {
            if (bandInfo != null)
            {
                this.ConnectionType = bandInfo.ConnectionType;
                this.Name = bandInfo.Name;
            }
        }

        /// <summary>
        /// Gets or sets the connection type.
        /// </summary>
        public BandConnectionType ConnectionType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the firmware version (used by <see cref="FakeBandClient"/>).
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the hardware version (used by <see cref="FakeBandClient"/>).
        /// </summary>
        public string HardwareVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this Band is "connected;" default: <c>true</c>.
        /// </summary>
        public bool IsConnected { get; set; } = true;

        /// <summary>
        /// Gets or sets the Me Tile image associated with this Band (used by <see cref="FakePersonalizationManager"/>).
        /// </summary>
        public BandImage MeTile { get; set; }

        /// <summary>
        /// Gets or sets the theme associated with this Band (used by <see cref="FakePersonalizationManager"/>).
        /// </summary>
        public BandTheme Theme { get; set; }
    }
}
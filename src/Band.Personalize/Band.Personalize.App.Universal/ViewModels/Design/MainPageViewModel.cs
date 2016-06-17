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

namespace Band.Personalize.App.Universal.ViewModels.Design
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Model.Library.Band;

    /// <summary>
    /// The design View Model for the Main Page.
    /// </summary>
    public class MainPageViewModel
    {
        private static readonly IReadOnlyList<IBand> DefaultBands = new ReadOnlyCollection<IBand>(new List<IBand>
        {
            new BandStub
            {
                Name = "Sample Band",
                ConnectionType = ConnectionType.Bluetooth,
                HardwareRevision = HardwareRevision.Band,
                HardwareVersion = 3,
            },
            new BandStub
            {
                Name = "Sample Band 2",
                ConnectionType = ConnectionType.Usb,
                HardwareRevision = HardwareRevision.Band2,
                HardwareVersion = 20,
            },
            new BandStub
            {
                Name = "Sample Unknown Band",
                ConnectionType = ConnectionType.Unknown,
                HardwareRevision = HardwareRevision.Band2,
                HardwareVersion = 400,
            },
        });

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a read-only collection of connected Microsoft Bands.
        /// </summary>
        public IReadOnlyList<IBand> ConnectedBands
        {
            get { return DefaultBands; }
        }

        /// <summary>
        /// A stub for fake <see cref="IBand"/> data.
        /// </summary>
        private class BandStub : IBand
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the hardware major revision level.
            /// </summary>
            public ConnectionType ConnectionType { get; set; }

            /// <summary>
            /// Gets or sets the actual hardware version.
            /// </summary>
            public HardwareRevision HardwareRevision { get; set; }

            /// <summary>
            /// Gets or sets the connection type between the application host and the Microsoft Band.
            /// </summary>
            public int HardwareVersion { get; set; }
        }
    }
}
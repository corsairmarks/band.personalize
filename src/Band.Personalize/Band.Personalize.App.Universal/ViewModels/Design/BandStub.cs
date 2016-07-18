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
    using Microsoft.Band;
    using Model.Library.Band;

    /// <summary>
    /// A stub for fake <see cref="IBand"/> data.
    /// </summary>
    internal class BandStub : IBand
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
        public int? HardwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the Band info.
        /// </summary>
        public IBandInfo BandInfo { get; set; }
    }
}
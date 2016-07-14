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
    /// <summary>
    /// An enumeration of connection types between the application host and the Band.
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// An unknown connection type.
        /// </summary>
        Unknown,

        /// <summary>
        /// A hard-wired USB connection.
        /// </summary>
        Usb,

        /// <summary>
        /// A Bluetooth connection.
        /// </summary>
        Bluetooth,
    }
}
﻿// Copyright 2016 Nicholas Butcher
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
    using System;
    using Microsoft.Band;
    using Microsoft.Band.Store;

    /// <summary>
    /// Information about a Microsoft Band.
    /// </summary>
    public interface IBand
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the connection type between the application host and the Microsoft Band.
        /// </summary>
        ConnectionType ConnectionType { get; }

        /// <summary>
        /// Gets a value indicating whether this Band is connected.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets the hardware major revision level.
        /// </summary>
        HardwareRevision HardwareRevision { get; }

        /// <summary>
        /// Gets the specific hardware version.
        /// </summary>
        int? HardwareVersion { get; }

        /// <summary>
        /// Gets the SDK <see cref="IBandInfo"/> for this Band.
        /// </summary>
        /// <remarks>
        /// This is exposed because the <see cref="BandClientManager.ConnectAsync(IBandInfo)"/> method in
        /// <c>Microsoft.Band.Phone_UAP</c> breaks Liskov substitution and requires an instance of
        /// <see cref="BluetoothDeviceInfo"/> or it will throw an <see cref="ArgumentException"/>.
        /// </remarks>
        IBandInfo BandInfo { get; }
    }
}
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

namespace Band.Personalize.Model.Implementation.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Library.Band;
    using Library.Repository;
    using Microsoft.Band;

    /// <summary>
    /// A facade for retrieving information about Microsoft Bands.
    /// </summary>
    public class BandRepository : IBandRepository
    {
        /// <summary>
        /// The Band client manager.
        /// </summary>
        private readonly IBandClientManager bandClientManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandRepository"/> class.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        public BandRepository(IBandClientManager bandClientManager)
        {
            if (bandClientManager == null)
            {
                throw new ArgumentNullException(nameof(bandClientManager));
            }

            this.bandClientManager = bandClientManager;
        }

        /// <summary>
        /// Gets information about a specific Microsoft Band paired with the application host by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the Band to retrieve.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that when complete that returns, if successful, a paired Band; otherwise, <c>null</c>.</returns>
        public async Task<IBand> GetPairedBandAsync(string name, CancellationToken token)
        {
            var bands = await this.GetPairedBandsAsync(token);

            return bands.FirstOrDefault(band => string.Equals(name, band.Name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets information about all Microsoft Bands paired with the application host.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns a read-only collection of paired Bands when it completes.</returns>
        public async Task<IReadOnlyList<IBand>> GetPairedBandsAsync(CancellationToken token)
        {
            var bandInfos = await this.bandClientManager.GetBandsAsync();
            var bands = new List<IBand>(bandInfos.Count());
            foreach (var bandInfo in bandInfos)
            {
                int? hardwareVersion;
                var isConnected = false;
                try
                {
                    hardwareVersion = await this.bandClientManager.ConnectAndPerformFunctionAsync(bandInfo, token, async (bc, t) => await this.GetHardwareVersion(bc, t));
                    isConnected = true;
                }
                catch (BandIOException)
                {
                    hardwareVersion = null;
                }

                bands.Add(new Band(bandInfo, isConnected, hardwareVersion));
            }

            return new ReadOnlyCollection<IBand>(bands);
        }

        /// <summary>
        /// Get the hardware version using the <paramref name="bandClient"/>.
        /// </summary>
        /// <param name="bandClient">The Band client</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the hardware version of the Band when it completes.</returns>
        protected async Task<int?> GetHardwareVersion(IBandClient bandClient, CancellationToken token)
        {
            var hardwareVersionString = await bandClient.GetHardwareVersionAsync(token);
            int hardwareVersion;
            return int.TryParse(hardwareVersionString, out hardwareVersion)
                ? hardwareVersion
                : null as int?;
        }
    }
}
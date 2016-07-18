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
    public class BandRepository : BaseBandConnectionRepository, IBandRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BandRepository"/> class.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        public BandRepository(IBandClientManager bandClientManager)
            : base(bandClientManager)
        {
        }

        /// <summary>
        /// Gets information about all Microsoft Bands connected to the application host.
        /// </summary>
        /// <returns>An asynchronous task that returns a read-only collection of connected Bands when it completes.</returns>
        public async Task<IReadOnlyList<IBand>> GetBands()
        {
            return await this.GetBands(CancellationToken.None);
        }

        /// <summary>
        /// Gets information about all Microsoft Bands connected to the application host.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns a read-only collection of connected Bands when it completes.</returns>
        public async Task<IReadOnlyList<IBand>> GetBands(CancellationToken token)
        {
            var bandInfos = await this.BandClientManager.GetBandsAsync();
            var bands = new List<IBand>(bandInfos.Count());
            foreach (var bandInfo in bandInfos)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                bands.Add(await this.ConnectAndPerformFunctionAsync(bandInfo, async bc => new Band(bandInfo, await this.GetHardwareVersion(bc, token))));
            }

            return new ReadOnlyCollection<IBand>(bands);
        }

        /// <summary>
        /// Get the hardware version using the <paramref name="bandClient"/>.
        /// </summary>
        /// <param name="bandClient">The Band client</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the hardware version of the Band when it completes.</returns>
        protected async Task<int> GetHardwareVersion(IBandClient bandClient, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                var hardwareVersionString = await bandClient.GetHardwareVersionAsync(token);
                int hardwareVersion;
                if (int.TryParse(hardwareVersionString, out hardwareVersion))
                {
                    return hardwareVersion;
                }
            }

            return -1; // TODO: What to return on not-parse, and cancel?
        }
    }
}
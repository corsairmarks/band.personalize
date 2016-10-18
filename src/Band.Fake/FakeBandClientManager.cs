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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Band;

    /// <summary>
    /// A fake implementation of <see cref="IBandClientManager"/>.
    /// </summary>
    public class FakeBandClientManager : IBandClientManager
    {
        /// <summary>
        /// An instance of <see cref="Random"/> to use for choosing fake Band data.
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// The delay added to all Band operations.
        /// </summary>
        private readonly int delay;

        /// <summary>
        /// The instances or <see cref="IBandInfo"/> to which this <see cref="FakeBandClientManager"/> may connect
        /// </summary>
        private readonly IReadOnlyCollection<IBandInfo> bandInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandClientManager"/> class.  Defaults to a 0 millisecond delay.
        /// </summary>
        /// <param name="bandInfos">0 or more instances of <see cref="IBandInfo"/> to which this <see cref="FakeBandClientManager"/> may try to "connect."</param>
        public FakeBandClientManager(params IBandInfo[] bandInfos)
            : this(0, null, bandInfos)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandClientManager"/> class.
        /// </summary>
        /// <param name="delay">The delay in milliseconds to be added to all Band operations.  Automatically sets values less than 0 to 0.</param>
        /// <param name="random">An instance of <see cref="Random"/> to use for choosing fake Band data.</param>
        /// <param name="bandInfos">0 or more instances of <see cref="IBandInfo"/> to which this <see cref="FakeBandClientManager"/> may connect.</param>
        public FakeBandClientManager(int delay, Random random, params IBandInfo[] bandInfos)
        {
            this.delay = delay >= 0 ? delay : 0;
            this.random = random ?? new Random();
            this.bandInfos = new ReadOnlyCollection<IBandInfo>(bandInfos ?? new IBandInfo[0]);
        }

        /// <summary>
        /// "Connect" to the target instance of <see cref="IBandInfo"/>.
        /// </summary>
        /// <param name="bandInfo">The Band to which to connect.</param>
        /// <returns>An asynchronous task that returns a "connected" <see cref="IBandClient"/> when it completes.</returns>
        public async Task<IBandClient> ConnectAsync(IBandInfo bandInfo)
        {
            return await Task.Delay(this.delay).ContinueWith(t => new FakeBandClient(bandInfo, this.delay, this.random));
        }

        /// <summary>
        /// Gets the collection of Bands to which this <see cref="FakeBandClientManager"/> can attempt to "connect."
        /// </summary>
        /// <returns>An asynchronous task that returns an array of known Bands when it completes.</returns>
        public async Task<IBandInfo[]> GetBandsAsync()
        {
            return await Task
                .Delay(this.delay)
                .ContinueWith(t => this.bandInfos.ToArray());
        }

        /// <summary>
        /// Gets the collection of Bands to which this <see cref="FakeBandClientManager"/> can attempt to "connect."
        /// </summary>
        /// <param name="isBackground">Whether the task is running in a background thread.  Ignored for this fake implementation.</param>
        /// <returns>An asynchronous task that returns an array of known Bands when it completes.</returns>
        public Task<IBandInfo[]> GetBandsAsync(bool isBackground)
        {
            return this.GetBandsAsync();
        }
    }
}
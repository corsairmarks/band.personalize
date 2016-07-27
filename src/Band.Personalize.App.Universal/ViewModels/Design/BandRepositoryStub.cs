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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Windows.ApplicationModel;

    /// <summary>
    /// A stub for a fake <see cref="IBandRepository"/>.
    /// </summary>
    internal class BandRepositoryStub : IBandRepository
    {
        /// <summary>
        /// Sample Band data for design.
        /// </summary>
        private static readonly IReadOnlyList<IBand> DefaultBands = new ReadOnlyCollection<IBand>(new List<IBand>
            {
                new BandStub
                {
                    Name = "Sample Band",
                    ConnectionType = ConnectionType.Bluetooth,
                    IsConnected = true,
                    HardwareRevision = HardwareRevision.Band,
                    HardwareVersion = 3,
                },
                new BandStub
                {
                    Name = "Sample Band 2",
                    ConnectionType = ConnectionType.Usb,
                    IsConnected = true,
                    HardwareRevision = HardwareRevision.Band2,
                    HardwareVersion = 26,
                },
                new BandStub
                {
                    Name = "Sample Unknown Band",
                    ConnectionType = ConnectionType.Unknown,
                    IsConnected = false,
                    HardwareRevision = HardwareRevision.Unknown,
                    HardwareVersion = null,
                },
            });

        /// <summary>
        /// The lazy-initialized singleton instanxe of <see cref="BandRepositoryStub"/>.
        /// </summary>
        private static readonly Lazy<BandRepositoryStub> LazyInstance = new Lazy<BandRepositoryStub>(() => new BandRepositoryStub());

        /// <summary>
        /// Initializes a new instance of the <see cref="BandRepositoryStub"/> class.
        /// </summary>
        private BandRepositoryStub()
        {
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="BandRepositoryStub"/>.
        /// </summary>
        public static IBandRepository Instance
        {
            get { return LazyInstance.Value; }
        }

        #region IBandRepository Members

        /// <summary>
        /// Gets information about all Microsoft Bands paired with the application host.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns a read-only collection of paired Bands when it completes.</returns>
        public async Task<IReadOnlyList<IBand>> GetPairedBands(CancellationToken token)
        {
            if (DesignMode.DesignModeEnabled)
            {
                return await Task.FromResult(DefaultBands);
            }
            else
            {
                return await Task
                    .Delay(StubConstants.DefaultAsyncDelayMilliseconds, token)
                    .ContinueWith(t => DefaultBands, TaskContinuationOptions.OnlyOnRanToCompletion);
            }
        }

        #endregion
    }
}
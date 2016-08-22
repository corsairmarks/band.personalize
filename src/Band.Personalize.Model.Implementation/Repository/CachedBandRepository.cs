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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Library.Band;
    using Library.Repository;

    /// <summary>
    /// A caching repository for Bands.
    /// </summary>
    public class CachedBandRepository : IBandRepository, ICachedRepository<IBandRepository>, IDisposable
    {
        /// <summary>
        /// The uncached Band repository.
        /// </summary>
        private readonly IBandRepository uncached;

        /// <summary>
        /// A semaphore to control access to <see cref="cached"/>.
        /// </summary>
        private SemaphoreSlim cacheSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// The cached themes.
        /// </summary>
        private Dictionary<string, IBand> cached;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedBandRepository"/> class.
        /// </summary>
        /// <param name="uncached">The uncached Band repository.</param>
        public CachedBandRepository(IBandRepository uncached)
        {
            if (uncached == null)
            {
                throw new ArgumentNullException(nameof(uncached));
            }

            this.uncached = uncached;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CachedBandRepository"/> class.
        /// </summary>
        ~CachedBandRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the repository that is being cached.
        /// </summary>
        public IBandRepository Repository
        {
            get { return this; }
        }

        /// <summary>
        /// Gets information about a specific Microsoft Band paired with the application host by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the Band to retrieve.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that when complete that returns, if successful, a paired Band; otherwise, <c>null</c>.</returns>
        public async Task<IBand> GetPairedBandAsync(string name, CancellationToken token)
        {
            await this.cacheSemaphore.WaitAsync();
            try
            {
                await this.EnsureBandsCached(token);

                IBand band;
                if (this.cached.TryGetValue(name, out band))
                {
                    return band;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                this.cacheSemaphore.Release();
            }
        }

        /// <summary>
        /// Gets information about all Microsoft Bands paired with the application host.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns a read-only collection of paired Bands when it completes.</returns>
        public async Task<IReadOnlyList<IBand>> GetPairedBandsAsync(CancellationToken token)
        {
            await this.cacheSemaphore.WaitAsync();
            try
            {
                await this.EnsureBandsCached(token);

                return this.cached.Values.ToList().AsReadOnly();
            }
            finally
            {
                this.cacheSemaphore.Release();
            }
        }

        /// <summary>
        /// Clear the cached data.
        /// </summary>
        public void Clear()
        {
            this.cacheSemaphore.Wait();
            try
            {
                if (this.cached != null)
                {
                    this.cached = null;
                }
            }
            finally
            {
                this.cacheSemaphore.Release();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Whether this instance is being disposed.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.cacheSemaphore != null)
                {
                    this.cacheSemaphore.Dispose();
                    this.cacheSemaphore = null;
                }
            }
        }

        private async Task EnsureBandsCached(CancellationToken token)
        {
            if (this.cached == null)
            {
                var uncachedThemes = await this.uncached.GetPairedBandsAsync(token);
                this.cached = uncachedThemes.ToDictionary(k => k.Name, v => v);
            }
        }
    }
}
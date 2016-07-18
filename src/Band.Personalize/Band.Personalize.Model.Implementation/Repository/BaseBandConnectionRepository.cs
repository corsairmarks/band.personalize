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
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Band;

    /// <summary>
    /// A base for facades that can connect to Microsoft Bands.
    /// </summary>
    public abstract class BaseBandConnectionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBandConnectionRepository"/> class.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        public BaseBandConnectionRepository(IBandClientManager bandClientManager)
        {
            if (bandClientManager == null)
            {
                throw new ArgumentNullException(nameof(bandClientManager));
            }

            this.BandClientManager = bandClientManager;
        }

        /// <summary>
        /// Gets the Band client manager.
        /// </summary>
        protected IBandClientManager BandClientManager { get; }

        /// <summary>
        /// Connect the specified <paramref name="band"/> and execute the <paramref name="clientAction"/>.
        /// </summary>
        /// <param name="band">The Band to which to connect.</param>
        /// <param name="clientAction">The action to be executed while connected.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        protected async Task ConnectAndPerformActionAsync(IBandInfo band, Func<IBandClient, Task> clientAction)
        {
            await this.ConnectAndPerformActionAsync(band, CancellationToken.None, (bc, t) => clientAction(bc));
        }

        /// <summary>
        /// Connect the specified <paramref name="band"/> and execute the <paramref name="clientAction"/>.
        /// </summary>
        /// <param name="band">The Band to which to connect.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <param name="clientAction">The action to be executed while connected.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        protected async Task ConnectAndPerformActionAsync(IBandInfo band, CancellationToken token, Func<IBandClient, CancellationToken, Task> clientAction)
        {
            if (!token.IsCancellationRequested)
            {
                try
                {
                    using (var theBand = await this.BandClientManager.ConnectAsync(band))
                    {
                        await clientAction(theBand, token);
                    }
                }
                catch (BandException e)
                {
                    // TODO: report failure to the user
                }
            }
        }

        /// <summary>
        /// Connect the specified <paramref name="band"/> and execute the <paramref name="clientFunction"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the <paramref name="clientFunction"/>.</typeparam>
        /// <param name="band">The Band to which to connect.</param>
        /// <param name="clientFunction">The function to be executed while connected.</param>
        /// <returns>An asynchronous task that returns <typeparamref name="T"/> when work is complete.</returns>
        protected async Task<T> ConnectAndPerformFunctionAsync<T>(IBandInfo band, Func<IBandClient, Task<T>> clientFunction)
        {
            return await this.ConnectAndPerformFunctionAsync(band, CancellationToken.None, (bc, t) => clientFunction(bc));
        }

        /// <summary>
        /// Connect the specified <paramref name="band"/> and execute the <paramref name="clientFunction"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the <paramref name="clientFunction"/>.</typeparam>
        /// <param name="band">The Band to which to connect.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <param name="clientFunction">The function to be executed while connected.</param>
        /// <returns>An asynchronous task that returns <typeparamref name="T"/> when work is complete.</returns>
        protected async Task<T> ConnectAndPerformFunctionAsync<T>(IBandInfo band, CancellationToken token, Func<IBandClient, CancellationToken, Task<T>> clientFunction)
        {
            if (!token.IsCancellationRequested)
            {
                try
                {
                    using (var theBand = await this.BandClientManager.ConnectAsync(band))
                    {
                        return await clientFunction(theBand, token);
                    }
                }
                catch (BandException e)
                {
                    // TODO: report failure to the user
                }
            }

            return default(T); // TODO: does this make sense?
        }
    }
}
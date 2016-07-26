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

namespace Band.Personalize.Model.Library.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Band;

    /// <summary>
    /// Extension methods for the <see cref="IBandClientManager"/> interface.
    /// </summary>
    public static class IBandClientManagerExtensions
    {
        /// <summary>
        /// Connect to the specified <paramref name="bandInfo"/> and execute the <paramref name="clientAction"/>.
        /// </summary>
        /// <param name="bandClientManager">The Band client manager.</param>
        /// <param name="bandInfo">The Band to which to connect.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <param name="clientAction">The action to be executed while connected.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bandClientManager"/>, <paramref name="bandInfo"/>, or <paramref name="clientAction"/> is <c>null</c>.</exception>
        public static async Task ConnectAndPerformActionAsync(this IBandClientManager bandClientManager, IBandInfo bandInfo, CancellationToken token, Func<IBandClient, CancellationToken, Task> clientAction)
        {
            if (bandClientManager == null)
            {
                throw new ArgumentNullException(nameof(bandClientManager));
            }
            else if (bandInfo == null)
            {
                throw new ArgumentNullException(nameof(bandInfo));
            }
            else if (clientAction == null)
            {
                throw new ArgumentNullException(nameof(clientAction));
            }

            token.ThrowIfCancellationRequested();
            using (var theBand = await bandClientManager.ConnectAsync(bandInfo))
            {
                await clientAction(theBand, token);
            }
        }

        /// <summary>
        /// Connect to the specified <paramref name="bandInfo"/> and execute the <paramref name="clientFunction"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the <paramref name="clientFunction"/>.</typeparam>
        /// <param name="bandClientManager">The Band client manager.</param>
        /// <param name="bandInfo">The Band to which to connect.</param>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <param name="clientFunction">The function to be executed while connected.</param>
        /// <returns>An asynchronous task that returns <typeparamref name="T"/> when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="bandClientManager"/>, <paramref name="bandInfo"/>, or <paramref name="clientFunction"/> is <c>null</c>.</exception>
        public static async Task<T> ConnectAndPerformFunctionAsync<T>(this IBandClientManager bandClientManager, IBandInfo bandInfo, CancellationToken token, Func<IBandClient, CancellationToken, Task<T>> clientFunction)
        {
            if (bandClientManager == null)
            {
                throw new ArgumentNullException(nameof(bandClientManager));
            }
            else if (bandInfo == null)
            {
                throw new ArgumentNullException(nameof(bandInfo));
            }
            else if (clientFunction == null)
            {
                throw new ArgumentNullException(nameof(clientFunction));
            }

            token.ThrowIfCancellationRequested();
            using (var theBand = await bandClientManager.ConnectAsync(bandInfo))
            {
                return await clientFunction(theBand, token);
            }
        }
    }
}
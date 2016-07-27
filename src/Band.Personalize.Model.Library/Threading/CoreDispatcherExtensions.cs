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

namespace Band.Personalize.Model.Library.Threading
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Core;

    /// <summary>
    /// Extension methods for the <see cref="CoreDispatcher"/> class.
    /// </summary>
    public static class CoreDispatcherExtensions
    {
        /// <summary>
        /// Execute the <paramref name="dispatchActionAsync"/> on the <paramref name="dispatcher"/>'s thread at
        /// <see cref="CoreDispatcherPriority.Normal"/> priority and wait for it to complete before returning.
        /// This method will propagate any exceptions thrown by the <paramref name="dispatchActionAsync"/>.
        /// </summary>
        /// <param name="dispatcher">The <see cref="CoreDispatcher"/> with which to schedule the <paramref name="dispatchActionAsync"/>.</param>
        /// <param name="dispatchActionAsync">An action to perform on the <paramref name="dispatcher"/> thread.</param>
        /// <returns>An asynchronous task that returns when the <paramref name="dispatcher"/>-based task is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispatcher"/> or <paramref name="dispatchActionAsync"/> is <c>null</c>.</exception>
        public static async Task WaitForRunAsync(this CoreDispatcher dispatcher, Func<Task> dispatchActionAsync)
        {
            await WaitForRunAsync(dispatcher, CoreDispatcherPriority.Normal, dispatchActionAsync);
        }

        /// <summary>
        /// Execute the <paramref name="dispatchActionAsync"/> on the <paramref name="dispatcher"/> thread at
        /// the specified <paramref name="priority"/> and wait for it to complete before returning.
        /// This method will propagate any exceptions thrown by the <paramref name="dispatchActionAsync"/>.
        /// </summary>
        /// <param name="dispatcher">The <see cref="CoreDispatcher"/> with which to schedule the <paramref name="dispatchActionAsync"/>.</param>
        /// <param name="priority">The priority at which to execute <paramref name="dispatchActionAsync"/>.</param>
        /// <param name="dispatchActionAsync">An action to perform on the <paramref name="dispatcher"/> thread.</param>
        /// <returns>An asynchronous task that returns when the <paramref name="dispatcher"/>-based task is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispatcher"/> or <paramref name="dispatchActionAsync"/> is <c>null</c>.</exception>
        public static async Task WaitForRunAsync(this CoreDispatcher dispatcher, CoreDispatcherPriority priority, Func<Task> dispatchActionAsync)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }
            else if (dispatchActionAsync == null)
            {
                throw new ArgumentNullException(nameof(dispatchActionAsync));
            }

            var taskSource = new TaskCompletionSource<object>();
            await dispatcher.RunAsync(priority, async () =>
            {
                try
                {
                    await dispatchActionAsync();
                    taskSource.SetResult(null);
                }
                catch (Exception e)
                {
                    taskSource.SetException(e);
                }
            });

            await taskSource.Task;
        }

        /// <summary>
        /// Execute the <paramref name="dispatchFunctionAsync"/> on the <paramref name="dispatcher"/> thread at
        /// <see cref="CoreDispatcherPriority.Normal"/> priority and wait for it to complete before returning.
        /// This method will propagate any exceptions thrown by the <paramref name="dispatchFunctionAsync"/>.
        /// </summary>
        /// <typeparam name="TResult">The return <see cref="Type"/> of <paramref name="dispatchFunctionAsync"/>.</typeparam>
        /// <param name="dispatcher">The <see cref="CoreDispatcher"/> with which to schedule the <paramref name="dispatchFunctionAsync"/>.</param>
        /// <param name="dispatchFunctionAsync">A function to perform on the <paramref name="dispatcher"/> thread.</param>
        /// <returns>An asynchronous task that returns when the <paramref name="dispatcher"/>-based task is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispatcher"/> or <paramref name="dispatchFunctionAsync"/> is <c>null</c>.</exception>
        public static async Task<TResult> WaitForRunAsync<TResult>(this CoreDispatcher dispatcher, Func<Task<TResult>> dispatchFunctionAsync)
        {
            return await WaitForRunAsync(dispatcher, CoreDispatcherPriority.Normal, dispatchFunctionAsync);
        }

        /// <summary>
        /// Execute the <paramref name="dispatchFunctionAsync"/> on the <paramref name="dispatcher"/> thread at
        /// the specified <paramref name="priority"/> and wait for it to complete before returning.
        /// This method will propagate any exceptions thrown by the <paramref name="dispatchFunctionAsync"/>.
        /// </summary>
        /// <typeparam name="TResult">The return <see cref="Type"/> of <paramref name="dispatchFunctionAsync"/>.</typeparam>
        /// <param name="dispatcher">The <see cref="CoreDispatcher"/> with which to schedule the <paramref name="dispatchFunctionAsync"/>.</param>
        /// <param name="priority">The priority at which to execute <paramref name="dispatchFunctionAsync"/>.</param>
        /// <param name="dispatchFunctionAsync">A function to perform on the <paramref name="dispatcher"/> thread.</param>
        /// <returns>An asynchronous task that returns when the <paramref name="dispatcher"/>-based task is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dispatcher"/> or <paramref name="dispatchFunctionAsync"/> is <c>null</c>.</exception>
        public static async Task<TResult> WaitForRunAsync<TResult>(this CoreDispatcher dispatcher, CoreDispatcherPriority priority, Func<Task<TResult>> dispatchFunctionAsync)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }
            else if (dispatchFunctionAsync == null)
            {
                throw new ArgumentNullException(nameof(dispatchFunctionAsync));
            }

            var taskSource = new TaskCompletionSource<TResult>();
            await dispatcher.RunAsync(priority, async () =>
            {
                try
                {
                    taskSource.SetResult(await dispatchFunctionAsync());
                }
                catch (Exception e)
                {
                    taskSource.SetException(e);
                }
            });

            return await taskSource.Task;
        }
    }
}
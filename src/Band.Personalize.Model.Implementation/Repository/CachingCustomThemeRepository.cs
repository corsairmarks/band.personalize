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
    using System.Threading;
    using System.Threading.Tasks;
    using Library.Repository;
    using Library.Theme;

    /// <summary>
    /// A caching repository for custom themes.
    /// </summary>
    public class CachingCustomThemeRepository : ICustomThemeRepository, IDisposable
    {
        /// <summary>
        /// The uncached custom theme repository.
        /// </summary>
        private readonly ICustomThemeRepository uncached;

        /// <summary>
        /// A semaphore to control access to <see cref="cached"/>.
        /// </summary>
        private SemaphoreSlim cacheSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// The cached themes.
        /// </summary>
        private Dictionary<Guid, TitledRgbColorTheme> cached;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingCustomThemeRepository"/> class.
        /// </summary>
        /// <param name="uncached">The uncached custom theme repository.</param>
        public CachingCustomThemeRepository(ICustomThemeRepository uncached)
        {
            if (uncached == null)
            {
                throw new ArgumentNullException(nameof(uncached));
            }

            this.uncached = uncached;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CachingCustomThemeRepository"/> class.
        /// </summary>
        ~CachingCustomThemeRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Persists the <paramref name="theme"/> and returns the newly-assigned identifier.
        /// </summary>
        /// <param name="theme">The theme to persist.</param>
        /// <returns>An asynchronous task that returns the <paramref name="theme"/> identifier.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theme"/> is <c>null</c>.</exception>
        public async Task<Guid> PersistThemeAsync(TitledRgbColorTheme theme)
        {
            var id = await this.uncached.PersistThemeAsync(theme);

            this.CacheTheme(id, theme);

            return id;
        }

        /// <summary>
        /// Persists the <paramref name="theme"/>.  This operation overwrites any existing theme with the specified <paramref name="id"/> without warning.
        /// </summary>
        /// <param name="id">The persisted theme identifier to replace.</param>
        /// <param name="theme">The theme to persist.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theme"/> is <c>null</c>.</exception>
        public async Task PersistThemeAsync(Guid id, TitledRgbColorTheme theme)
        {
            await this.uncached.PersistThemeAsync(id, theme);

            this.CacheTheme(id, theme);
        }

        /// <summary>
        /// Retrieve all persisted <see cref="TitledRgbColorTheme"/> objects.
        /// </summary>
        /// <returns>An asynchronous task that returns a read-only collection containing all <see cref="TitledRgbColorTheme"/> objects that have been persisted.</returns>
        public async Task<IReadOnlyDictionary<Guid, TitledRgbColorTheme>> GetThemesAsync()
        {
            Dictionary<Guid, TitledRgbColorTheme> themes = null;
            await this.cacheSemaphore.WaitAsync();
            try
            {
                if (this.cached != null)
                {
                    themes = this.cached;
                }
                else
                {
                    var uncachedThemes = await this.uncached.GetThemesAsync();
                    this.cached = themes = uncachedThemes.ToDictionary(k => k.Key, v => v.Value);
                }
            }
            finally
            {
                this.cacheSemaphore.Release();
            }

            return themes;
        }

        /// <summary>
        /// Delete the persisted theme with the specified <paramref name="id"/>.  This operation deletes without warning.
        /// </summary>
        /// <param name="id">The persisted theme idnetifier to delete.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task DeleteThemeAsync(Guid id)
        {
            await this.uncached.DeleteThemeAsync(id);

            this.EnterCacheSemaphore(() =>
            {
                if (this.cached != null)
                {
                    this.cached.Remove(id);
                }
            });
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

        /// <summary>
        /// Add the <paramref name="theme"/> with <paramref name="id"/> to <see cref="cached"/> while respecting <see cref="cacheSemaphore"/>.
        /// The theme is only cached if the cache exists.
        /// </summary>
        /// <param name="id">The identifier of the <paramref name="theme"/> to cache.</param>
        /// <param name="theme">The theme to cache.</param>
        private void CacheTheme(Guid id, TitledRgbColorTheme theme)
        {
            this.EnterCacheSemaphore(() =>
            {
                if (this.cached != null)
                {
                    this.cached[id] = theme;
                }
            });
        }

        /// <summary>
        /// Enter a work section protected by the <see cref="cacheSemaphore"/>.
        /// </summary>
        /// <param name="action">The work to complete.</param>
        private void EnterCacheSemaphore(Action action)
        {
            this.cacheSemaphore.Wait();
            try
            {
                action();
            }
            finally
            {
                this.cacheSemaphore.Release();
            }
        }
    }
}
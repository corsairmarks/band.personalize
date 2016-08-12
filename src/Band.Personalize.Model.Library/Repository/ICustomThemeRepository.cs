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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Theme;

    /// <summary>
    /// A repository for custom themes.
    /// </summary>
    public interface ICustomThemeRepository
    {
        /// <summary>
        /// Persists the <paramref name="theme"/> and returns the newly-assigned identifier.
        /// </summary>
        /// <param name="theme">The theme to persist.</param>
        /// <returns>An asynchronous task that returns the <paramref name="theme"/> identifier.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theme"/> is <c>null</c>.</exception>
        Task<Guid> PersistThemeAsync(TitledRgbColorTheme theme);

        /// <summary>
        /// Persists the <paramref name="theme"/>.  This operation overwrites any existing theme with the specified <paramref name="id"/> without warning.
        /// </summary>
        /// <param name="id">The persisted theme identifier to replace.</param>
        /// <param name="theme">The theme to persist.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theme"/> is <c>null</c>.</exception>
        Task PersistThemeAsync(Guid id, TitledRgbColorTheme theme);

        /// <summary>
        /// Retrieve all persisted <see cref="TitledRgbColorTheme"/> objects.
        /// </summary>
        /// <returns>An asynchronous task that returns a read-only collection containing all <see cref="TitledRgbColorTheme"/> objects that have been persisted.</returns>
        Task<IReadOnlyDictionary<Guid, TitledRgbColorTheme>> GetThemesAsync();

        /// <summary>
        /// Delete the persisted theme with the specified <paramref name="id"/>.  This operation deletes without warning.
        /// </summary>
        /// <param name="id">The persisted theme identifier to delete.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        Task DeleteThemeAsync(Guid id);
    }
}
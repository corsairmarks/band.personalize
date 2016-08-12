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
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Repository;
    using Library.Theme;
    using Newtonsoft.Json;
    using Windows.Foundation;
    using Windows.Storage;

    /// <summary>
    /// A repository for custom themes.
    /// </summary>
    public class CustomThemeRepository : ICustomThemeRepository
    {
        /// <summary>
        /// The directory in the isolated User Store for storing themes.
        /// </summary>
        private const string ThemeStoreDirectory = "custom-themes";

        /// <summary>
        /// The prefix of theme files.
        /// </summary>
        private const string ThemeFileNamePrefix = "theme-";

        /// <summary>
        /// The <see cref="IStorageFolder"/> for persisting files.
        /// </summary>
        private readonly IStorageFolder storage;

        /// <summary>
        /// The <see cref="JsonSerializer"/> for reading and writing objects.
        /// </summary>
        private readonly JsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomThemeRepository"/> class.
        /// </summary>
        /// <param name="storage">The file storage location for custom themes.</param>
        /// <param name="serializer">The JSON serializer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="serializer"/> is <c>null</c>.</exception>
        public CustomThemeRepository(IStorageFolder storage, JsonSerializer serializer)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }
            else if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            this.storage = storage;
            this.serializer = serializer;
        }

        /// <summary>
        /// Persists the <paramref name="theme"/> and returns the newly-assigned identifier.
        /// </summary>
        /// <param name="theme">The theme to persist.</param>
        /// <returns>An asynchronous task that returns the <paramref name="theme"/> identifier.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="theme"/> is <c>null</c>.</exception>
        public async Task<Guid> PersistThemeAsync(TitledRgbColorTheme theme)
        {
            var id = Guid.NewGuid();
            await this.PersistThemeAsync(id, theme);
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
            if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            var folder = await this.EnsureThemeStorageFolderExists();
            var file = await folder.CreateFileAsync(this.GetThemeStoreFileName(id), CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                using (var writer = new StreamWriter(stream))
                {
                    this.serializer.Serialize(writer, theme);
                }
            }
        }

        /// <summary>
        /// Retrieve all persisted <see cref="TitledRgbColorTheme"/> objects.
        /// </summary>
        /// <returns>An asynchronous task that returns a read-only collection containing all <see cref="TitledRgbColorTheme"/> objects that have been persisted.</returns>
        public async Task<IReadOnlyDictionary<Guid, TitledRgbColorTheme>> GetThemesAsync()
        {
            var folder = await this.EnsureThemeStorageFolderExists();
            var files = await folder.GetFilesAsync();
            var themeFiles = files.Where(sf => sf.Name.StartsWith(ThemeFileNamePrefix));
            IDictionary<Guid, TitledRgbColorTheme> dictionary;
            if (themeFiles.Any())
            {
                var themes = await Task.WhenAll(themeFiles.Select(this.ReadThemeFileAsync));
                dictionary = themes.ToDictionary(k => k.Item1, v => v.Item2);
            }
            else
            {
                dictionary = new Dictionary<Guid, TitledRgbColorTheme>();
            }

            return new ReadOnlyDictionary<Guid, TitledRgbColorTheme>(dictionary);
        }

        /// <summary>
        /// Delete the persisted theme with the specified <paramref name="id"/>.  This operation deletes without warning.
        /// </summary>
        /// <param name="id">The persisted theme idnetifier to delete.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        public async Task DeleteThemeAsync(Guid id)
        {
            var folder = await this.EnsureThemeStorageFolderExists();
            var file = await folder.GetItemAsync(this.GetThemeStoreFileName(id));
            if (file != null && file.IsOfType(StorageItemTypes.File))
            {
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }

        /// <summary>
        /// This method ensures the <see cref="ThemeStoreDirectory"/> has been created before doing work.
        /// </summary>
        /// <returns>An asyncronous task that returns the existing or created <see cref="ThemeStoreDirectory"/>.</returns>
        private IAsyncOperation<StorageFolder> EnsureThemeStorageFolderExists()
        {
            return this.storage.CreateFolderAsync(ThemeStoreDirectory, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Deserialize an instance of <see cref="TitledRgbColorTheme"/> from the <paramref name="file"/>.
        /// </summary>
        /// <param name="file">The store which contains theme files.</param>
        /// <returns>An asynchronous task that returns the deserialized <see cref="TitledRgbColorTheme"/>.</returns>
        private async Task<Tuple<Guid, TitledRgbColorTheme>> ReadThemeFileAsync(IStorageFile file)
        {
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        try
                        {
                            return Tuple.Create(
                                Guid.Parse(file.Name.Substring(ThemeFileNamePrefix.Length)),
                                this.serializer.Deserialize<TitledRgbColorTheme>(jsonReader));
                        }
                        catch (JsonSerializationException)
                        {
                            // NOTE: probably a poorly-formatted file - it may be necessary to hande this and pass errors up the stack, see: http://www.newtonsoft.com/json/help/html/SerializationErrorHandling.htm
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the filename for a theme based in its <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The value of <see cref="TitledRgbColorTheme.Title"/>.</param>
        /// <returns>The filename for a theme base on its <paramref name="id"/>.</returns>
        private string GetThemeStoreFileName(Guid id)
        {
            return ThemeFileNamePrefix + id.ToString();
        }
    }
}
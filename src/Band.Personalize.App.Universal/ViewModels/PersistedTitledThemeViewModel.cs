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

namespace Band.Personalize.App.Universal.ViewModels
{
    using System;
    using System.Windows.Input;
    using Model.Library.Repository;
    using Prism.Commands;
    using Prism.Windows.AppModel;
    using Windows.UI.Popups;

    /// <summary>
    /// The View Model for a titled theme that has been persisted.
    /// </summary>
    public class PersistedTitledThemeViewModel : TitledThemeViewModel
    {
        /// <summary>
        /// The resource loader.
        /// </summary>
        private readonly IResourceLoader resourceLoader;

        /// <summary>
        /// The custom theme repository.
        /// </summary>
        private readonly ICustomThemeRepository customThemeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistedTitledThemeViewModel"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the theme.</param>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="customThemeRepository">The custom theme repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resourceLoader"/> is <c>null</c>.</exception>
        public PersistedTitledThemeViewModel(Guid id, IResourceLoader resourceLoader, ICustomThemeRepository customThemeRepository)
        {
            if (resourceLoader == null)
            {
                throw new ArgumentNullException(nameof(resourceLoader));
            }
            else if (customThemeRepository == null)
            {
                throw new ArgumentNullException(nameof(customThemeRepository));
            }

            this.Id = id;
            this.resourceLoader = resourceLoader;
            this.customThemeRepository = customThemeRepository;

            this.DeleteCommand = DelegateCommand.FromAsyncHandler(async () =>
            {
                var deleteContentDialog = new MessageDialog(this.resourceLoader.GetString("DeleteThemeMessageDialog/Content"), this.resourceLoader.GetString("DeleteThemeMessageDialog/Title"))
                {
                    DefaultCommandIndex = 0,
                    CancelCommandIndex = 1,
                };

                deleteContentDialog.Commands.Add(new UICommand(this.resourceLoader.GetString("DeleteThemeMessageDialog/PrimaryButtonText"), async c => await this.customThemeRepository.DeleteThemeAsync(this.Id)));
                deleteContentDialog.Commands.Add(new UICommand(this.resourceLoader.GetString("DeleteThemeMessageDialog/SecondaryButtonText")));

                await deleteContentDialog.ShowAsync();
            });
        }

        /// <summary>
        /// Gets the "Delete" command for a custom theme.
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Gets the unique identifier for the theme.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Create a shallow clone of this instance.
        /// </summary>
        /// <returns>A new instance of <see cref="TitledThemeViewModel"/> with the same data as this instance.</returns>
        public override TitledThemeViewModel ShallowClone()
        {
            return new PersistedTitledThemeViewModel(this.Id, this.resourceLoader, this.customThemeRepository)
            {
                Title = this.Title,
                Base = this.Base,
                HighContrast = this.HighContrast,
                Lowlight = this.Lowlight,
                Highlight = this.Highlight,
                Muted = this.Muted,
                SecondaryText = this.SecondaryText,
            };
        }
    }
}
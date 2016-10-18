﻿// Copyright 2016 Nicholas Butcher
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Model.Library.Band;
    using Prism.Windows.Navigation;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The design View Model for the Band Page.
    /// </summary>
    public class DesignBandPageViewModel : IBandPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignBandPageViewModel"/> class.
        /// </summary>
        public DesignBandPageViewModel()
        {
            // TODO: populate the default band theme (remove OnNavigatedTo)
            this.AvailableThemes = null;
            this.CurrentBand = null;
            this.CurrentTheme = null;
            this.CurrentMeTileImage = null;
        }

        /// <summary>
        /// Gets the "Refresh" command
        /// </summary>
        public ICommand RefreshPersonalizationCommand { get; }

        /// <summary>
        /// Gets the "Apply" command for them theme.
        /// </summary>
        public ICommand ApplyPersonalizationCommand { get; }

        /// <summary>
        /// Gets the "Browser" command for the Me Tile image.
        /// </summary>
        public ICommand BrowserForMeTileImageCommand { get; }

        /// <summary>
        /// Gets the "Save" command for the Me Tile image.
        /// </summary>
        public ICommand SaveMeTileImageCommand { get; }

        /// <summary>
        /// Gets the clear command for all Me Tile image validation messages.
        /// </summary>
        public ICommand ClearValidationMessagesCommand { get; }

        /// <summary>
        /// Gets the command to select a theme.
        /// </summary>
        public ICommand ChooseThemeCommand { get; }

        /// <summary>
        /// Gets th command to refresh the <see cref="AvailableThemes"/>.
        /// </summary>
        public ICommand RefreshAvailableThemesCommand { get; }

        /// <summary>
        /// Gets the command to persist a theme.
        /// </summary>
        public ICommand PersistThemeCommand { get; }

        /// <summary>
        /// Gets the current Band.
        /// </summary>
        public IBand CurrentBand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the Me Tile image height for the original Microsoft Band.  The only applies when <see cref="CurrentBand"/> is not <see cref="HardwareRevision.Band"/>.
        /// </summary>
        public bool IsUseOriginalBandHeight { get; set; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentTheme"/>.
        /// </summary>
        public bool IsThemeBusy { get; }

        /// <summary>
        /// Gets a value indicating whether the current theme has been edited from the last refresh or theme choice.
        /// </summary>
        public bool IsThemeEdited { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        public bool IsMeTileImageBusy { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy for the <see cref="CurrentTheme"/>.
        /// </summary>
        public bool NotIsThemeBusy
        {
            get { return !this.IsThemeBusy; }
        }

        /// <summary>
        /// Gets a value indicating whether the current theme has not been edited from the last refresh or theme choice.
        /// </summary>
        public bool NotIsThemeEdited
        {
            get { return !this.IsThemeEdited; }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        public bool NotIsMeTileImageBusy
        {
            get { return !this.IsMeTileImageBusy; }
        }

        /// <summary>
        /// Gets all currently available themes.
        /// </summary>
        public ReadOnlyObservableCollection<IGrouping<string, TitledThemeViewModel>> AvailableThemes { get; }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public TitledThemeViewModel CurrentTheme { get; }

        /// <summary>
        /// Gets the status message of the most recent "Save" operation.
        /// </summary>
        public string SaveStatusMessage { get; }

        /// <summary>
        /// Gets the currently-selected Me Tile image.
        /// </summary>
        public WriteableBitmap CurrentMeTileImage { get; }
    }
}
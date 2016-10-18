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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Model.Library.Band;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public interface IBandPageViewModel
    {
        /// <summary>
        /// Gets the "Refresh" command
        /// </summary>
        ICommand RefreshPersonalizationCommand { get; }

        /// <summary>
        /// Gets the "Apply" command for them theme.
        /// </summary>
        ICommand ApplyPersonalizationCommand { get; }

        /// <summary>
        /// Gets the "Browser" command for the Me Tile image.
        /// </summary>
        ICommand BrowserForMeTileImageCommand { get; }

        /// <summary>
        /// Gets the "Save" command for the Me Tile image.
        /// </summary>
        ICommand SaveMeTileImageCommand { get; }

        /// <summary>
        /// Gets the clear command for all Me Tile image validation messages.
        /// </summary>
        ICommand ClearValidationMessagesCommand { get; }

        /// <summary>
        /// Gets the command to select a theme.
        /// </summary>
        ICommand ChooseThemeCommand { get; }

        /// <summary>
        /// Gets th command to refresh the <see cref="AvailableThemes"/>.
        /// </summary>
        ICommand RefreshAvailableThemesCommand { get; }

        /// <summary>
        /// Gets the command to persist a theme.
        /// </summary>
        ICommand PersistThemeCommand { get; }

        /// <summary>
        /// Gets the current Band.
        /// </summary>
        IBand CurrentBand { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the Me Tile image height for the original Microsoft Band.  The only applies when <see cref="CurrentBand"/> is not <see cref="HardwareRevision.Band"/>.
        /// </summary>
        bool IsUseOriginalBandHeight { get; set; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentTheme"/>.
        /// </summary>
        bool IsThemeBusy { get; }

        /// <summary>
        /// Gets a value indicating whether the current theme has been edited from the last refresh or theme choice.
        /// </summary>
        bool IsThemeEdited { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        bool IsMeTileImageBusy { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy for the <see cref="CurrentTheme"/>.
        /// </summary>
        bool NotIsThemeBusy { get; }

        /// <summary>
        /// Gets a value indicating whether the current theme has not been edited from the last refresh or theme choice.
        /// </summary>
        bool NotIsThemeEdited { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        bool NotIsMeTileImageBusy { get; }

        /// <summary>
        /// Gets all currently available themes.
        /// </summary>
        ReadOnlyObservableCollection<IGrouping<string, TitledThemeViewModel>> AvailableThemes { get; }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        TitledThemeViewModel CurrentTheme { get; }

        /// <summary>
        /// Gets the status message of the most recent "Save" operation.
        /// </summary>
        string SaveStatusMessage { get; }

        /// <summary>
        /// Gets the currently-selected Me Tile image.
        /// </summary>
        WriteableBitmap CurrentMeTileImage { get; }
    }
}
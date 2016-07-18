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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Model.Library.Band;
    using Model.Library.Color;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Prism.Commands;
    using Prism.Windows.AppModel;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public class BandPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The resource loader.
        /// </summary>
        private readonly IResourceLoader resourceLoader;

        /// <summary>
        /// The Band personalizer.
        /// </summary>
        private readonly IBandPersonalizer bandPersonalizer;

        /// <summary>
        /// The collection of theme colors for editing.
        /// </summary>
        private readonly ObservableCollection<ThemeColorViewModel> currentThemeColors;

        /// <summary>
        /// The current Band.
        /// </summary>
        private IBand currentBand;

        /// <summary>
        /// A value indicating whether to use the Me Tile image height for the original Microsoft Band.  The only applies when <see cref="CurrentBand"/> is not <see cref="HardwareRevision.Band"/>.
        /// </summary>
        private bool isUseOriginalBandHeight;

        /// <summary>
        /// A value indicationg whether the <see cref="RefreshPersonalizationCommand"/> is busy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// The currently-selected Me Tile image.
        /// </summary>
        private WriteableBitmap currentMeTileImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPageViewModel"/> class.
        /// </summary>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="bandPersonalizer">The Band personalizer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bandPersonalizer"/> is <c>null</c>.</exception>
        public BandPageViewModel(IResourceLoader resourceLoader, IBandPersonalizer bandPersonalizer)
        {
            if (resourceLoader == null)
            {
                throw new ArgumentNullException(nameof(resourceLoader));
            }
            else if (bandPersonalizer == null)
            {
                throw new ArgumentNullException(nameof(bandPersonalizer));
            }

            this.resourceLoader = resourceLoader;
            this.bandPersonalizer = bandPersonalizer;
            this.currentThemeColors = new ObservableCollection<ThemeColorViewModel>();
            this.CurrentThemeColors = new ReadOnlyObservableCollection<ThemeColorViewModel>(this.currentThemeColors);

            var refreshPersonalizationCommand = new CompositeCommand();
            refreshPersonalizationCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = true, () => !this.IsBusy));
            refreshPersonalizationCommand.RegisterCommand(DelegateCommand<int>.FromAsyncHandler(this.RefreshPersonalization));
            refreshPersonalizationCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = false));
            this.RefreshPersonalizationCommand = refreshPersonalizationCommand;

            var applyPersonalizationCommand = new CompositeCommand();
            applyPersonalizationCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = true, () => !this.IsBusy));
            applyPersonalizationCommand.RegisterCommand(DelegateCommand<int>.FromAsyncHandler(this.ApplyPersonalization));
            applyPersonalizationCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = false));
            this.ApplyPersonalizationCommand = applyPersonalizationCommand;

            this.BrowserForMeTileImageCommand = DelegateCommand.FromAsyncHandler(async () =>
            {
                var picker = new FileOpenPicker
                {
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                    FileTypeFilter = { ".jpg", ".jpeg", ".png", },
                };

                var chosenFile = await picker.PickSingleFileAsync();
                if (chosenFile != null)
                {
                    var dimensions = this.IsUseOriginalBandHeight
                        ? HardwareRevision.Band.GetDefaultMeTileDimensions()
                        : this.CurrentBand.HardwareRevision.GetDefaultMeTileDimensions();
                    var bitmap = new WriteableBitmap(dimensions.Width, dimensions.Height);
                    using (var stream = await chosenFile.OpenReadAsync())
                    {
                        await bitmap.SetSourceAsync(stream);
                    }

                    this.CurrentMeTileImage = bitmap;
                }
            });
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
        /// Gets the current Band.
        /// </summary>
        /// <remarks>
        /// Should only be set in <see cref="OnNavigatedTo(NavigatedToEventArgs, Dictionary{string, object})"/>.
        /// </remarks>
        public IBand CurrentBand
        {
            get { return this.currentBand; }
            private set { this.SetProperty(ref this.currentBand, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the Me Tile image height for the original Microsoft Band.  The only applies when <see cref="CurrentBand"/> is not <see cref="HardwareRevision.Band"/>.
        /// </summary>
        public bool IsUseOriginalBandHeight
        {
            get { return this.isUseOriginalBandHeight; }
            set { this.SetProperty(ref this.isUseOriginalBandHeight, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            private set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsNotBusy
        {
            get { return !this.isBusy; }
            private set { this.SetProperty(ref this.isBusy, !value); }
        }

        /// <summary>
        /// Gets the theme colors.
        /// </summary>
        public ReadOnlyObservableCollection<ThemeColorViewModel> CurrentThemeColors { get; }

        /// <summary>
        /// Gets or sets the currently-selected Me Tile image.
        /// </summary>
        public WriteableBitmap CurrentMeTileImage
        {
            get { return this.currentMeTileImage; }
            set { this.SetProperty(ref this.currentMeTileImage, value); }
        }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="NavigatedToEventArgs"/> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <see cref="NavigatedToEventArgs.Parameter"/> of <paramref name="e"/> is <c>null</c> or is not castable to <see cref="IBand"/>.</exception>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            else if (e.Parameter == null || !(e.Parameter is IBand))
            {
                throw new ArgumentException($"The {nameof(e.Parameter)} must be an instance of {typeof(IBand)}");
            }

            this.CurrentBand = e.Parameter as IBand;

            this.RefreshPersonalizationCommand.Execute(0);
            this.RefreshPersonalizationCommand.Execute(1);
        }

        /// <summary>
        /// Refresh the dislayed personalization options to display the values for the current Band, based on the section of the pivot that is sending the command.
        /// </summary>
        /// <param name="selectedPivotIndex">The index of the active pivot view.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task RefreshPersonalization(int selectedPivotIndex)
        {
            switch (selectedPivotIndex)
            {
                case 0:
                    var themeColors = this.RgbColorThemeToCollection(await this.bandPersonalizer.GetTheme(this.CurrentBand, CancellationToken.None));
                    this.currentThemeColors.Clear();
                    if (themeColors != null && themeColors.Any())
                    {
                        foreach (var themeColor in themeColors)
                        {
                            this.currentThemeColors.Add(themeColor);
                        }
                    }

                    break;
                case 1:
                    var originalBandDimensions = HardwareRevision.Band.GetDefaultMeTileDimensions();
                    var currentMeTileImage = await this.bandPersonalizer.GetMeTileImage(this.CurrentBand, CancellationToken.None);
                    this.IsUseOriginalBandHeight = this.CurrentBand.HardwareRevision != HardwareRevision.Band
                        ? currentMeTileImage.PixelHeight <= originalBandDimensions.Height
                        : true;
                    this.CurrentMeTileImage = currentMeTileImage;
                    break;
                default:
                    throw new ArgumentNullException($"Unhandled pivot index: {selectedPivotIndex}");
            }
        }

        /// <summary>
        /// Apply the dislayed personalization options the current Band, based on the section of the pivot that is sending the command.
        /// </summary>
        /// <param name="selectedPivotIndex">The index of the active pivot view.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task ApplyPersonalization(int selectedPivotIndex)
        {
            switch (selectedPivotIndex)
            {
                case 0:
                    var newRgbColorTheme = new RgbColorTheme
                    {
                        Base = this.CurrentThemeColors[0].Swatch.ToRgbColor(),
                        HighContrast = this.CurrentThemeColors[1].Swatch.ToRgbColor(),
                        Lowlight = this.CurrentThemeColors[2].Swatch.ToRgbColor(),
                        Highlight = this.CurrentThemeColors[3].Swatch.ToRgbColor(),
                        Muted = this.CurrentThemeColors[4].Swatch.ToRgbColor(),
                        SecondaryText = this.CurrentThemeColors[5].Swatch.ToRgbColor(),
                    };

                    await this.bandPersonalizer.SetTheme(this.CurrentBand, newRgbColorTheme, CancellationToken.None);
                    break;
                case 1:
                    await this.bandPersonalizer.SetMeTileImage(this.CurrentBand, this.CurrentMeTileImage, CancellationToken.None);
                    break;
                default:
                    throw new ArgumentNullException($"Unhandled pivot index: {selectedPivotIndex}");
            }
        }

        /// <summary>
        /// Create a read-only observable collection of theme colors using the specified <paramref name="theme"/>.
        /// </summary>
        /// <param name="theme">The theme from which to get colors.</param>
        /// <returns>A read-only observable collection of theme colors.</returns>
        private IReadOnlyCollection<ThemeColorViewModel> RgbColorThemeToCollection(RgbColorTheme theme)
        {
            return new ReadOnlyCollection<ThemeColorViewModel>(new[]
            {
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Base)}TextBox/Text"),
                    Swatch = theme.Base.ToColor(),
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.HighContrast)}TextBox/Text"),
                    Swatch = theme.HighContrast.ToColor(),
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Lowlight)}TextBox/Text"),
                    Swatch = theme.Lowlight.ToColor(),
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Highlight)}TextBox/Text"),
                    Swatch = theme.Highlight.ToColor(),
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Muted)}TextBox/Text"),
                    Swatch = theme.Muted.ToColor(),
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.SecondaryText)}TextBox/Text"),
                    Swatch = theme.SecondaryText.ToColor(),
                },
            });
        }
    }
}
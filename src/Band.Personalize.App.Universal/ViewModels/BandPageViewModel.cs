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

namespace Band.Personalize.App.Universal.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Microsoft.Band;
    using Model.Library.Band;
    using Model.Library.Color;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Prism.Commands;
    using Prism.Windows.AppModel;
    using Prism.Windows.Navigation;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public class BandPageViewModel : BaseNavigationViewModel
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
        /// A value indicationg whether the <see cref="RefreshPersonalizationCommand"/> is busy for the <see cref="CurrentThemeColors"/>.
        /// </summary>
        private bool isThemeBusy;

        /// <summary>
        /// A value indicationg whether the <see cref="RefreshPersonalizationCommand"/> is busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        private bool isMeTileImageBusy;

        /// <summary>
        /// The currently-selected Me Tile image.
        /// </summary>
        private WriteableBitmap currentMeTileImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="bandPersonalizer">The Band personalizer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resourceLoader"/> or <paramref name="bandPersonalizer"/> is <c>null</c>.</exception>
        public BandPageViewModel(INavigationService navigationService, IResourceLoader resourceLoader, IBandPersonalizer bandPersonalizer)
            : base(navigationService)
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

            this.RefreshPersonalizationCommand = DelegateCommand<int>
                .FromAsyncHandler(this.RefreshPersonalizationAsync, this.NotIsPivotBusy)
                .ObservesProperty(() => this.NotIsThemeBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy);

            this.ApplyPersonalizationCommand = DelegateCommand<int>
                .FromAsyncHandler(this.ApplyPersonalizationAsync, this.NotIsPivotBusy)
                .ObservesProperty(() => this.NotIsThemeBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy);

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
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentThemeColors"/>.
        /// </summary>
        public bool IsThemeBusy
        {
            get
            {
                return this.isThemeBusy;
            }

            private set
            {
                this.SetProperty(ref this.isThemeBusy, value);
                this.OnPropertyChanged(nameof(this.NotIsThemeBusy));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy for the <see cref="CurrentMeTileImage"/>.
        /// </summary>
        public bool IsMeTileImageBusy
        {
            get
            {
                return this.isMeTileImageBusy;
            }

            private set
            {
                this.SetProperty(ref this.isMeTileImageBusy, value);
                this.OnPropertyChanged(nameof(this.NotIsMeTileImageBusy));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy <see cref="CurrentThemeColors"/>.
        /// </summary>
        public bool NotIsThemeBusy
        {
            get { return !this.IsThemeBusy; }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy <see cref="CurrentMeTileImage"/>.
        /// </summary>
        public bool NotIsMeTileImageBusy
        {
            get { return !this.IsMeTileImageBusy; }
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
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
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

            try
            {
                this.IsThemeBusy = this.IsMeTileImageBusy = true;
                await Task.WhenAll(this.BeginInvokeRefreshTheme(), this.BeginInvokeRefreshMeTileImage());
                this.IsThemeBusy = this.IsMeTileImageBusy = false;
            }
            catch (BandException)
            {
                if (this.NavigationService.CanGoBack())
                {
                    this.NavigationService.GoBack();
                }
                else
                {
                    this.NavigationService.Navigate(PageNavigationTokens.MainPage, null);
                }

                this.NavigationService.RemoveLastPage(PageNavigationTokens.BandPage, e.Parameter);

                throw;
            }
        }

        /// <summary>
        /// Refresh the dislayed personalization options to display the values for the current Band, based on the section of the pivot that is sending the command.
        /// </summary>
        /// <param name="selectedPivotIndex">The index of the active pivot view.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task RefreshPersonalizationAsync(int selectedPivotIndex)
        {
            switch (selectedPivotIndex)
            {
                case 0:
                    await this.WrapWithUiBlockWhileExecuting(b => this.IsThemeBusy = b, this.BeginInvokeRefreshTheme);
                    break;
                case 1:
                    await this.WrapWithUiBlockWhileExecuting(b => this.IsMeTileImageBusy = b, this.BeginInvokeRefreshMeTileImage);
                    break;
                default:
                    throw new ArgumentNullException($"Unhandled pivot index: {selectedPivotIndex}");
            }
        }

        /// <summary>
        /// Refresh the dislayed theme options to display the values for the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task BeginInvokeRefreshTheme()
        {
            return Task
                .Run(async () => await this.bandPersonalizer.GetTheme(this.CurrentBand, CancellationToken.None))
                .ContinueWith(t => this.UpdateTheme(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Update the UI to show the specified <paramref name="theme"/>.
        /// </summary>
        /// <param name="theme">The theme to display in the UI.</param>
        private void UpdateTheme(RgbColorTheme theme)
        {
            var themeColors = this.RgbColorThemeToCollection(theme);
            this.currentThemeColors.Clear();
            if (themeColors != null && themeColors.Any())
            {
                foreach (var themeColor in themeColors)
                {
                    this.currentThemeColors.Add(themeColor);
                }
            }
        }

        /// <summary>
        /// Refresh the dislayed Me Tile image to display the image for the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task BeginInvokeRefreshMeTileImage()
        {
            return Task
                .Run(async () => await this.bandPersonalizer.GetMeTileImage(this.CurrentBand, CancellationToken.None))
                .ContinueWith(t => this.UpdateMeTileImage(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Update the UI to show the specified <paramref name="bitmap"/> as the Me Tile image.
        /// </summary>
        /// <param name="bitmap">The Me Tile image to display in the UI.</param>
        private void UpdateMeTileImage(WriteableBitmap bitmap)
        {
            var originalBandDimensions = HardwareRevision.Band.GetDefaultMeTileDimensions();
            this.IsUseOriginalBandHeight = this.CurrentBand.HardwareRevision != HardwareRevision.Band
                ? bitmap.PixelHeight <= originalBandDimensions.Height
                : true;
            this.CurrentMeTileImage = bitmap;
        }

        /// <summary>
        /// Wrap a <see cref="Task"/> with a UI block by setting <paramref name="setBusy"/> to <c>true</c> before invoking
        /// the task and continuing with setting <paramref name="setBusy"/> to <c>false</c> when the task is completed.
        /// </summary>
        /// <param name="setBusy">An <see cref="Action{Boolean}"/> that updates a busy indicator before and after <paramref name="getAndBeginInvokeBlockingTask"/>.</param>
        /// <param name="getAndBeginInvokeBlockingTask">A function that begins invocation an action to perform while the UI is blocked.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task WrapWithUiBlockWhileExecuting(Action<bool> setBusy, Func<Task> getAndBeginInvokeBlockingTask)
        {
            if (getAndBeginInvokeBlockingTask == null)
            {
                throw new ArgumentNullException(nameof(getAndBeginInvokeBlockingTask));
            }

            setBusy(true);
            return getAndBeginInvokeBlockingTask().ContinueWith(t => setBusy(false), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Apply the dislayed personalization options the current Band, based on the section of the pivot that is sending the command.
        /// </summary>
        /// <param name="selectedPivotIndex">The index of the active pivot view.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task ApplyPersonalizationAsync(int selectedPivotIndex)
        {
            switch (selectedPivotIndex)
            {
                case 0:
                    await this.WrapWithUiBlockWhileExecuting(b => this.IsThemeBusy = b, this.BeginInvokeApplyTheme);
                    break;
                case 1:
                    await this.WrapWithUiBlockWhileExecuting(b => this.IsMeTileImageBusy = b, this.BeginInvokeApplyMeTileImage);
                    break;
                default:
                    throw new ArgumentNullException($"Unhandled pivot index: {selectedPivotIndex}");
            }
        }

        /// <summary>
        /// Apply the selected theme colors the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task BeginInvokeApplyTheme()
        {
            var newRgbColorTheme = new RgbColorTheme
            {
                Base = this.CurrentThemeColors[0].Swatch.ToRgbColor(),
                HighContrast = this.CurrentThemeColors[1].Swatch.ToRgbColor(),
                Lowlight = this.CurrentThemeColors[2].Swatch.ToRgbColor(),
                Highlight = this.CurrentThemeColors[3].Swatch.ToRgbColor(),
                Muted = this.CurrentThemeColors[4].Swatch.ToRgbColor(),
                SecondaryText = this.CurrentThemeColors[5].Swatch.ToRgbColor(),
            };

            return this.bandPersonalizer.SetTheme(this.CurrentBand, newRgbColorTheme, CancellationToken.None);
        }

        /// <summary>
        /// Apply the selected Me Tile image the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task BeginInvokeApplyMeTileImage()
        {
            return this.bandPersonalizer.SetMeTileImage(this.CurrentBand, this.CurrentMeTileImage, CancellationToken.None);
        }

        /// <summary>
        /// Get whether the specified section of the pivot is not busy.
        /// </summary>
        /// <param name="selectedPivotIndex">The index of the active pivot view.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private bool NotIsPivotBusy(int selectedPivotIndex)
        {
            switch (selectedPivotIndex)
            {
                case 0:
                    return this.NotIsThemeBusy;
                case 1:
                    return this.NotIsMeTileImageBusy;
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
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
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Model.Library.Band;
    using Model.Library.IO;
    using Model.Library.Linq;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Prism.Commands;
    using Prism.Windows.AppModel;
    using Prism.Windows.Navigation;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Provider;
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
        /// The custom theme repository.
        /// </summary>
        private readonly ICustomThemeRepository customThemeRepository;

        /// <summary>
        /// The themes available for selection.
        /// </summary>
        private readonly ObservableCollection<IGrouping<string, TitledRgbColorTheme>> availableThemes;

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
        /// The unresized Me Tile image.  This field exists so that resize operations (and associated
        /// quality loss) can be skipped if the new target size is equal to the unresize image's size.
        /// </summary>
        private WriteableBitmap unresizedCurrentMeTileImage;

        /// <summary>
        /// The status message of the most recent "Save" operation.
        /// </summary>
        private string saveStatusMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="bandPersonalizer">The Band personalizer.</param>
        /// <param name="customThemeRepository">The custom theme repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resourceLoader"/>, <paramref name="bandPersonalizer"/>, or <paramref name="customThemeRepository"/> is <c>null</c>.</exception>
        public BandPageViewModel(INavigationService navigationService, IResourceLoader resourceLoader, IBandPersonalizer bandPersonalizer, ICustomThemeRepository customThemeRepository)
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
            else if (customThemeRepository == null)
            {
                throw new ArgumentNullException(nameof(customThemeRepository));
            }

            this.resourceLoader = resourceLoader;
            this.bandPersonalizer = bandPersonalizer;
            this.customThemeRepository = customThemeRepository;

            this.AvailableThemes = new ReadOnlyObservableCollection<IGrouping<string, TitledRgbColorTheme>>(this.availableThemes = new ObservableCollection<IGrouping<string, TitledRgbColorTheme>>(new List<IGrouping<string, TitledRgbColorTheme>>
            {
                new ReadOnlyGrouping<string, TitledRgbColorTheme>(resourceLoader.GetString("HardwareRevision/Band"), DefaultThemes.Band.DefaultThemes.ToList()),
                new ReadOnlyGrouping<string, TitledRgbColorTheme>(resourceLoader.GetString("HardwareRevision/Band2"), DefaultThemes.Band2.DefaultThemes.ToList()),
            }));

            this.CurrentThemeColors = new ReadOnlyObservableCollection<ThemeColorViewModel>(this.currentThemeColors = new ObservableCollection<ThemeColorViewModel>());

            this.ChooseThemeCommand = new DelegateCommand<TitledRgbColorTheme>(this.UpdateThemeColors, t => this.NotIsThemeBusy)
                .ObservesProperty(() => this.NotIsThemeBusy);

            this.ClearValidationMessagesCommand = new DelegateCommand(() => this.SaveStatusMessage = null);

            var refreshPersonalizationCommand = new CompositeCommand();
            refreshPersonalizationCommand.RegisterCommand(this.ClearValidationMessagesCommand);
            refreshPersonalizationCommand.RegisterCommand(DelegateCommand<int>
                .FromAsyncHandler(this.RefreshPersonalizationAsync, this.NotIsPivotBusy)
                .ObservesProperty(() => this.NotIsThemeBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy));
            this.RefreshPersonalizationCommand = refreshPersonalizationCommand;

            var applyPersonalizationCommand = new CompositeCommand();
            applyPersonalizationCommand.RegisterCommand(this.ClearValidationMessagesCommand);
            applyPersonalizationCommand.RegisterCommand(DelegateCommand<int>
                .FromAsyncHandler(this.ApplyPersonalizationAsync, this.NotIsPivotBusy)
                .ObservesProperty(() => this.NotIsThemeBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy));
            this.ApplyPersonalizationCommand = applyPersonalizationCommand;

            var browserForMeTileImageCommand = new CompositeCommand();
            browserForMeTileImageCommand.RegisterCommand(this.ClearValidationMessagesCommand);
            browserForMeTileImageCommand.RegisterCommand(DelegateCommand
                .FromAsyncHandler(this.ChooseMeTileImageAsync, () => this.NotIsMeTileImageBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy));
            this.BrowserForMeTileImageCommand = browserForMeTileImageCommand;

            var saveMeTileImageCommand = new CompositeCommand();
            saveMeTileImageCommand.RegisterCommand(this.ClearValidationMessagesCommand);
            saveMeTileImageCommand.RegisterCommand(DelegateCommand
                .FromAsyncHandler(this.SaveMeTileImageAsync, () => this.NotIsMeTileImageBusy)
                .ObservesProperty(() => this.NotIsMeTileImageBusy));
            this.SaveMeTileImageCommand = saveMeTileImageCommand;

            this.RefreshAvailableThemesCommand = DelegateCommand.FromAsyncHandler(async () =>
            {
                await Task
                    .Run(async () => await this.customThemeRepository.GetThemesAsync())
                    .ContinueWith(
                        t =>
                        {
                            var key = resourceLoader.GetString("CustomThemes/Header");
                            var stale = this.availableThemes.Where(g => g.Key == key).ToList();
                            if (stale.Any())
                            {
                                foreach (var s in stale)
                                {
                                    this.availableThemes.Remove(s);
                                }
                            }

                            this.availableThemes.Add(new ReadOnlyGrouping<string, TitledRgbColorTheme>(key, t.Result.Select(theme => theme.Value).ToList()));
                        },
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            });

            this.PersistThemeCommand = DelegateCommand.FromAsyncHandler(async () =>
            {
                var currentColorTheme = this.CurrentThemeColorsToRgbColorTheme();
                await Task.Run(async () => await this.customThemeRepository.PersistThemeAsync(currentColorTheme));
            });

            this.PropertyChanged += this.OnIsUseOriginalBandHeightChanged;
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
        /// Gets all currently available themes.
        /// </summary>
        public ReadOnlyObservableCollection<IGrouping<string, TitledRgbColorTheme>> AvailableThemes { get; }

        /// <summary>
        /// Gets the theme colors.
        /// </summary>
        public ReadOnlyObservableCollection<ThemeColorViewModel> CurrentThemeColors { get; }

        /// <summary>
        /// Gets the status message of the most recent "Save" operation.
        /// </summary>
        public string SaveStatusMessage
        {
            get { return this.saveStatusMessage; }
            private set { this.SetProperty(ref this.saveStatusMessage, value); }
        }

        /// <summary>
        /// Gets the currently-selected Me Tile image.
        /// </summary>
        public WriteableBitmap CurrentMeTileImage
        {
            get { return this.currentMeTileImage; }
            private set { this.SetProperty(ref this.currentMeTileImage, value); }
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
            catch (Exception)
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
        /// Gets the default filename for saving a Me Tile image, based on the name of the <see cref="CurrentBand"/>.
        /// </summary>
        /// <returns>The default save filename for a Me Tile image.</returns>
        private string GetDefaultSaveFileName()
        {
            return string.Format(this.resourceLoader.GetString("DefaultSaveFileNameFormat"), this.CurrentBand.Name.Trim().StripInvalidFileNameCharacters().ReplaceWhiteSpaceWithDash());
        }

        /// <summary>
        /// Represents the method that will handle the <see cref="INotifyPropertyChanged.PropertyChanged"/> event raised when the <see cref="IsUseOriginalBandHeight"/> property is changed on this component.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
        private void OnIsUseOriginalBandHeightChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == nameof(this.IsUseOriginalBandHeight))
            {
                var dimensions = (this.IsUseOriginalBandHeight
                    ? HardwareRevision.Band
                    : this.CurrentBand.HardwareRevision).GetDefaultMeTileDimensions();

                if (dimensions.Width != this.CurrentMeTileImage.PixelWidth || dimensions.Height != this.CurrentMeTileImage.PixelHeight)
                {
                    this.CurrentMeTileImage = dimensions.Width == this.unresizedCurrentMeTileImage.PixelWidth && dimensions.Height == this.unresizedCurrentMeTileImage.PixelHeight
                        ? this.unresizedCurrentMeTileImage
                        : this.unresizedCurrentMeTileImage.Resize(dimensions.Width, dimensions.Height, WriteableBitmapExtensions.Interpolation.Bilinear);
                }
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
                .ContinueWith(t => this.UpdateThemeColors(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Update the UI to show the specified <paramref name="theme"/>.
        /// </summary>
        /// <param name="theme">The theme to display in the UI.</param>
        private void UpdateThemeColors(RgbColorTheme theme)
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
                .ContinueWith(t => this.UpdateCurrentMeTileImage(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Update the UI to show the specified <paramref name="bitmap"/> as the Me Tile image.
        /// </summary>
        /// <param name="bitmap">The Me Tile image to display in the UI.</param>
        private void UpdateCurrentMeTileImage(WriteableBitmap bitmap)
        {
            this.unresizedCurrentMeTileImage = bitmap;
            this.CurrentMeTileImage = bitmap;

            var originalBandDimensions = HardwareRevision.Band.GetDefaultMeTileDimensions();
            this.IsUseOriginalBandHeight = this.CurrentBand.HardwareRevision != HardwareRevision.Band
                ? bitmap.PixelHeight <= originalBandDimensions.Height
                : true;
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
            var newRgbColorTheme = this.CurrentThemeColorsToRgbColorTheme();

            return Task.Run(async () => await this.bandPersonalizer.SetTheme(this.CurrentBand, newRgbColorTheme, CancellationToken.None));
        }

        /// <summary>
        /// Apply the selected Me Tile image the current Band.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private Task BeginInvokeApplyMeTileImage()
        {
            return Task
                .Run(async () => await this.bandPersonalizer.SetMeTileImage(this.CurrentBand, this.CurrentMeTileImage, CancellationToken.None))
                .ContinueWith(t => this.unresizedCurrentMeTileImage = this.CurrentMeTileImage, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Choose a Me Tile image using a <see cref="FileOpenPicker"/> and all file types allowed by the
        /// installed bitmap decoders as described by <see cref="BitmapDecoder.GetDecoderInformationEnumerator()"/>.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task ChooseMeTileImageAsync()
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                ViewMode = PickerViewMode.Thumbnail,
            };

            var extensions = BitmapDecoder
                .GetDecoderInformationEnumerator()
                .SelectMany(e => e.FileExtensions)
                .Distinct()
                .ToList();
            foreach (var extension in extensions)
            {
                picker.FileTypeFilter.Add(extension);
            }

            var chosenFile = await picker.PickSingleFileAsync();
            if (chosenFile != null)
            {
                WriteableBitmap bitmap;
                using (var stream = await chosenFile.OpenReadAsync())
                {
                    bitmap = await WriteableBitmapExtensions.FromStream(null, stream);
                }

                this.unresizedCurrentMeTileImage = bitmap;

                var dimensions = this.IsUseOriginalBandHeight
                    ? HardwareRevision.Band.GetDefaultMeTileDimensions()
                    : this.CurrentBand.HardwareRevision.GetDefaultMeTileDimensions();

                this.CurrentMeTileImage = dimensions.Width == bitmap.PixelWidth && dimensions.Height == bitmap.PixelHeight
                    ? bitmap
                    : bitmap.Resize(dimensions.Width, dimensions.Height, WriteableBitmapExtensions.Interpolation.Bilinear);
            }
        }

        /// <summary>
        /// Save a Me Tile image using a <see cref="FileSavePicker"/> and allowing all file types allowed by the
        /// installed bitmap encoders as described by <see cref="BitmapEncoder.GetEncoderInformationEnumerator()"/>.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task SaveMeTileImageAsync()
        {
            var picker = new FileSavePicker
            {
                SuggestedFileName = this.GetDefaultSaveFileName(),
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };

            var encoders = BitmapEncoder.GetEncoderInformationEnumerator();
            foreach (var encoder in encoders)
            {
                picker.FileTypeChoices.Add(encoder.FriendlyName, encoder.FileExtensions.ToList());
            }

            var extensions = encoders
                .SelectMany(e => e.FileExtensions)
                .Distinct()
                .ToList();
            if (extensions.Any())
            {
                picker.DefaultFileExtension = extensions.FirstOrDefault(ext => string.Equals(ext, ".png")) ?? extensions.First();
            }

            var chosenFile = await picker.PickSaveFileAsync();
            if (chosenFile != null)
            {
                var chosenEncoder = encoders.FirstOrDefault(e => e.MimeTypes.Contains(chosenFile.ContentType, StringComparer.OrdinalIgnoreCase) && e.FileExtensions.Contains(chosenFile.FileType, StringComparer.OrdinalIgnoreCase));
                if (chosenEncoder == null)
                {
                    throw new Exception(string.Format(this.resourceLoader.GetString("NoSuchEncoderExceptionMessage"), chosenFile.ContentType, chosenFile.FileType));
                }

                CachedFileManager.DeferUpdates(chosenFile);
                using (var stream = await chosenFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    await this.CurrentMeTileImage.ToStream(stream, chosenEncoder.CodecId);
                    await stream.FlushAsync();
                }

                var status = await CachedFileManager.CompleteUpdatesAsync(chosenFile);
                switch (status)
                {
                    case FileUpdateStatus.Complete:
                    case FileUpdateStatus.CompleteAndRenamed:
                        break;
                    default:
                        this.SaveStatusMessage = string.Format(this.resourceLoader.GetString("MeTileImageSaveFailedMessage"), chosenFile.DisplayName);
                        break;
                }
            }
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
                    Swatch = theme.Base,
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.HighContrast)}TextBox/Text"),
                    Swatch = theme.HighContrast,
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Lowlight)}TextBox/Text"),
                    Swatch = theme.Lowlight,
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Highlight)}TextBox/Text"),
                    Swatch = theme.Highlight,
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.Muted)}TextBox/Text"),
                    Swatch = theme.Muted,
                },
                new ThemeColorViewModel
                {
                    Title = this.resourceLoader.GetString($"{nameof(theme.SecondaryText)}TextBox/Text"),
                    Swatch = theme.SecondaryText,
                },
            });
        }

        /// <summary>
        /// Create a theme using the current theme colors.
        /// </summary>
        /// <returns>A color theme.</returns>
        private TitledRgbColorTheme CurrentThemeColorsToRgbColorTheme()
        {
            return new TitledRgbColorTheme
            {
                Title = "Sample", // TODO: how to set title with UI? - prompt for name if unnamed?  Save/Save As... with flyout?
                Base = this.CurrentThemeColors[0].Swatch,
                HighContrast = this.CurrentThemeColors[1].Swatch,
                Lowlight = this.CurrentThemeColors[2].Swatch,
                Highlight = this.CurrentThemeColors[3].Swatch,
                Muted = this.CurrentThemeColors[4].Swatch,
                SecondaryText = this.CurrentThemeColors[5].Swatch,
            };
        }
    }
}
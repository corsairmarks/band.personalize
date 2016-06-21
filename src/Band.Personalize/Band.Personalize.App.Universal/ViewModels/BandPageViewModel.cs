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
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Prism.Commands;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;
    using Windows.ApplicationModel;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public class BandPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The Band personalizer.
        /// </summary>
        private readonly IBandPersonalizer bandPersonalizer;

        /// <summary>
        /// The current Band.
        /// </summary>
        private IBand currentBand;

        /// <summary>
        /// A value indicationg whether the <see cref="RefreshPersonalizationDataCommand"/> is busy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// The current theme.
        /// </summary>
        private RgbColorTheme currentTheme;

        /// <summary>
        /// The currently-selected Me Tile image.
        /// </summary>
        private BitmapSource currentMeTileImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandPageViewModel"/> class.
        /// </summary>
        /// <param name="bandPersonalizer">The Band personalizer.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bandPersonalizer"/> is <c>null</c>.</exception>
        public BandPageViewModel(IBandPersonalizer bandPersonalizer)
        {
            if (bandPersonalizer == null)
            {
                throw new ArgumentNullException(nameof(bandPersonalizer));
            }

            this.bandPersonalizer = bandPersonalizer;

            var refreshPersonalizationDataCommand = new CompositeCommand();
            refreshPersonalizationDataCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = true, () => this.IsBusy));
            refreshPersonalizationDataCommand.RegisterCommand(DelegateCommand<PivotItem>.FromAsyncHandler(async pi => this.CurrentTheme = await this.bandPersonalizer.GetTheme())); // TODO: pivot conditions
            refreshPersonalizationDataCommand.RegisterCommand(DelegateCommand<PivotItem>.FromAsyncHandler(async pi => this.CurrentMeTileImage = await this.bandPersonalizer.GetMeTileImage())); // TODO
            refreshPersonalizationDataCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = false));
            this.RefreshPersonalizationDataCommand = refreshPersonalizationDataCommand;

            this.ApplyThemeCommand = DelegateCommand.FromAsyncHandler(async () => await this.bandPersonalizer.SetTheme(this.CurrentTheme), () => this.CurrentTheme != null);

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
                    var bitmap = new BitmapImage();
                    bitmap.DecodePixelWidth = 310;
                    bitmap.DecodePixelHeight = 128; // TODO: probably get from a control on the page which size to use
                    using (var stream = await chosenFile.OpenReadAsync())
                    {
                        await bitmap.SetSourceAsync(stream);
                    }

                    this.CurrentMeTileImage = bitmap;
                }
            });

            this.ApplyMeTileImageCommand = DelegateCommand.FromAsyncHandler(async () => await this.bandPersonalizer.SetMeTileImage(null, HardwareRevision.Band2), () => this.CurrentMeTileImage != null); // TODO
        }

        /// <summary>
        /// Gets the "Refresh" command
        /// </summary>
        public ICommand RefreshPersonalizationDataCommand { get; }

        /// <summary>
        /// Gets the "Apply" command for them theme.
        /// </summary>
        public ICommand ApplyThemeCommand { get; }

        /// <summary>
        /// Gets the "Browser" command for the Me Tile image.
        /// </summary>
        public ICommand BrowserForMeTileImageCommand { get; }

        /// <summary>
        /// Gets the "Apply" command for the Me Tile image.
        /// </summary>
        public ICommand ApplyMeTileImageCommand { get; }

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
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            private set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public RgbColorTheme CurrentTheme
        {
            get { return this.currentTheme; }
            private set { this.SetProperty(ref this.currentTheme, value); }
        }

        /// <summary>
        /// Gets or sets the currently-selected Me Tile image.
        /// </summary>
        public BitmapSource CurrentMeTileImage
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

            this.RefreshPersonalizationDataCommand.Execute(null);
        }
    }
}
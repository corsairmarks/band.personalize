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
    using Model.Library.Repository;
    using Prism.Commands;
    using Prism.Windows.Navigation;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public class MainPageViewModel : BaseNavigationViewModel, IDisposable
    {
        /// <summary>
        /// The cached Band repository.
        /// </summary>
        private readonly ICachedRepository<IBandRepository> bandRepository;

        /// <summary>
        /// A read-only collection of paired Bands.
        /// </summary>
        private readonly ObservableCollection<IBand> pairedBands;

        /// <summary>
        /// A value indicationg whether the <see cref="RefreshPairedBandsCommand"/> is busy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// A semaphore to control access to <see cref="refreshCancellationTokenSource"/>.
        /// </summary>
        private SemaphoreSlim refreshCancellationTokenSourceSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// A source for <see cref="CancellationToken"/> instances used when executing a "Refresh" operation.
        /// </summary>
        private CancellationTokenSource refreshCancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="bandRepository">The cached Band repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bandRepository"/> is <c>null</c>.</exception>
        public MainPageViewModel(INavigationService navigationService, ICachedRepository<IBandRepository> bandRepository)
            : base(navigationService)
        {
            if (bandRepository == null)
            {
                throw new ArgumentNullException(nameof(bandRepository));
            }

            this.bandRepository = bandRepository;
            this.pairedBands = new ObservableCollection<IBand>();
            this.PairedBands = new ReadOnlyObservableCollection<IBand>(this.pairedBands);

            this.CancelRefreshPairedBandsCommand = DelegateCommand.FromAsyncHandler(
                async () =>
                {
                    await this
                        .CancelRefreshPairedBands()
                        .ContinueWith(t => this.IsBusy = false, TaskScheduler.FromCurrentSynchronizationContext());
                },
                () => this.IsBusy)
                .ObservesProperty(() => this.IsBusy);

            this.RefreshPairedBandsCommand = DelegateCommand.FromAsyncHandler(async () =>
            {
                await this
                    .CancelRefreshPairedBands()
                    .ContinueWith(
                        async t =>
                        {
                            this.bandRepository.Clear();
                            await this.RefreshPairedBands();
                        },
                        TaskScheduler.FromCurrentSynchronizationContext());
            });

            this.NavigateToBandPageCommand = new DelegateCommand<string>(band => this.NavigationService.Navigate(PageNavigationTokens.BandPage, band));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        ~MainPageViewModel()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the "Refresh" command.
        /// </summary>
        public ICommand RefreshPairedBandsCommand { get; }

        /// <summary>
        /// Gets the "Cancel" command.
        /// </summary>
        public ICommand CancelRefreshPairedBandsCommand { get; }

        /// <summary>
        /// Gets the "Band Page" navigation command.
        /// </summary>
        public ICommand NavigateToBandPageCommand { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            private set
            {
                this.SetProperty(ref this.isBusy, value);
                this.OnPropertyChanged(nameof(this.NotIsBusy));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is not busy.
        /// </summary>
        public bool NotIsBusy
        {
            get { return !this.IsBusy; }
        }

        /// <summary>
        /// Gets a read-only collection of paired Microsoft Bands.
        /// </summary>
        public ReadOnlyObservableCollection<IBand> PairedBands { get; }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="NavigatedToEventArgs"/> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="e"/> is <c>null</c>.</exception>
        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            // TODO: store a list of previously queried bands for this session.  Maybe ICachingBandRepository of some sort?
            if (!this.PairedBands.Any())
            {
                await this.RefreshPairedBands();
            }
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
                if (this.refreshCancellationTokenSource != null)
                {
                    this.refreshCancellationTokenSource.Cancel();
                    this.refreshCancellationTokenSource.Dispose();
                    this.refreshCancellationTokenSource = null;
                }

                if (this.refreshCancellationTokenSourceSemaphore != null)
                {
                    this.refreshCancellationTokenSourceSemaphore.Dispose();
                    this.refreshCancellationTokenSourceSemaphore = null;
                }
            }
        }

        /// <summary>
        /// Refreshes the list of paired Bands.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task RefreshPairedBands()
        {
            CancellationToken token;
            await this.EnterRefreshSemaphore(() =>
            {
                if (this.refreshCancellationTokenSource == null)
                {
                    this.refreshCancellationTokenSource = new CancellationTokenSource();
                }

                token = this.refreshCancellationTokenSource.Token;
            });

            this.IsBusy = true;
            await this.bandRepository.Repository
                .GetPairedBandsAsync(token)
                .ContinueWith(t => this.UpdatePairedBands(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext())
                .ContinueWith(t => this.IsBusy = false, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Update the UI to show the specified <paramref name="bands"/>.
        /// </summary>
        /// <param name="bands">The Bands to display in the UI.</param>
        private void UpdatePairedBands(IReadOnlyList<IBand> bands)
        {
            if (bands != null)
            {
                this.pairedBands.Clear();
                foreach (var band in bands)
                {
                    this.pairedBands.Add(band);
                }
            }
        }

        /// <summary>
        /// Cancels the current "Refresh" operation, if one is in progress.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task CancelRefreshPairedBands()
        {
            await this.EnterRefreshSemaphore(() =>
            {
                if (this.refreshCancellationTokenSource != null)
                {
                    this.refreshCancellationTokenSource.Cancel();
                    this.refreshCancellationTokenSource.Dispose();
                    this.refreshCancellationTokenSource = null;
                }
            });
        }

        /// <summary>
        /// Enter a work section protected by the <see cref="refreshCancellationTokenSourceSemaphore"/>.
        /// </summary>
        /// <param name="action">The work to complete.</param>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task EnterRefreshSemaphore(Action action)
        {
            await this.refreshCancellationTokenSourceSemaphore.WaitAsync();

            try
            {
                action();
            }
            finally
            {
                this.refreshCancellationTokenSourceSemaphore.Release();
            }
        }
    }
}
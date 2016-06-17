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
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Prism.Commands;
    using Prism.Windows.Mvvm;

    /// <summary>
    /// The View Model for the Main Page.
    /// </summary>
    public class MainPageViewModel : ViewModelBase, IDisposable
    {
        /// <summary>
        /// The Band repository.
        /// </summary>
        private readonly IBandRepository bandRepository;

        /// <summary>
        /// A read-only collection of connected Bands.
        /// </summary>
        private IReadOnlyList<IBand> connectedBands;

        /// <summary>
        /// A value indicationg whether the <see cref="RefreshConnectedBandsCommand"/> is busy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// A semaphore to control access to <see cref="refreshCancellationTokenSource"/>.
        /// </summary>
        private SemaphoreSlim refreshCancellationTokenSourceSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// A source for <see cref="CancellationToken"/> instances used when executing a "Refresh" operation.
        /// </summary>
        private CancellationTokenSource refreshCancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="bandRepository">The Band repository.</param>
        public MainPageViewModel(IBandRepository bandRepository)
        {
            if (bandRepository == null)
            {
                throw new ArgumentNullException(nameof(bandRepository));
            }

            this.bandRepository = bandRepository;

            var cancelRefreshConnectedBandsCommand = new CompositeCommand();
            cancelRefreshConnectedBandsCommand.RegisterCommand(DelegateCommand.FromAsyncHandler(this.CancelRefreshConnectedBands, () => this.IsBusy));
            cancelRefreshConnectedBandsCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = false));
            this.CancelRefreshConnectedBandsCommand = cancelRefreshConnectedBandsCommand;

            var refreshConnectedBandsCommand = new CompositeCommand();
            refreshConnectedBandsCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = true));
            refreshConnectedBandsCommand.RegisterCommand(DelegateCommand.FromAsyncHandler(this.CancelRefreshConnectedBands));
            refreshConnectedBandsCommand.RegisterCommand(DelegateCommand.FromAsyncHandler(async () => this.ConnectedBands = await this.RefreshConnectedBands()));
            refreshConnectedBandsCommand.RegisterCommand(new DelegateCommand(() => this.IsBusy = false));
            this.RefreshConnectedBandsCommand = refreshConnectedBandsCommand;
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
        public ICommand RefreshConnectedBandsCommand { get; }

        /// <summary>
        /// Gets the "Cancel" command.
        /// </summary>
        public ICommand CancelRefreshConnectedBandsCommand { get; }

        /// <summary>
        /// Gets a value indicating whether the "Refresh" command is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            private set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets a read-only collection of connected Microsoft Bands.
        /// </summary>
        public IReadOnlyList<IBand> ConnectedBands
        {
            get { return this.connectedBands; }
            private set { this.SetProperty(ref this.connectedBands, value); }
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
        /// Refreshes the list of connected Bands.
        /// </summary>
        /// <returns>An asynchronous task that returns a read-only collection of connected Bands when it completes.</returns>
        private async Task<IReadOnlyList<IBand>> RefreshConnectedBands()
        {
            CancellationToken cancellationToken;

            await this.EnterRefreshSemaphore(() =>
            {
                if (this.refreshCancellationTokenSource == null)
                {
                    this.refreshCancellationTokenSource = new CancellationTokenSource();
                }

                cancellationToken = this.refreshCancellationTokenSource.Token;
            });

            return await this.bandRepository.GetBands(cancellationToken);
        }

        /// <summary>
        /// Cancels the current "Refresh" operation, if one is in progress.
        /// </summary>
        /// <returns>An asynchronous task that returns when work is complete.</returns>
        private async Task CancelRefreshConnectedBands()
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
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

namespace Band.Personalize.App.Universal.ViewModels.Design
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Prism.Windows.Navigation;

    /// <summary>
    /// The design View Model for the Main Page.
    /// </summary>
    public class DesignMainPageViewModel : MainPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignMainPageViewModel"/> class.
        /// </summary>
        public DesignMainPageViewModel()
            : base(NavigationServiceStub.Instance, BandRepositoryStub.Instance)
        {
            this.RefreshConnectedBandsCommand.Execute(null);
        }

        /// <summary>
        /// A stub for a fake <see cref="INavigationService"/>.
        /// </summary>
        private class NavigationServiceStub : INavigationService
        {
            /// <summary>
            /// The lazy-initialized singleton instanxe of <see cref="NavigationServiceStub"/>.
            /// </summary>
            private static readonly Lazy<NavigationServiceStub> LazyInstance = new Lazy<NavigationServiceStub>(() => new NavigationServiceStub());

            /// <summary>
            /// Initializes a new instance of the <see cref="NavigationServiceStub"/> class.
            /// </summary>
            private NavigationServiceStub()
            {
            }

            /// <summary>
            /// Gets the singleton instance of <see cref="NavigationServiceStub"/>.
            /// </summary>
            public static INavigationService Instance
            {
                get { return LazyInstance.Value; }
            }

            public bool CanGoBack()
            {
                throw new NotImplementedException();
            }

            public bool CanGoForward()
            {
                throw new NotImplementedException();
            }

            public void ClearHistory()
            {
                throw new NotImplementedException();
            }

            public void GoBack()
            {
                throw new NotImplementedException();
            }

            public void GoForward()
            {
                throw new NotImplementedException();
            }

            public bool Navigate(string pageToken, object parameter)
            {
                throw new NotImplementedException();
            }

            public void RemoveAllPages(string pageToken = null, object parameter = null)
            {
                throw new NotImplementedException();
            }

            public void RemoveFirstPage(string pageToken = null, object parameter = null)
            {
                throw new NotImplementedException();
            }

            public void RemoveLastPage(string pageToken = null, object parameter = null)
            {
                throw new NotImplementedException();
            }

            public void RestoreSavedNavigation()
            {
                throw new NotImplementedException();
            }

            public void Suspending()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// A stub for a fake <see cref="IBandRepository"/>.
        /// </summary>
        private class BandRepositoryStub : IBandRepository
        {
            /// <summary>
            /// Sample Band data for design.
            /// </summary>
            private static readonly IReadOnlyList<IBand> DefaultBands = new ReadOnlyCollection<IBand>(new List<IBand>
            {
                new BandStub
                {
                    Name = "Sample Band",
                    ConnectionType = ConnectionType.Bluetooth,
                    HardwareRevision = HardwareRevision.Band,
                    HardwareVersion = 3,
                },
                new BandStub
                {
                    Name = "Sample Band 2",
                    ConnectionType = ConnectionType.Usb,
                    HardwareRevision = HardwareRevision.Band2,
                    HardwareVersion = 20,
                },
                new BandStub
                {
                    Name = "Sample Unknown Band",
                    ConnectionType = ConnectionType.Unknown,
                    HardwareRevision = HardwareRevision.Band2,
                    HardwareVersion = 400,
                },
            });

            /// <summary>
            /// The lazy-initialized singleton instanxe of <see cref="BandRepositoryStub"/>.
            /// </summary>
            private static readonly Lazy<BandRepositoryStub> LazyInstance = new Lazy<BandRepositoryStub>(() => new BandRepositoryStub());

            /// <summary>
            /// Initializes a new instance of the <see cref="BandRepositoryStub"/> class.
            /// </summary>
            private BandRepositoryStub()
            {
            }

            /// <summary>
            /// Gets the singleton instance of <see cref="BandRepositoryStub"/>.
            /// </summary>
            public static IBandRepository Instance
            {
                get { return LazyInstance.Value; }
            }

            #region IBandRepository Members

            /// <summary>
            /// Gets information about all Microsoft Bands connected to the application host.
            /// </summary>
            /// <returns>An asynchronous task that returns a read-only collection of connected Bands when it completes.</returns>
            public async Task<IReadOnlyList<IBand>> GetBands()
            {
                return await this.GetBands(CancellationToken.None);
            }

            /// <summary>
            /// Gets information about all Microsoft Bands connected to the application host.
            /// </summary>
            /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
            /// <returns>An asynchronous task that returns a read-only collection of connected Bands when it completes.</returns>
            public async Task<IReadOnlyList<IBand>> GetBands(CancellationToken token)
            {
                return await Task.FromResult(DefaultBands);
            }

            #endregion

            /// <summary>
            /// A stub for fake <see cref="IBand"/> data.
            /// </summary>
            private class BandStub : IBand
            {
                /// <summary>
                /// Gets or sets the name.
                /// </summary>
                public string Name { get; set; }

                /// <summary>
                /// Gets or sets the hardware major revision level.
                /// </summary>
                public ConnectionType ConnectionType { get; set; }

                /// <summary>
                /// Gets or sets the actual hardware version.
                /// </summary>
                public HardwareRevision HardwareRevision { get; set; }

                /// <summary>
                /// Gets or sets the connection type between the application host and the Microsoft Band.
                /// </summary>
                public int HardwareVersion { get; set; }
            }
        }
    }
}
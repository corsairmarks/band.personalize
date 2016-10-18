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
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Model.Library.Band;

    /// <summary>
    /// The design View Model for the Main Page.
    /// </summary>
    public class DesignMainPageViewModel : IMainPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignMainPageViewModel"/> class.
        /// </summary>
        public DesignMainPageViewModel()
        {
            // TODO: PairedBands
            this.PairedBands = null;
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
        public bool IsBusy { get; }

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
    }
}
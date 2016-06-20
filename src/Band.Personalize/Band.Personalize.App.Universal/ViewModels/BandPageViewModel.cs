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
    using Model.Library.Band;
    using Model.Library.Repository;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;

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
        /// The Band being personalized.
        /// </summary>
        /// <remarks>
        /// Should only be set in <see cref="OnNavigatedTo(NavigatedToEventArgs, Dictionary{string, object})"/>.
        /// </remarks>
        private IBand band;

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
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            else if (e.Parameter == null || !(e.Parameter is IBand))
            {
                throw new ArgumentException($"The {nameof(e.Parameter)} must be an instance of {typeof(IBand)}");
            }

            this.band = e.Parameter as IBand;
        }
    }
}
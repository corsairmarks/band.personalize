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
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;

    /// <summary>
    /// The base View Model for the pages that can trigger navigation.
    /// </summary>
    public abstract class BaseNavigationViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNavigationViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <exception cref="ArgumentNullException"><paramref name="navigationService"/> is <c>null</c>.</exception>
        public BaseNavigationViewModel(INavigationService navigationService)
            : base()
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException(nameof(navigationService));
            }

            this.NavigationService = navigationService;
        }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        protected INavigationService NavigationService { get; }
    }
}
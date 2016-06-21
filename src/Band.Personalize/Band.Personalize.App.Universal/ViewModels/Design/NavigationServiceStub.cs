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
    using Prism.Windows.Navigation;

    /// <summary>
    /// A stub for a fake <see cref="INavigationService"/>.
    /// </summary>
    internal class NavigationServiceStub : INavigationService
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

        /// <summary>
        /// Determines whether the navigation service can navigate to the previous page or not.
        /// </summary>
        /// <returns><c>true</c> if the navigation service can go back; otherwise, <c>false</c>.</returns>
        public bool CanGoBack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the navigation service can navigate to the next page or not.
        /// </summary>
        /// <returns><c>true</c> if the navigation service can go forward; otherwise, <c>false</c>.</returns>
        public bool CanGoForward()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the navigation history.
        /// </summary>
        public void ClearHistory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes to the previous page in the navigation stack.
        /// </summary>
        public void GoBack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes to the next page in the navigation stack.
        /// </summary>
        public void GoForward()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Navigates to the page with the specified page token, passing the specified <paramref name="parameter"/>.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>Returns <c>true</c> if navigation succeeds; otherwise, <c>false</c>.</returns>
        public bool Navigate(string pageToken, object parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the all pages of the backstack with optional <paramref name="pageToken"/> and <paramref name="parameter"/>.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <param name="parameter">The parameter.</param>
        public void RemoveAllPages(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the first page of the backstack with optional <paramref name="pageToken"/> and <paramref name="parameter"/>.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <param name="parameter">The parameter.</param>
        public void RemoveFirstPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the last page of the backstack with optional <paramref name="pageToken"/> and <paramref name="parameter"/>.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <param name="parameter">The parameter.</param>
        public void RemoveLastPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Restores the saved navigation.
        /// </summary>
        public void RestoreSavedNavigation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used for navigating away from the current view model due to a suspension event, in this way you can execute additional logic to handle suspensions.
        /// </summary>
        public void Suspending()
        {
            throw new NotImplementedException();
        }
    }
}
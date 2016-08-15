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

namespace Band.Personalize.App.Universal.Behaviors
{
    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// An action that calls <see cref="FlyoutBase.ShowAttachedFlyout(FrameworkElement)"/> method show the attached flyout.
    /// </summary>
    public class OpenFlyoutAction : DependencyObject, IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that is passed to the action by the behavior. Generally this is <see cref="IBehavior.AssociatedObject"/> or a target object.</param>
        /// <param name="parameter">The value of this parameter is determined by the caller.</param>
        /// <returns>Returns the result of the action.</returns>
        public object Execute(object sender, object parameter)
        {
            var element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }

            return null;
        }
    }
}
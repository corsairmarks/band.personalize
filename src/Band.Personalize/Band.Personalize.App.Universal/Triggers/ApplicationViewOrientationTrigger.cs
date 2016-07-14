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

namespace Band.Personalize.App.Universal.Triggers
{
    using System;
    using Windows.ApplicationModel;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;

    /// <summary>
    /// A custom visual state trigger based on device orientation.
    /// </summary>
    public class ApplicationViewOrientationTrigger : StateTriggerBase, IDisposable
    {
        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty ApplicationViewOrientationProperty = DependencyProperty.Register(
            nameof(ApplicationViewOrientation),
            typeof(ApplicationViewOrientation),
            typeof(ApplicationViewOrientationTrigger),
            new PropertyMetadata(ApplicationViewOrientation.Portrait));

        /// <summary>
        /// The application view relevant to this trigger.
        /// </summary>
        private readonly ApplicationView applicationView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewOrientationTrigger"/> class.
        /// </summary>
        public ApplicationViewOrientationTrigger()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                this.applicationView = ApplicationView.GetForCurrentView();
                this.applicationView.VisibleBoundsChanged += this.OnOrientationChanged;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ApplicationViewOrientationTrigger"/> class.
        /// </summary>
        ~ApplicationViewOrientationTrigger()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public ApplicationViewOrientation ApplicationViewOrientation
        {
            get { return (ApplicationViewOrientation)this.GetValue(ApplicationViewOrientationProperty); }
            set { this.SetValue(ApplicationViewOrientationProperty, value); }
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
        /// <param name="isDisposing">Whether this object is currently being disposed.</param>
        protected void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (this.applicationView != null)
                {
                    this.applicationView.VisibleBoundsChanged -= this.OnOrientationChanged;
                }
            }
        }

        /// <summary>
        /// Represents a method that handles general events.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data. If there is no event data, this parameter will be null.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is <c>null</c>.</exception>
        private void OnOrientationChanged(ApplicationView sender, object args)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            this.SetActive(this.ApplicationViewOrientation == sender.Orientation);
        }
    }
}
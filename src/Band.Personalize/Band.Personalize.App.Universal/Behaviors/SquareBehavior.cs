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
    using System;
    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// A behavior for <see cref="FrameworkElement"/> objects that sets the <see cref="FrameworkElement.Height"/>
    /// or <see cref="FrameworkElement.Width"/> property based on the Actual dimension in the opposite orientation.
    /// </summary>
    public class SquareBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Scale orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty ScalableOrientationProperty = DependencyProperty.Register(
            nameof(ScalableOrientation),
            typeof(Orientation),
            typeof(SquareBehavior),
            new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// Gets or sets the assumed scalable (resibalbe, fluid) dimension of the square object.  <see cref="Orientation.Horizontal"/>
        /// implies the <see cref="FrameworkElement.ActualWidth"/> property determines the <see cref="FrameworkElement.Height"/>, where
        /// <see cref="Orientation.Vertical"/> implies the <see cref="FrameworkElement.ActualHeight"/> property determines the
        /// <see cref="FrameworkElement.Width"/>.
        /// </summary>
        public Orientation ScalableOrientation
        {
            get { return (Orientation)this.GetValue(ScalableOrientationProperty); }
            set { this.SetValue(ScalableOrientationProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to the <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SizeChanged += this.OnSizeChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SizeChanged -= this.OnSizeChanged;
        }

        /// <summary>
        /// Represents the method that will handle the SizeChanged event.
        /// See also <seealso cref="SizeChangedEventHandler"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var senderFrameworkElement = sender as FrameworkElement;
            if (senderFrameworkElement != null && e != null && e.NewSize != e.PreviousSize)
            {
                switch (this.ScalableOrientation)
                {
                    case Orientation.Horizontal:
                        senderFrameworkElement.Height = senderFrameworkElement.ActualWidth;
                        break;
                    case Orientation.Vertical:
                        senderFrameworkElement.Width = senderFrameworkElement.ActualHeight;
                        break;
                    default:
                        throw new NotImplementedException($"Unhandled {typeof(Orientation)}: \"{this.ScalableOrientation}\"");
                }
            }
        }
    }
}
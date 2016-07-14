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
            new PropertyMetadata(Orientation.Horizontal, OnScalableOrientationChanged));

        /// <summary>
        /// Gets or sets the assumed scalable (resizable, fluid) dimension of the square object.  <see cref="Orientation.Horizontal"/>
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
            ScaleObject(this.AssociatedObject, this.ScalableOrientation);
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SizeChanged -= this.OnSizeChanged;
            this.AssociatedObject.Width = double.NaN;
            this.AssociatedObject.Height = double.NaN;
        }

        /// <summary>
        /// Scale the <paramref name="sender"/> as a square based on the <see cref="ScalableOrientation"/>.
        /// </summary>
        /// <param name="sender">The object requesting a square scale.</param>
        /// <param name="scalableOrientation">The orientation that is fluid.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sender"/> is <c>null</c>.</exception>
        /// <exception cref="NotImplementedException"><see cref="ScalableOrientation"/> is an unhandled value of <see cref="Orientation"/>.</exception>
        private static void ScaleObject(FrameworkElement sender, Orientation scalableOrientation)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            switch (scalableOrientation)
            {
                case Orientation.Horizontal:
                    sender.Height = sender.ActualWidth;
                    break;
                case Orientation.Vertical:
                    sender.Width = sender.ActualHeight;
                    break;
                default:
                    throw new NotImplementedException($"Unhandled {typeof(Orientation)}: \"{scalableOrientation}\"");
            }
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ScalableOrientationProperty"/> dependency property changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        /// <exception cref="NotImplementedException"><see cref="ScalableOrientation"/> is an unhandled value of <see cref="Orientation"/>.</exception>
        private static void OnScalableOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = d as SquareBehavior;
            if (that != null && e != null && that.AssociatedObject != null)
            {
                var newScalableOrientation = (Orientation)e.NewValue;
                switch (newScalableOrientation)
                {
                    case Orientation.Horizontal:
                        that.AssociatedObject.Width = double.NaN;
                        break;
                    case Orientation.Vertical:
                        that.AssociatedObject.Height = double.NaN;
                        break;
                    default:
                        throw new NotImplementedException($"Unhandled {typeof(Orientation)}: \"{newScalableOrientation}\"");
                }
            }
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
                ScaleObject(senderFrameworkElement, this.ScalableOrientation);
            }
        }
    }
}
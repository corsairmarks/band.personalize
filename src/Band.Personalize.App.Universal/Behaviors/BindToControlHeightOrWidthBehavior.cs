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

    /// <summary>
    /// A behavior for any <see cref="FrameworkElement"/> the binds its <see cref="FrameworkElement.Height"/> and <see cref="FrameworkElement.Width"/> properties to
    /// another <see cref="FrameworkElement"/> identified by the <see cref="SourceObject"/> property.
    /// </summary>
    public class BindToControlHeightOrWidthBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty UseHeightProperty = DependencyProperty.Register(
            nameof(UseHeight),
            typeof(bool),
            typeof(BindToControlHeightOrWidthBehavior),
            new PropertyMetadata(true));

        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty UseWidthProperty = DependencyProperty.Register(
            nameof(UseWidth),
            typeof(bool),
            typeof(BindToControlHeightOrWidthBehavior),
            new PropertyMetadata(true));

        /// <summary>
        /// Source object dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register(
            nameof(SourceObject),
            typeof(FrameworkElement),
            typeof(BindToControlHeightOrWidthBehavior),
            null);

        /// <summary>
        /// Scale factory dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.Register(
            nameof(ScaleFactor),
            typeof(double),
            typeof(BindToControlHeightOrWidthBehavior),
            new PropertyMetadata(1D, OnScaleFactorChanged));

        /// <summary>
        /// Gets or sets a value indicating whether to use the height from the <see cref="SourceObject"/>.
        /// </summary>
        public bool UseHeight
        {
            get { return (bool)this.GetValue(UseHeightProperty); }
            set { this.SetValue(UseHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the width from the <see cref="SourceObject"/>.
        /// </summary>
        public bool UseWidth
        {
            get { return (bool)this.GetValue(UseWidthProperty); }
            set { this.SetValue(UseWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source object to which to bind the <see cref="FrameworkElement.Height"/> and/or
        /// <see cref="FrameworkElement.Width"/> of the <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        public FrameworkElement SourceObject
        {
            get { return (FrameworkElement)this.GetValue(SourceObjectProperty); }
            set { this.SetValue(SourceObjectProperty, value); }
        }

        /// <summary>
        /// Gets or sets the amount to scale the <see cref="FrameworkElement.Height"/> and <see cref="FrameworkElement.Width"/> of <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        public double ScaleFactor
        {
            get { return (double)this.GetValue(ScaleFactorProperty); }
            set { this.SetValue(ScaleFactorProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to the <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.SourceObject != null)
            {
                this.SourceObject.SizeChanged += this.SourceObject_OnSizeChanged;
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="Behavior{FrameworkElement}.AssociatedObject"/>.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.SourceObject != null)
            {
                this.SourceObject.SizeChanged -= this.SourceObject_OnSizeChanged;
            }
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ScaleFactorProperty"/> dependency property changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnScaleFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (BindToControlHeightOrWidthBehavior)d;
            if (that != null && e != null && (double)e.NewValue < 0)
            {
                that.ScaleFactor = (double)e.OldValue;
            }
        }

        /// <summary>
        /// Represents the method that will handle the SizeChanged event.
        /// See also <seealso cref="SizeChangedEventHandler"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void SourceObject_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e != null)
            {
                if (this.UseHeight)
                {
                    this.AssociatedObject.Height = Math.Floor(e.NewSize.Height * this.ScaleFactor);
                }

                if (this.UseWidth)
                {
                    this.AssociatedObject.Width = Math.Floor(e.NewSize.Width * this.ScaleFactor);
                }
            }
        }
    }
}
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

namespace Band.Personalize.App.Universal.Controls
{
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
    /// <summary>
    /// A custom <see cref="UserControl"/> for picking a <see cref="Windows.UI.Color"/> with a confirm-able and cancel-able flyout.
    /// </summary>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
    public sealed partial class ColorPickerFlyoutUserControl : BindableUserControl
    {
        /// <summary>
        /// Color dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorOuterProperty = DependencyProperty.Register(
            nameof(ColorOuter),
            typeof(Color),
            typeof(ColorPickerFlyoutUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(OnColorPropertyChanged)));

        /// <summary>
        /// The picker color.
        /// </summary>
        private Color pickerColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerFlyoutUserControl"/> class.
        /// </summary>
        public ColorPickerFlyoutUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the dependency color.
        /// </summary>
        public Color ColorOuter
        {
            get { return (Color)this.GetValue(ColorOuterProperty); }
            set { this.SetValue(ColorOuterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the picker color.
        /// </summary>
        public Color PickerColor
        {
            get { return this.pickerColor; }
            set { this.SetProperty(ref this.pickerColor, value); }
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ColorOuterProperty"/> dependency property changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        /// <remarks>Guards internal state by only triggering property changes on actually changed values.</remarks>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = d as ColorPickerFlyoutUserControl;
            if (that != null && e != null && e.NewValue != null && e.NewValue is Color)
            {
                var newColor = (Color)e.NewValue;
                that.PickerColor = newColor;
            }
        }

        /// <summary>
        /// Represents the method that will handle routed events for the "Confirm" button's <see cref="ButtonBase.Click"/> event.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.ColorOuter = this.PickerColor;
            this.TheFlyout.Hide();
        }

        /// <summary>
        /// Represents the method that will handle routed events for the "Cancel" button's <see cref="ButtonBase.Click"/> event.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.TheFlyout.Hide();
        }

        /// <summary>
        /// Represents the method that will handle the <see cref="FlyoutBase.Opening"/> event when the event provides data.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void TheFlyout_Opening(object sender, object e)
        {
            this.PickerColor = this.ColorOuter; // TODO: should this remember state or clear?  for now, the data resets when opened
        }
    }
}
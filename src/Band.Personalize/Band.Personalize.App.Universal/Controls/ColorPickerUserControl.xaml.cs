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
    using System;
    using System.Linq;
    using Model.Library.Color;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

#pragma warning disable CS0419 // Ambiguous reference in cref attribute
    /// <summary>
    /// A custom <see cref="UserControl"/> for picking a <see cref="Windows.UI.Color"/>.
    /// </summary>
#pragma warning restore CS0419 // Ambiguous reference in cref attribute
    public partial class ColorPickerUserControl : BindableUserControl
    {
        /// <summary>
        /// Alpha dependency property.
        /// </summary>
        public static readonly DependencyProperty UseAlphaProperty = DependencyProperty.Register(
            nameof(UseAlpha),
            typeof(bool),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(true));

        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// Color dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color),
            typeof(Color),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(null, new PropertyChangedCallback(OnColorPropertyChanged)));

        /// <summary>
        /// The x-axis location of the picker target.
        /// </summary>
        private double pointX;

        /// <summary>
        /// The y-axis location of the picker target.
        /// </summary>
        private double pointY;

        /// <summary>
        /// The hue amount (used for the visual picker).
        /// </summary>
        private double hue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerUserControl"/> class.
        /// </summary>
        public ColorPickerUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display the alpha controls.
        /// </summary>
        public bool UseAlpha
        {
            get { return (bool)this.GetValue(UseAlphaProperty); }
            set { this.SetValue(UseAlphaProperty, value); }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the location of the picker target.
        /// </summary>
        public double PointX
        {
            get { return this.pointX; }
            set { this.SetProperty(ref this.pointX, value); }
        }

        /// <summary>
        /// Gets or sets the location of the picker target.
        /// </summary>
        public double PointY
        {
            get { return this.pointY; }
            set { this.SetProperty(ref this.pointY, value); }
        }

        /// <summary>
        /// Gets or sets the dependency color.
        /// </summary>
        public Color Color
        {
            get { return (Color)this.GetValue(ColorProperty); }
            set { this.SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Gets the starting alpha gradient color.
        /// </summary>
        public Color AlphaStartColor
        {
            get
            {
                return new Color
                {
                    A = byte.MinValue,
                    R = this.Red,
                    G = this.Green,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the ending alpha gradient color.
        /// </summary>
        public Color AlphaEndColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = this.Red,
                    G = this.Green,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the starting red gradient color.
        /// </summary>
        public Color RedStartColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = byte.MinValue,
                    G = this.Green,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the ending red gradient color.
        /// </summary>
        public Color RedEndColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = byte.MaxValue,
                    G = this.Green,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the starting green gradient color.
        /// </summary>
        public Color GreenStartColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = this.Red,
                    G = byte.MinValue,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the ending green gradient color.
        /// </summary>
        public Color GreenEndColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = this.Red,
                    G = byte.MaxValue,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the starting blue gradient color.
        /// </summary>
        public Color BlueStartColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = this.Red,
                    G = this.Green,
                    B = byte.MinValue,
                };
            }
        }

        /// <summary>
        /// Gets the ending blue gradient color.
        /// </summary>
        public Color BlueEndColor
        {
            get
            {
                return new Color
                {
                    A = byte.MaxValue,
                    R = this.Red,
                    G = this.Green,
                    B = byte.MaxValue,
                };
            }
        }

        /// <summary>
        /// Gets the current color with alpha or not as appropriate base on <see cref="UseAlpha"/>.
        /// </summary>
        public Color SwatchColor
        {
            get
            {
                return new Color
                {
                    // TODO: is this right place?
                    A = this.UseAlpha
                        ? this.Color.A
                        : byte.MaxValue,
                    R = this.Red,
                    G = this.Green,
                    B = this.Blue,
                };
            }
        }

        /// <summary>
        /// Gets the hue color.
        /// </summary>
        public Color HueColor
        {
            get { return new RgbColor(this.Hue, 1, 1).ToColor(); }
        }

        /// <summary>
        /// Gets or sets the hue amount (used for the visual picker).
        /// </summary>
        public double Hue
        {
            get
            {
                return this.hue;
            }

            set
            {
                this.hue = value;
                var current = this.Color.ToRgbColor();
                var updated = new RgbColor(value, current.Saturation, current.Value).ToColor();
                updated.A = this.Alpha;
                this.Color = updated;
            }
        }

        /// <summary>
        /// Gets or sets the alpha channel.
        /// </summary>
        public byte Alpha
        {
            get
            {
                return this.Color.A;
            }

            set
            {
                var updateColor = this.Color;
                updateColor.A = value;
                this.Color = updateColor;
            }
        }

        /// <summary>
        /// Gets or sets the red channel.
        /// </summary>
        public byte Red
        {
            get
            {
                return this.Color.R;
            }

            set
            {
                var updateColor = this.Color;
                updateColor.R = value;
                this.Color = updateColor;
            }
        }

        /// <summary>
        /// Gets or sets the green channel.
        /// </summary>
        public byte Green
        {
            get
            {
                return this.Color.G;
            }

            set
            {
                var updateColor = this.Color;
                updateColor.G = value;
                this.Color = updateColor;
            }
        }

        /// <summary>
        /// Gets or sets the blue channel.
        /// </summary>
        public byte Blue
        {
            get
            {
                return this.Color.B;
            }

            set
            {
                var updateColor = this.Color;
                updateColor.B = value;
                this.Color = updateColor;
            }
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="UserControl"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="UserControl"/>.</returns>
        public override string ToString()
        {
            return $"#{this.Alpha:X2}{this.Red:X2}{this.Green:X2}{this.Blue:X2}";
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ColorProperty"/> dependency property changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = d as ColorPickerUserControl;
            if (that != null)
            {
                that.OnColorChannelChanged();
            }
        }

        /// <summary>
        /// A custom event handler for when any color channel (or alpha) is changed.
        /// </summary>
        private void OnColorChannelChanged()
        {
            this.OnPropertyChanged(nameof(this.Alpha));
            this.OnPropertyChanged(nameof(this.Red));
            this.OnPropertyChanged(nameof(this.Green));
            this.OnPropertyChanged(nameof(this.Blue));
            this.OnPropertyChanged(nameof(this.AlphaStartColor));
            this.OnPropertyChanged(nameof(this.AlphaEndColor));
            this.OnPropertyChanged(nameof(this.RedStartColor));
            this.OnPropertyChanged(nameof(this.RedEndColor));
            this.OnPropertyChanged(nameof(this.GreenStartColor));
            this.OnPropertyChanged(nameof(this.GreenEndColor));
            this.OnPropertyChanged(nameof(this.BlueStartColor));
            this.OnPropertyChanged(nameof(this.BlueEndColor));
            this.OnPropertyChanged(nameof(this.SwatchColor));
            this.UpdatePointAndHue();
        }

        private void UpdatePointAndHue()
        {
            var color = this.SwatchColor.ToRgbColor();
            this.UpdatePoint(color);
            this.UpdateHue(color);
        }

        private void UpdatePoint(RgbColor color = null)
        {
            if (color == null)
            {
                color = this.SwatchColor.ToRgbColor();
            }

            this.PointX = this.PickerCanvas.ActualWidth * color.Saturation;
            this.PointY = this.PickerCanvas.ActualHeight * (1 - color.Value);
        }

        private void UpdateHue(RgbColor color = null)
        {
            if (color == null)
            {
                color = this.SwatchColor.ToRgbColor();
            }

            this.hue = color.Hue;
            this.OnPropertyChanged(nameof(this.Hue));
            this.OnPropertyChanged(nameof(this.HueColor));
        }

        /// <summary>
        /// A <see cref="PointerEventHandler"/> for the <see cref="UIElement.PointerPressed"/> event of <see cref="PickerCanvas"/>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Event data for the event.</param>
        private void PickerCanvas_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null && (uiElement.PointerCaptures == null || !uiElement.PointerCaptures.Any()))
            {
                this.PickColor(e.GetCurrentPoint(uiElement).Position);
                if (uiElement.CapturePointer(e.Pointer))
                {
                    uiElement.PointerMoved += this.PickerCanvas_OnPointerMoved;
                }

                e.Handled = true;
            }
        }

        /// <summary>
        /// A <see cref="PointerEventHandler"/> for the <see cref="UIElement.PointerReleased"/> event of <see cref="PickerCanvas"/>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Event data for the event.</param>
        private void PickerCanvas_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                this.PickColor(e.GetCurrentPoint(uiElement).Position);
                uiElement.ReleasePointerCaptures();
                uiElement.PointerMoved -= this.PickerCanvas_OnPointerMoved;
                e.Handled = true;
            }
        }

        /// <summary>
        /// A <see cref="PointerEventHandler"/> for the <see cref="UIElement.PointerMoved"/> event of <see cref="PickerCanvas"/>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Event data for the event.</param>
        private void PickerCanvas_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                this.PickColor(e.GetCurrentPoint(uiElement).Position);
                e.Handled = true;
            }
        }

        /// <summary>
        /// A <see cref="PointerEventHandler"/> for the <see cref="UIElement.PointerCaptureLost"/> event of <see cref="PickerCanvas"/>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Event data for the event.</param>
        private void PickerCanvas_OnPointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                uiElement.PointerMoved -= this.PickerCanvas_OnPointerMoved;
                e.Handled = true;
            }
        }

        /// <summary>
        /// A <see cref="PointerEventHandler"/> for the <see cref="UIElement.PointerCanceled"/> event of <see cref="PickerCanvas"/>.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">Event data for the event.</param>
        private void PickerCanvas_OnPointerCancelled(object sender, PointerRoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                uiElement.PointerMoved -= this.PickerCanvas_OnPointerMoved;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Choose a color from the <see cref="PickerCanvas"/> based on pointer location.
        /// </summary>
        /// <param name="point">The location of the pointer.</param>
        private void PickColor(Point point)
        {
            this.PointX = Math.Min(this.PickerCanvas.ActualWidth, Math.Max(0, point.X));
            this.PointY = Math.Min(this.PickerCanvas.ActualHeight, Math.Max(0, point.Y));

            var updated = new RgbColor(this.Hue, this.PointX / this.PickerCanvas.ActualWidth, 1 - (this.PointY / this.PickerCanvas.ActualHeight)).ToColor();
            updated.A = this.Color.A;
            this.Color = updated;
        }
    }
}
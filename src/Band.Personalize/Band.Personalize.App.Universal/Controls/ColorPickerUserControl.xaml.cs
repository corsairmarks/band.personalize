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
            "UseAlpha",
            typeof(bool),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(true, OnUseAlphaChanged));

        /// <summary>
        /// Orientation dependency property.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation",
            typeof(Orientation),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));

        /// <summary>
        /// Color dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Color),
            typeof(ColorPickerUserControl),
            new PropertyMetadata(null, OnColorChanged));

        /// <summary>
        /// The x-axis location of the picker target.
        /// </summary>
        private double pointX = 150;

        /// <summary>
        /// The y-axis location of the picker target.
        /// </summary>
        private double pointY;

        /// <summary>
        /// The alpha channel.
        /// </summary>
        private byte alpha;

        /// <summary>
        /// The red color channel.
        /// </summary>
        private byte red;

        /// <summary>
        /// The green color channel.
        /// </summary>
        private byte green;

        /// <summary>
        /// The blue color channel.
        /// </summary>
        private byte blue;

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
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }

            set
            {
                this.SetValue(OrientationProperty, value);
                this.OnPropertyChanged(() => this.OppositeOrientation);
            }
        }

        /// <summary>
        /// Gets the opposite orientation.
        /// </summary>
        public Orientation OppositeOrientation
        {
            get
            {
                return this.Orientation == Orientation.Horizontal
                    ? Orientation.Vertical
                    : Orientation.Horizontal;
            }
        }

        /// <summary>
        /// Gets or sets the location of the picker target.
        /// </summary>
        public Point Point
        {
            get
            {
                return new Point
                {
                    X = this.PointX,
                    Y = this.PointY,
                };
            }

            set
            {
                this.PointX = value.X;
                this.PointY = value.Y;
            }
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
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return new Color
                {
                    A = this.Alpha,
                    R = this.Red,
                    G = this.Green,
                    B = this.Blue,
                };
            }

            set
            {
                this.Alpha = value.A;
                this.Red = value.R;
                this.Green = value.G;
                this.Blue = value.B;
            }
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
        /// Gets the hue (with full alpha) color.
        /// </summary>
        public Color OpaqueColor
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
        /// Gets or sets the hue amount (used for the visual picker).
        /// </summary>
        public double Hue
        {
            get { return this.hue; }
            set { this.SetProperty(ref this.hue, value); }
        }

        /// <summary>
        /// Gets or sets the alpha channel.
        /// </summary>
        public byte Alpha
        {
            get
            {
                return this.alpha;
            }

            set
            {
                this.SetProperty(ref this.alpha, value);
                this.OnColorChanged();
            }
        }

        /// <summary>
        /// Gets or sets the red channel.
        /// </summary>
        public byte Red
        {
            get
            {
                return this.red;
            }

            set
            {
                this.SetProperty(ref this.red, value);
                this.OnColorChanged();
            }
        }

        /// <summary>
        /// Gets or sets the green channel.
        /// </summary>
        public byte Green
        {
            get
            {
                return this.green;
            }

            set
            {
                this.SetProperty(ref this.green, value);
                this.OnColorChanged();
            }
        }

        /// <summary>
        /// Gets or sets the blue channel.
        /// </summary>
        public byte Blue
        {
            get
            {
                return this.blue;
            }

            set
            {
                this.SetProperty(ref this.blue, value);
                this.OnColorChanged();
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
        /// <see cref="UseAlphaProperty"/> changed event handler.
        /// </summary>
        /// <param name="d">dependency object</param>
        /// <param name="e">event argument</param>
        private static void OnUseAlphaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as ColorPickerUserControl;
            if (panel == null)
            {
                return;
            }

            if (!(e.NewValue is bool))
            {
                panel.UseAlpha = (bool)e.OldValue;
            }
        }

        /// <summary>
        /// <see cref="UseAlphaProperty"/> changed event handler.
        /// </summary>
        /// <param name="d">dependency object</param>
        /// <param name="e">event argument</param>
        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as ColorPickerUserControl;
            if (panel == null)
            {
                return;
            }

            if (!(e.NewValue is Orientation))
            {
                panel.Orientation = (Orientation)e.OldValue;
            }
        }

        /// <summary>
        /// <see cref="Color"/> changed event handler
        /// </summary>
        /// <param name="d">dependency object</param>
        /// <param name="e">event argument</param>
        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as ColorPickerUserControl;
            if (panel == null)
            {
                return;
            }

            if (!(e.NewValue is Color))
            {
                panel.Color = (Color)e.OldValue;
            }
        }

        /// <summary>
        /// Notifies all event listeners that are affected when the color changes.
        /// </summary>
        private void OnColorChanged()
        {
            this.OnPropertyChanged(() => this.Color);
            this.SetValue(ColorProperty, this.Color);
            this.OnPropertyChanged(() => this.AlphaStartColor);
            this.OnPropertyChanged(() => this.AlphaEndColor);
            this.OnPropertyChanged(() => this.RedStartColor);
            this.OnPropertyChanged(() => this.RedEndColor);
            this.OnPropertyChanged(() => this.GreenStartColor);
            this.OnPropertyChanged(() => this.GreenEndColor);
            this.OnPropertyChanged(() => this.BlueStartColor);
            this.OnPropertyChanged(() => this.BlueEndColor);
            this.OnPropertyChanged(() => this.OpaqueColor);
            this.OnPropertyChanged(() => this.Hue);
            this.OnPropertyChanged(() => this.Point);
        }

        /// <summary>
        /// Picker area pointer pressed event handler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event aruments</param>
        private void OnPickerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.PickColor(e.GetCurrentPoint(this.PickerCanvas).Position);
            this.PickerCanvas.CapturePointer(e.Pointer);

            PointerEventHandler moved = null;
            moved = (s, args) =>
            {
                this.PickColor(args.GetCurrentPoint(this.PickerCanvas).Position);
            };
            PointerEventHandler released = null;
            released = (s, args) =>
            {
                this.PickerCanvas.ReleasePointerCapture(args.Pointer);
                this.PickColor(args.GetCurrentPoint(this.PickerCanvas).Position);
                this.PickerCanvas.PointerMoved -= moved;
                this.PickerCanvas.PointerReleased -= released;
            };

            this.PickerCanvas.PointerMoved += moved;
            this.PickerCanvas.PointerReleased += released;
        }

        /// <summary>
        /// Handler for changing the picker point. TODO: remove, should be bound to the x/y coords.
        /// </summary>
        private void OnPickPointChanged()
        {
            var updated = new RgbColor(this.Hue, this.PointX / this.PickerCanvas.Width, 1 - (this.PointY / this.PickerCanvas.Width)).ToColor();
            updated.A = this.OpaqueColor.A;
            this.Color = updated;
        }

        /// <summary>
        /// Pick color
        /// </summary>
        /// <param name="point">pick point</param>
        private void PickColor(Point point)
        {
            var px = Math.Min(this.PickerCanvas.ActualWidth, Math.Max(0, point.X));
            var py = Math.Min(this.PickerCanvas.ActualHeight, Math.Max(0, point.Y));

            this.Point = new Point
            {
                X = Math.Round(px, MidpointRounding.AwayFromZero),
                Y = Math.Round(py, MidpointRounding.AwayFromZero),
            };

            this.OnPickPointChanged();
        }
    }
}
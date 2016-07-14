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
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// A custom <see cref="UserControl"/> for picking a single color (or alpha) channel of an ARGB color.
    /// </summary>
    public sealed partial class RgbColorChannelUserControl : BindableUserControl
    {
        /// <summary>
        /// Header dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(RgbColorChannelUserControl),
            null);

        /// <summary>
        /// Channel dependency property.
        /// </summary>
        public static readonly DependencyProperty ChannelProperty = DependencyProperty.Register(
            nameof(Channel),
            typeof(byte),
            typeof(RgbColorChannelUserControl),
            new PropertyMetadata(byte.MinValue, new PropertyChangedCallback(OnChannelPropertyChanged)));

        /// <summary>
        /// End color dependency property.
        /// </summary>
        public static readonly DependencyProperty SliderTrackBackgroundProperty = DependencyProperty.Register(
            nameof(SliderTrackBackground),
            typeof(Brush),
            typeof(RgbColorChannelUserControl),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// A regular expression defining the allowable format for a hexadecimal 16-bit color channel string.
        /// </summary>
        private static readonly Regex ByteHexFormat = new Regex("^\\s*[0-9a-f]{2}\\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The byte color channel string.
        /// </summary>
        private string byteString = byte.MinValue.ToString();

        /// <summary>
        /// Initializes a new instance of the <see cref="RgbColorChannelUserControl"/> class.
        /// </summary>
        public RgbColorChannelUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        public string Header
        {
            get { return (string)this.GetValue(HeaderProperty); }
            set { this.SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the channel value.
        /// </summary>
        public byte Channel
        {
            get { return (byte)this.GetValue(ChannelProperty); }
            set { this.SetValue(ChannelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brush for the slider track background.
        /// </summary>
        public Brush SliderTrackBackground
        {
            get { return (Brush)this.GetValue(SliderTrackBackgroundProperty); }
            set { this.SetValue(SliderTrackBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the byte color channel string.
        /// </summary>
        public string ByteString
        {
            get
            {
                return this.byteString;
            }

            set
            {
                this.SetProperty(ref this.byteString, value);
                if (value != null)
                {
                    if (ByteHexFormat.IsMatch(value))
                    {
                        this.Channel = byte.Parse(value.Trim(), NumberStyles.HexNumber);
                    }
                    else
                    {
                        byte result;
                        if (byte.TryParse(value, out result))
                        {
                            this.Channel = result;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ChannelProperty"/> dependency property changes.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnChannelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = d as RgbColorChannelUserControl;
            if (that != null && e != null && e.NewValue != null && e.NewValue != e.OldValue)
            {
                that.SetProperty(ref that.byteString, e.NewValue.ToString(), nameof(that.ByteString));
            }
        }
    }
}
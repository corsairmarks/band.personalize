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

    /// <summary>
    /// A custom <see cref="UserControl"/> for picking a single color (or alpha) channel of an ARGB color.
    /// </summary>
    public sealed partial class RgbColorChannelUserControl : UserControl
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
            null);

        /// <summary>
        /// Start color dependency property.
        /// </summary>
        public static readonly DependencyProperty StartColorProperty = DependencyProperty.Register(
            nameof(StartColor),
            typeof(Color),
            typeof(RgbColorChannelUserControl),
            null);

        /// <summary>
        /// End color dependency property.
        /// </summary>
        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register(
            nameof(EndColor),
            typeof(Color),
            typeof(RgbColorChannelUserControl),
            null);

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
        /// Gets or sets the starting gradient color of the control.
        /// </summary>
        public Color StartColor
        {
            get { return (Color)this.GetValue(StartColorProperty); }
            set { this.SetValue(StartColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the starting gradient color of the control.
        /// </summary>
        public Color EndColor
        {
            get { return (Color)this.GetValue(EndColorProperty); }
            set { this.SetValue(EndColorProperty, value); }
        }
    }
}
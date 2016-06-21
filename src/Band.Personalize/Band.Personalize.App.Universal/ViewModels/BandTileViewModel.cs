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

namespace Band.Personalize.App.Universal.ViewModels
{
    using System;
    using Model.Library.Band;
    using Prism.Windows.AppModel;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// The View Model for a Band tile.
    /// </summary>
    public class BandTileViewModel
    {
        /// <summary>
        /// Gets the character for Bluetooth in the Segoe MDL2 Assets font.
        /// </summary>
        public const Symbol Bluetooth = (Symbol)0xE702; // or EC41

        /// <summary>
        /// The character for a USB cord in the Segoe MDL2 Assets font.
        /// </summary>
        public const Symbol Usb = (Symbol)0xECF0;

        /// <summary>
        /// The character for a question mark in the Segoe MDL2 Assets font.
        /// </summary>
        public const Symbol QuestionMark = (Symbol)0xE897; // or E11B

        /// <summary>
        /// The resource loader.
        /// </summary>
        private readonly IResourceLoader resourceLoader;

        /// <summary>
        /// The band.
        /// </summary>
        private readonly IBand band;

        /// <summary>
        /// Initializes a new instance of the <see cref="BandTileViewModel"/> class.
        /// </summary>
        /// <param name="resourceLoader">The resource loader.</param>
        /// <param name="band">The Band.</param>
        public BandTileViewModel(IResourceLoader resourceLoader, IBand band)
        {
            if (resourceLoader == null)
            {
                throw new ArgumentNullException(nameof(resourceLoader));
            }
            else if (band == null)
            {
                throw new ArgumentNullException(nameof(band));
            }

            this.resourceLoader = resourceLoader;
            this.band = band;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this.band.Name; }
        }

        /// <summary>
        /// Gets the hardware major revision level.
        /// </summary>
        public string HardwareString
        {
            get { return $"{this.resourceLoader.GetString($"HardwareRevision.{this.band.HardwareRevision}")} ({this.band.HardwareVersion})"; }
        }

        /// <summary>
        /// Gets the connection type between the application host and the Microsoft Band.
        /// </summary>
        public Symbol ConnectionSymbol
        {
            get
            {
                switch (this.band.ConnectionType)
                {
                    case ConnectionType.Usb:
                        return Usb;
                    case ConnectionType.Bluetooth:
                        return Bluetooth;
                    default:
                        return QuestionMark;
                }
            }
        }
    }
}
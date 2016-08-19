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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Model.Library.Color;
    using Model.Library.Repository;
    using Model.Library.Theme;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Windows.AppModel;
    using Windows.UI.Popups;

    /// <summary>
    /// The View Model for a titled theme.
    /// </summary>
    public class TitledThemeViewModel : BindableBase
    {
        /// <summary>
        /// The theme title.
        /// </summary>
        private string title;

        /// <summary>
        /// The base Start Strip color, used as the default for tiles.
        /// </summary>
        private RgbColor @base;

        /// <summary>
        /// The high contrast Start Strip color, used for highlighted tiles (i.e., new content alerts).
        /// </summary>
        private RgbColor highContrast;

        /// <summary>
        /// The lowlight Start Strip color, used for "pressed" tiles.
        /// </summary>
        private RgbColor lowlight;

        /// <summary>
        /// The in-tile/in-app header color.
        /// </summary>
        private RgbColor highlight;

        /// <summary>
        /// The in-tile/in-app muted color, used for the achievement marker background.
        /// </summary>
        private RgbColor muted;

        /// <summary>
        /// A color that is either the in-tile/in-app secondary header text color (original Band) or the
        /// toggle button "On" state (Band 2).  The Band 2 refers to this color as "Medium" instead of "Secondary Text,"
        /// which itself is a static, predefined color.
        /// </summary>
        private RgbColor secondaryText;

        /// <summary>
        /// Gets or sets the theme title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets the base Start Strip color, used as the default for tiles.
        /// </summary>
        public RgbColor Base
        {
            get { return this.@base; }
            set { this.SetProperty(ref this.@base, value); }
        }

        /// <summary>
        /// Gets or sets the high contrast Start Strip color, used for highlighted tiles (i.e., new content alerts).
        /// </summary>
        public RgbColor HighContrast
        {
            get { return this.highContrast; }
            set { this.SetProperty(ref this.highContrast, value); }
        }

        /// <summary>
        /// Gets or sets the lowlight Start Strip color, used for "pressed" tiles.
        /// </summary>
        public RgbColor Lowlight
        {
            get { return this.lowlight; }
            set { this.SetProperty(ref this.lowlight, value); }
        }

        /// <summary>
        /// Gets or sets the in-tile/in-app header color.
        /// </summary>
        public RgbColor Highlight
        {
            get { return this.highlight; }
            set { this.SetProperty(ref this.highlight, value); }
        }

        /// <summary>
        /// Gets or sets the in-tile/in-app muted color, used for the achievement marker background.
        /// </summary>
        public RgbColor Muted
        {
            get { return this.muted; }
            set { this.SetProperty(ref this.muted, value); }
        }

        /// <summary>
        /// Gets or sets a color that is either the in-tile/in-app secondary header text color (original Band) or the
        /// toggle button "On" state (Band 2).  The Band 2 refers to this color as "Medium" instead of "Secondary Text,"
        /// which itself is a static, predefined color.
        /// </summary>
        public RgbColor SecondaryText
        {
            get { return this.secondaryText; }
            set { this.SetProperty(ref this.secondaryText, value); }
        }

        /// <summary>
        /// Create a shallow clone of this instance.
        /// </summary>
        /// <returns>A new instance of <see cref="TitledThemeViewModel"/> with the same data as this instance.</returns>
        public virtual TitledThemeViewModel ShallowClone()
        {
            return new TitledThemeViewModel
            {
                Title = this.Title,
                Base = this.Base,
                HighContrast = this.HighContrast,
                Lowlight = this.Lowlight,
                Highlight = this.Highlight,
                Muted = this.Muted,
                SecondaryText = this.SecondaryText,
            };
        }
    }
}
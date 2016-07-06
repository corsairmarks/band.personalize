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
    using Prism.Mvvm;
    using Windows.UI;

    /// <summary>
    /// A titled color swatch for editing.
    /// </summary>
    public class ThemeColorViewModel : BindableBase
    {
        /// <summary>
        /// The title of the color swatch.
        /// </summary>
        private string title;

        /// <summary>
        /// The editable color swatch.
        /// </summary>
        private Color swatch;

        /// <summary>
        /// Gets or sets the title of the color swatch.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets the editable color swatch.
        /// </summary>
        public Color Swatch
        {
            get { return this.swatch; }
            set { this.SetProperty(ref this.swatch, value); }
        }
    }
}
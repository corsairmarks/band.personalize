﻿// Copyright 2016 Nicholas Butcher
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

namespace Band.Personalize.Model.Library.Theme
{
    /// <summary>
    /// A six-color theme for use on the Microsoft Band, with a title.
    /// </summary>
    public class TitledRgbColorTheme : RgbColorTheme
    {
        /// <summary>
        /// Gets or sets the theme title.
        /// </summary>
        public string Title { get; set; }
    }
}
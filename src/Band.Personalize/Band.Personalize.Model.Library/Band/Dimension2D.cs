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

namespace Band.Personalize.Model.Library.Band
{
    /// <summary>
    /// Represents immutable dimensions on a two-dimensional plane.
    /// </summary>
    public class Dimension2D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension2D"/> class.
        /// </summary>
        /// <param name="width">The width to be represented by this dimension.</param>
        /// <param name="height">The height to be represented by this dimension.</param>
        public Dimension2D(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Returns a <see cref="bool"/> that indicates whether <paramref name="obj"/> is equal to this instance.
        /// Equal <see cref="Dimension2D"/> instances have equal values for <see cref="Width"/> and <see cref="Height"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test for equality.</param>
        /// <returns><c>true</c> if the <paramref name="obj"/> is equal to this instance, otherwise <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj != null && obj is Dimension2D)
            {
                var other = obj as Dimension2D;
                return this.Width == other.Width && this.Height == other.Height;
            }

            return base.Equals(obj);
        }

        /// /// <summary>
        /// Serves as a hash function.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Dimension2D"/>.</returns>
        public override int GetHashCode()
        {
            var hash = ((long)17 * this.Width) + this.Height;
            return hash.GetHashCode();
        }

        // TODO: unit tests for Equals and GetHashCode
    }
}
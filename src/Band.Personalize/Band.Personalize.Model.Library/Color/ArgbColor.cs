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

namespace Band.Personalize.Model.Library.Color
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A class representing a 16-bit-per-channel ARGB color, with supporting methods for manipulating the color.
    /// </summary>
    public class ArgbColor : RgbColor
    {
        /// <summary>
        /// A regular expression defining the allowable format for a hexadecimal ARGB color string.
        /// </summary>
        private static readonly Regex HexadecimalStringPattern = new Regex("^\\s*#?[0-9a-f]{8}\\s*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgbColor"/> class.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation.</param>
        /// <param name="red">The red channel color saturation.</param>
        /// <param name="green">The green channel color saturation.</param>
        /// <param name="blue">The blue channel color saturation.</param>
        public ArgbColor(byte alpha, byte red, byte green, byte blue)
            : base(red, green, blue)
        {
            this.Alpha = alpha;
        }

        /// <summary>
        /// Gets the green channel color saturation.
        /// </summary>
        public byte Alpha { get; }

        /// <summary>
        /// Gets the percentage of maximum saturation of the green color channel.
        /// </summary>
        public decimal AlphaSaturation
        {
            get
            {
                return PercentageOfMaximumSaturation(this.Alpha);
            }
        }

        /// <summary>
        /// Operator overload for equality (==).
        /// </summary>
        /// <param name="lhs">The left-hand operand of the operator.</param>
        /// <param name="rhs">The right-hand operand of the operator.</param>
        /// <returns><c>true</c> if <paramref name="lhs"/> and <paramref name="rhs"/> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ArgbColor lhs, ArgbColor rhs)
        {
            var isLhsNull = (object)lhs == null;
            var isRhsNull = (object)rhs == null;
            if (isLhsNull && isRhsNull)
            {
                return true;
            }
            else if (!isLhsNull && !isRhsNull)
            {
                return lhs.Equals(rhs);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Operator overload for inequality (!=).
        /// </summary>
        /// <param name="lhs">The left-hand operand of the operator.</param>
        /// <param name="rhs">The right-hand operand of the operator.</param>
        /// <returns><c>true</c> if <paramref name="lhs"/> and <paramref name="rhs"/> not are equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ArgbColor lhs, ArgbColor rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Parse a well-formatted hexadecimal color string into a instance of <see cref="ArgbColor"/>.
        /// The accepted format is 8 hexadecimal digits, optionally preceded by #.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>A new instance of <see cref="RgbColor"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="str"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException"><paramref name="str"/> is not a hexadecimal color string.</exception>
        public static ArgbColor FromArgbString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            else if (!HexadecimalStringPattern.IsMatch(str))
            {
                throw new FormatException(string.Format("the {0} parameter must be a hexadecimal ARGB color string: 8 hexadecimal digits, optionally preceded by #", nameof(str)));
            }

            var hexStr = str
                .Trim()
                .TrimStart('#');

            return new ArgbColor(
                byte.Parse(hexStr.Substring(0, 2), NumberStyles.HexNumber),
                byte.Parse(hexStr.Substring(2, 2), NumberStyles.HexNumber),
                byte.Parse(hexStr.Substring(4, 2), NumberStyles.HexNumber),
                byte.Parse(hexStr.Substring(6, 2), NumberStyles.HexNumber));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="ArgbColor"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="ArgbColor"/>.</returns>
        public override string ToString()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", this.Alpha, this.Red, this.Green, this.Blue);
        }

        /// <summary>
        /// Returns a <see cref="bool"/> that indicates whether <paramref name="obj"/> is equal to this instance.
        /// Equal <see cref="ArgbColor"/> instances have equal values for <see cref="Alpha"/>, <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>, and <see cref="RgbColor.Blue"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test for equality.</param>
        /// <returns><c>true</c> if the <paramref name="obj"/> is equal to this instance, otherwise <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj != null)
            {
                if (obj is ArgbColor)
                {
                    var other = obj as ArgbColor;
                    return other.Alpha == this.Alpha && base.Equals(obj);
                }
                else if (obj is RgbColor)
                {
                    return base.Equals(obj);
                }
            }

            return false;
        }

        /// <summary>
        /// Serves as a hash function.
        /// </summary>
        /// <returns>A hash code for the current <see cref="ArgbColor"/>.</returns>
        public override int GetHashCode()
        {
            var longHash = (long)(this.Alpha * 0x1000000) + base.GetHashCode();
            return longHash.GetHashCode();
        }
    }
}
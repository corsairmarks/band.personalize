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

namespace Band.Personalize.Model.Color
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A class representing a 8-bit-per-channel RGB color, with supporting methods for manipulating the color.
    /// </summary>
    public class HexadecimalColor
    {
        /// <summary>
        /// A regular expression defining the allowable formats for hexadecimal color string.
        /// </summary>
        private static readonly Regex HexadecimalStringPattern = new Regex("^\\s*#?(?:[0-9a-f]{3}){1,2}\\s*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="HexadecimalColor"/> class.
        /// </summary>
        /// <param name="red">The red channel color saturation.</param>
        /// <param name="green">The green channel color saturation.</param>
        /// <param name="blue">The blue channel color saturation.</param>
        public HexadecimalColor(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        /// <summary>
        /// Gets the red channel color saturation.
        /// </summary>
        public byte Red { get; }

        /// <summary>
        /// Gets the percentage of maximum saturation of the red color channel.
        /// </summary>
        public decimal RedSaturation
        {
            get
            {
                return PercentageOfMaximumSaturation(this.Red);
            }
        }

        /// <summary>
        /// Gets the green channel color saturation.
        /// </summary>
        public byte Green { get; }

        /// <summary>
        /// Gets the percentage of maximum saturation of the green color channel.
        /// </summary>
        public decimal GreenSaturation
        {
            get
            {
                return PercentageOfMaximumSaturation(this.Green);
            }
        }

        /// <summary>
        /// Gets the blue channel color saturation.
        /// </summary>
        public byte Blue { get; }

        /// <summary>
        /// Gets the percentage of maximum saturation of the blue color channel.
        /// </summary>
        public decimal BlueSaturation
        {
            get
            {
                return PercentageOfMaximumSaturation(this.Blue);
            }
        }

        /// <summary>
        /// Operator overload for equality (==).
        /// </summary>
        /// <param name="lhs">The left-hand operand of the operator.</param>
        /// <param name="rhs">The right-hand operand of the operator.</param>
        /// <returns><c>true</c> if <paramref name="lhs"/> and <paramref name="rhs"/> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(HexadecimalColor lhs, HexadecimalColor rhs)
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
        public static bool operator !=(HexadecimalColor lhs, HexadecimalColor rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Parse a well-formatted hexadecimal color string into a instance of <see cref="HexadecimalColor"/>.
        /// The accepted formats are either 3 or 6 hexadecimal digits, optionally preceded by #.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>A new instance of <see cref="HexadecimalColor"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="str"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException"><paramref name="str"/> is not a hexadecimal color string.</exception>
        public static HexadecimalColor Parse(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            else if (!HexadecimalStringPattern.IsMatch(str))
            {
                throw new FormatException(string.Format("the {0} parameter must be a hexadecimal color string: either 3 or 6 hexadecimal digits, optionally preceded by #", nameof(str)));
            }

            var hexStr = str
                .Trim()
                .TrimStart('#');
            if (hexStr.Length == 3)
            {
                hexStr = new string(new[]
                {
                        hexStr[0],
                        hexStr[0],
                        hexStr[1],
                        hexStr[1],
                        hexStr[2],
                        hexStr[2],
                    });
            }

            return new HexadecimalColor(byte.Parse(hexStr.Substring(0, 2), NumberStyles.HexNumber), byte.Parse(hexStr.Substring(2, 2), NumberStyles.HexNumber), byte.Parse(hexStr.Substring(4, 2), NumberStyles.HexNumber));
        }

        /// <summary>
        /// Gets the percentage of the maximum color saturation for the <paramref name="colorChannel"/>.
        /// </summary>
        /// <param name="colorChannel">The color channel for which to calculate the percentage of maximum saturation.</param>
        /// <returns>The percentage of maximum saturation for the <paramref name="colorChannel"/>.</returns>
        public static decimal PercentageOfMaximumSaturation(byte colorChannel)
        {
            return colorChannel / (decimal)byte.MaxValue;
        }

        /// <summary>
        /// Increase or decrease the luminance of the <paramref name="colorChannel"/> by a <paramref name="percentage"/> of the maximum saturation.
        /// </summary>
        /// <param name="colorChannel">The color channel from which to calculate a brighter or darker color.</param>
        /// <param name="percentage">The percentage of maximum saturation brighter or dimmer the new color should be.  Positive increases luminance, while negative reduces.</param>
        /// <returns>A new color that is the specified <paramref name="percentage"/> brighter or darker than the original.</returns>
        public static byte Luminance(byte colorChannel, decimal percentage)
        {
            var chunk = (byte.MaxValue + 1) * percentage;
            return (byte)Math.Max(Math.Min(Math.Round(colorChannel + chunk, MidpointRounding.AwayFromZero), byte.MaxValue), byte.MinValue);
        }

        /// <summary>
        /// Create a new <see cref="HexadecimalColor"/> by altering the luminance by a <paramref name="percentage"/> of the maximum.
        /// </summary>
        /// <param name="percentage">The percentage of maximum saturation brighter or dimmer the new color should be.  Positive increases luminance, while negative reduces.</param>
        /// <returns>A new instance of <see cref="HexadecimalColor"/> that is the specified <paramref name="percentage"/> brighter or darker than the original.</returns>
        public HexadecimalColor Luminance(decimal percentage)
        {
            return new HexadecimalColor(Luminance(this.Red, percentage), Luminance(this.Green, percentage), Luminance(this.Blue, percentage));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="HexadecimalColor"/>.</returns>
        public override string ToString()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", this.Red, this.Green, this.Blue);
        }

        /// <summary>
        /// Returns a <see cref="bool"/> that indicates whether <paramref name="obj"/> is equal to this instance.
        /// Equal <see cref="HexadecimalColor"/> instances have equal values for <see cref="Red"/>, <see cref="Green"/>, and <see cref="Blue"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test for equality.</param>
        /// <returns><c>true</c> if the <paramref name="obj"/> is equal to this instance, otherwise <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj != null && obj is HexadecimalColor)
            {
                var other = obj as HexadecimalColor;
                return other.Red == this.Red && other.Green == this.Green && other.Blue == this.Blue;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function.
        /// </summary>
        /// <returns>A hash code for the current <see cref="HexadecimalColor"/>.</returns>
        public override int GetHashCode()
        {
            return (this.Red * 0x10000) + (this.Green * 0x100) + this.Blue;
        }
    }
}
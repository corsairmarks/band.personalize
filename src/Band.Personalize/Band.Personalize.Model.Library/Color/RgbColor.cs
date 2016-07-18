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
    /// A class representing a 16-bit-per-channel RGB color, with supporting methods for manipulating the color.
    /// </summary>
    public class RgbColor
    {
        /// <summary>
        /// A regular expression defining the allowable formats for a hexadecimal RGB color string.
        /// </summary>
        private static readonly Regex HexadecimalStringPattern = new Regex("^\\s*#?(?:[0-9a-f]{3}){1,2}\\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="RgbColor"/> class.
        /// </summary>
        /// <param name="red">The red channel color saturation.</param>
        /// <param name="green">The green channel color saturation.</param>
        /// <param name="blue">The blue channel color saturation.</param>
        public RgbColor(byte red, byte green, byte blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;

            var hsv = ToHsv(red, green, blue);

            this.Hue = hsv.Item1;
            this.Saturation = hsv.Item2;
            this.Value = hsv.Item3;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RgbColor"/> class from HSV/HSB data.
        /// </summary>
        /// <param name="hue">The hue in degrees.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value (brightness).</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="saturation"/> is less than 0 or greater than 1
        /// or <paramref name="value"/> is less than 0 or greater than 1.
        /// </exception>
        public RgbColor(double hue, double saturation, double value)
        {
            if (saturation < 0 || saturation > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(saturation), saturation, "Must be between 0 and 1, inclusive");
            }
            else if (value < 0 || value > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Must be between 0 and 1, inclusive");
            }

            hue %= 360;
            if (hue < 0)
            {
                hue += 360;
            }

            this.Hue = hue;
            this.Saturation = saturation;
            this.Value = value;

            var rgb = FromHsv(hue, saturation, value);

            this.Red = rgb.Item1;
            this.Green = rgb.Item2;
            this.Blue = rgb.Item3;
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
        /// Gets the hue of this color (HSV/HSB model).
        /// </summary>
        public double Hue { get; }

        /// <summary>
        /// Gets the saturation of this color (HSV/HSB model).
        /// </summary>
        public double Saturation { get; }

        /// <summary>
        /// Gets the value (brightness) of this color (HSV/HSB model).
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Operator overload for equality (==).
        /// </summary>
        /// <param name="lhs">The left-hand operand of the operator.</param>
        /// <param name="rhs">The right-hand operand of the operator.</param>
        /// <returns><c>true</c> if <paramref name="lhs"/> and <paramref name="rhs"/> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(RgbColor lhs, RgbColor rhs)
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
        public static bool operator !=(RgbColor lhs, RgbColor rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Try to parse a hexadecimal color string into an instance of <see cref="RgbColor"/>.
        /// The accepted formats are either 3 or 6 hexadecimal digits, optionally preceded by #.
        /// </summary>
        /// <param name="str">The string to try to parse.</param>
        /// <param name="result">
        /// When this method returns, contains a new instance of <see cref="RgbColor"/> equivalent to the
        /// hexadecimal RGB string contained in <paramref name="str"/>, if the conversion succeeded, or
        /// <c>null</c> if the conversion failed.  This parameter is passed uninitialized; any value
        /// originally supplied in result will be overwritten.
        /// </param>
        /// <returns><c>true</c> if <paramref name="str"/> was converted successfully; otherwise, <c>false</c>.</returns>
        public static bool TryFromRgbString(string str, out RgbColor result)
        {
            if (str != null && HexadecimalStringPattern.IsMatch(str))
            {
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

                result = new RgbColor(
                    byte.Parse(hexStr.Substring(0, 2), NumberStyles.HexNumber),
                    byte.Parse(hexStr.Substring(2, 2), NumberStyles.HexNumber),
                    byte.Parse(hexStr.Substring(4, 2), NumberStyles.HexNumber));
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Parse a well-formatted hexadecimal color string into an instance of <see cref="RgbColor"/>.
        /// The accepted formats are either 3 or 6 hexadecimal digits, optionally preceded by #.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>A new instance of <see cref="RgbColor"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="str"/> is <c>null</c>.</exception>
        /// <exception cref="FormatException"><paramref name="str"/> is not a hexadecimal color string.</exception>
        public static RgbColor FromRgbString(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            RgbColor result;
            if (TryFromRgbString(str, out result))
            {
                return result;
            }
            else
            {
                throw new FormatException(string.Format("the {0} parameter must be a hexadecimal RGB color string: either 3 or 6 hexadecimal digits, optionally preceded by #", nameof(str)));
            }
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
        /// Create a new <see cref="RgbColor"/> by altering the luminance by a <paramref name="percentage"/> of the maximum.
        /// </summary>
        /// <param name="percentage">The percentage of maximum saturation brighter or dimmer the new color should be.  Positive increases luminance, while negative reduces.</param>
        /// <returns>A new instance of <see cref="RgbColor"/> that is the specified <paramref name="percentage"/> brighter or darker than the original.</returns>
        public RgbColor Luminance(decimal percentage)
        {
            return new RgbColor(Luminance(this.Red, percentage), Luminance(this.Green, percentage), Luminance(this.Blue, percentage));
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="RgbColor"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="RgbColor"/>.</returns>
        public override string ToString()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", this.Red, this.Green, this.Blue);
        }

        /// <summary>
        /// Returns a <see cref="bool"/> that indicates whether <paramref name="obj"/> is equal to this instance.
        /// Equal <see cref="RgbColor"/> instances have equal values for <see cref="Red"/>, <see cref="Green"/>, and <see cref="Blue"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to test for equality.</param>
        /// <returns><c>true</c> if the <paramref name="obj"/> is equal to this instance, otherwise <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj != null && obj is RgbColor)
            {
                var other = obj as RgbColor;
                return other.Red == this.Red && other.Green == this.Green && other.Blue == this.Blue;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function.
        /// </summary>
        /// <returns>A hash code for the current <see cref="RgbColor"/>.</returns>
        public override int GetHashCode()
        {
            return (this.Red * 0x10000) + (this.Green * 0x100) + this.Blue;
        }

        /// <summary>
        /// Converts a triplet of RGB color channels to an HSV/HSB triplet.
        /// </summary>
        /// <param name="red">The red color channel.</param>
        /// <param name="green">The green color channel.</param>
        /// <param name="blue">The blue color channel.</param>
        /// <returns>
        /// An instance of <see cref="Tuple{Double,Double,Double}"/> where
        /// <see cref="Tuple{Double,Double,Double}.Item1"/> is hue,
        /// <see cref="Tuple{Double,Double,Double}.Item2"/> is saturation,
        /// and <see cref="Tuple{Double,Double,Double}.Item3"/> is value (brightness).
        /// </returns>
        internal static Tuple<double, double, double> ToHsv(byte red, byte green, byte blue)
        {
            var redPrime = red / (double)byte.MaxValue;
            var greenPrime = green / (double)byte.MaxValue;
            var bluePrime = blue / (double)byte.MaxValue;

            var chromaMax = Math.Max(Math.Max(redPrime, greenPrime), bluePrime);
            var chromaMin = Math.Min(Math.Min(redPrime, greenPrime), bluePrime);
            var delta = chromaMax - chromaMin;

            double huePrime = 0;
            if (delta == 0)
            {
                huePrime = 0;
            }
            else if (chromaMax == redPrime)
            {
                huePrime = ((greenPrime - bluePrime) / delta) % 6;
            }
            else if (chromaMax == greenPrime)
            {
                huePrime = ((bluePrime - redPrime) / delta) + 2;
            }
            else if (chromaMax == bluePrime)
            {
                huePrime = ((redPrime - greenPrime) / delta) + 4;
            }

            var hue = huePrime * 60;
            if (hue < 0)
            {
                hue += 360;
            }

            var saturation = chromaMax != 0
                ? delta / chromaMax
                : 0;

            var value = chromaMax; // brightness

            return Tuple.Create(Math.Round(hue, MidpointRounding.AwayFromZero), saturation, value);
        }

        /// <summary>
        /// Converts an HSV/HSB triplet to a RGB color channel triplet.
        /// </summary>
        /// <param name="hue">The hue in degrees.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value (brightness).</param>
        /// <returns>
        /// An instance of <see cref="Tuple{Byte,Byte,Byte}"/> where
        /// <see cref="Tuple{Byte,Byte,Byte}.Item1"/> is red,
        /// <see cref="Tuple{Byte,Byte,Byte}.Item2"/> is green,
        /// and <see cref="Tuple{Byte,Byte,Byte}.Item3"/> is blue.
        /// </returns>
        internal static Tuple<byte, byte, byte> FromHsv(double hue, double saturation, double value)
        {
            var chroma = value * saturation;

            var huePrime = hue / 60;

            var x = chroma * (1 - Math.Abs((huePrime % 2) - 1));

            var m = value - chroma;

            double redPrime, greenPrime, bluePrime;

            if (huePrime >= 0 && huePrime < 1)
            {
                redPrime = chroma;
                greenPrime = x;
                bluePrime = 0;
            }
            else if (huePrime >= 1 && huePrime < 2)
            {
                redPrime = x;
                greenPrime = chroma;
                bluePrime = 0;
            }
            else if (huePrime >= 2 && huePrime < 3)
            {
                redPrime = 0;
                greenPrime = chroma;
                bluePrime = x;
            }
            else if (huePrime >= 3 && huePrime < 4)
            {
                redPrime = 0;
                greenPrime = x;
                bluePrime = chroma;
            }
            else if (huePrime >= 4 && huePrime < 5)
            {
                redPrime = x;
                greenPrime = 0;
                bluePrime = chroma;
            }
            else
            {
                // assumes: if (huePrime >= 5 && hue < 6)
                redPrime = chroma;
                greenPrime = 0;
                bluePrime = x;
            }

            var red = Unprime(redPrime, m);
            var green = Unprime(greenPrime, m);
            var blue = Unprime(bluePrime, m);

            return Tuple.Create(red, green, blue);
        }

        /// <summary>
        /// Un-primes a color channel prime as part of the HSV/HSB to RGB calculation.
        /// </summary>
        /// <param name="prime">The color channel prime.</param>
        /// <param name="m">The intermediate value m.</param>
        /// <returns>The color channel value.</returns>
        private static byte Unprime(double prime, double m)
        {
            return (byte)Math.Min(Math.Round((prime + m) * byte.MaxValue, MidpointRounding.AwayFromZero), byte.MaxValue);
        }
    }
}
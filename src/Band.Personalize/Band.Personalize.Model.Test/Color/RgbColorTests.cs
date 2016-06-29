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

namespace Band.Personalize.Model.Test.Color
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Library.Color;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="RgbColor"/> class.
    /// </summary>
    public class RgbColorTests
    {
        /// <summary>
        /// Gets the test data for the <see cref="RgbColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(RgbColor, decimal)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> LuminanceBytes
        {
            get
            {
                yield return new object[] { (byte)0x00, 0.25M, (byte)0x40, };
                yield return new object[] { (byte)0x00, -0.25M, (byte)0x00, };
                yield return new object[] { (byte)0x00, 1.25M, (byte)0xFF, };
                yield return new object[] { (byte)0x00, -1.25M, (byte)0x00, };
                yield return new object[] { (byte)0xFF, 0.25M, (byte)0xFF, };
                yield return new object[] { (byte)0xFF, -0.25M, (byte)0xBF, };
                yield return new object[] { (byte)0xFF, 1.25M, (byte)0xFF, };
                yield return new object[] { (byte)0xFF, -1.25M, (byte)0x00, };
                yield return new object[] { (byte)0x13, decimal.Zero, (byte)0x13, };
                yield return new object[] { (byte)0x13, 0.001953124M, (byte)0x13, };
                yield return new object[] { (byte)0x13, -0.001953124M, (byte)0x13, };
                yield return new object[] { (byte)0x13, 0.001953125M, (byte)0x14, };
                yield return new object[] { (byte)0x13, -0.0019531251M, (byte)0x12, };
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="RgbColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(RgbColor, decimal)"/> test.
        /// </summary>
        public static IEnumerable<IEnumerable<object>> LuminanceRgbColors
        {
            get
            {
                yield return new object[] { new RgbColor(0x00, 0x00, 0x00), 0.25M, };
                yield return new object[] { new RgbColor(0x00, 0x00, 0x00), -0.25M, };
                yield return new object[] { new RgbColor(0x00, 0x00, 0x00), 1.25M, };
                yield return new object[] { new RgbColor(0x00, 0x00, 0x00), -1.25M, };
                yield return new object[] { new RgbColor(0xFF, 0xFF, 0xFF), 0.25M, };
                yield return new object[] { new RgbColor(0xFF, 0xFF, 0xFF), -0.25M, };
                yield return new object[] { new RgbColor(0xFF, 0xFF, 0xFF), 1.25M, };
                yield return new object[] { new RgbColor(0xFF, 0xFF, 0xFF), -1.25M, };
                yield return new object[] { new RgbColor(0x04, 0x13, 0x84), decimal.Zero, };
                yield return new object[] { new RgbColor(0x04, 0x13, 0x84), 0.001953124M, };
                yield return new object[] { new RgbColor(0x04, 0x13, 0x84), -0.001953124M, };
                yield return new object[] { new RgbColor(0x04, 0x13, 0x84), 0.001953125M, };
                yield return new object[] { new RgbColor(0x04, 0x13, 0x84), -0.0019531251M, };
            }
        }

        /// <summary>
        /// Verify the constructor sets the public properties.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void CtorRgb_SetsProperties(byte red, byte green, byte blue)
        {
            // Act
            var result = new RgbColor(red, green, blue);

            // Assert
            Assert.Equal(red, result.Red);
            Assert.Equal(green, result.Green);
            Assert.Equal(blue, result.Blue);
        }

        /// <summary>
        /// Verify the constructor sets the public properties.
        /// </summary>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(HsvColorDoubleData))]
        public void CtorHsv_SetsProperties(double hue, double saturation, double value)
        {
            // Act
            var result = new RgbColor(hue, saturation, value);

            // Assert
            Assert.Equal(hue, result.Hue);
            Assert.Equal(saturation, result.Saturation);
            Assert.Equal(value, result.Value);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.RedSaturation"/> property calculates the
        /// correct saturation based on the <see cref="RgbColor.Red"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void RedSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new RgbColor(colorChannel, 0x00, 0x00);
            var expected = RgbColor.PercentageOfMaximumSaturation(target.Red);

            // Act
            var result = target.RedSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.GreenSaturation"/> property calculates the
        /// correct saturation based on the <see cref="RgbColor.Green"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void GreenSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new RgbColor(0x00, colorChannel, 0x00);
            var expected = RgbColor.PercentageOfMaximumSaturation(target.Green);

            // Act
            var result = target.GreenSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.BlueSaturation"/> property calculates the
        /// correct saturation based on the <see cref="RgbColor.Blue"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void BlueSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new RgbColor(0x00, 0x00, colorChannel);
            var expected = RgbColor.PercentageOfMaximumSaturation(target.Blue);

            // Act
            var result = target.BlueSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="RgbColor"/> does returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="RgbColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NonNullNotEqualRgbColors", MemberType = typeof(RgbColorData))]
        public void EqualityOperator_ChannelsNotEqual_IsFalse(RgbColor notEqual)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="RgbColor"/> returns <c>true</c> when the argument is another instance of <see cref="RgbColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_AllChannelsEqual_IsTrue(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new RgbColor(red, green, blue);
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="RgbColor"/> returns <c>true</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void EqualityOperator_BothOperandsNull_IsTrue()
        {
            // Arrange
            RgbColor equal = null;
            RgbColor target = null;

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative for <see cref="RgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="RgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("RgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void EqualityOperator_IsCommutative(RgbColor compare)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result1 = target == compare;
            var result2 = compare == target;

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for <see cref="RgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="RgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("RgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void EqualityOperator_IsReflexive(RgbColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for the same instance of <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new RgbColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="RgbColor"/> returns <c>true</c> if <paramref name="notEqual"/> is not an instance of <see cref="RgbColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NonNullNotEqualRgbColors", MemberType = typeof(RgbColorData))]
        public void InequalityOperator_ChannelsNotEqual_IsTrue(RgbColor notEqual)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="RgbColor"/> returns <c>false</c> when the argument is another instance of <see cref="RgbColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_AllChannelsEqual_IsFalse(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new RgbColor(red, green, blue);
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="RgbColor"/> returns <c>false</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void InequalityOperator_BothOperandsNull_IsFalse()
        {
            // Arrange
            RgbColor equal = null;
            RgbColor target = null;

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative for <see cref="RgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="RgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("RgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void InequalityOperator_IsCommutative(RgbColor compare)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result1 = target != compare;
            var result2 = compare != target;

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for <see cref="RgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="RgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("RgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void InequalityOperator_IsReflexive(RgbColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for the same instance of <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new RgbColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ParseHexadecimal(string)"/> method throws an <see cref="ArgumentNullException"/> when it is passed <c>null</c>.
        /// </summary>
        [Fact]
        public void ParseHexadecimal_NullInput_Throws()
        {
            // Arrange
            string target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => RgbColor.ParseHexadecimal(target));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("str", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ParseHexadecimal(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        [Theory]
        [MemberData("InvalidHexadecimalColorStrings", MemberType = typeof(RgbColorData))]
        public void ParseHexadecimal_InvalidFormat_Throws(string target)
        {
            // Act
            var expected = Assert.Throws<FormatException>(() => RgbColor.ParseHexadecimal(target));

            // Assert
            Assert.NotNull(expected);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ParseHexadecimal(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        /// <param name="expected">The expected result of parsing.</param>
        [Theory]
        [MemberData("ValidHexadecimalColorStrings", MemberType = typeof(RgbColorData))]
        public void ParseHexadecimal_ValidFormat_ReturnsEquivalentRgbColor(string target, RgbColor expected)
        {
            // Act
            var result = RgbColor.ParseHexadecimal(target);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.PercentageOfMaximumSaturation(byte)"/> method calculates the
        /// percentage of <see cref="byte.MaxValue"/> of the specified <paramref name="colorChannel"/>.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void PercentageOfMaximumSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var expected = colorChannel / (decimal)byte.MaxValue;

            // Act
            var result = RgbColor.PercentageOfMaximumSaturation(colorChannel);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Luminance(byte, decimal)"/> method returns a byte modified brighter or darker by the
        /// specified <paramref name="percentage"/>.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        /// <param name="percentage">The percentage of maximum by which to brighten/darken the <paramref name="colorChannel"/>.</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [MemberData("LuminanceBytes")]
        public void Luminance_Static_ReturnsModifiedByte(byte colorChannel, decimal percentage, byte expected)
        {
            // Act
            var result = RgbColor.Luminance(colorChannel, percentage);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Luminance(decimal)"/> method returns a new instance of <see cref="RgbColor"/> with
        /// each channel modified brighter or darker by the specified <paramref name="percentage"/>.
        /// </summary>
        /// <param name="target">The color to test.</param>
        /// <param name="percentage">The percentage of maximum by which to brighten/darken each channel on the <paramref name="target"/>.</param>
        [Theory]
        [MemberData("LuminanceRgbColors")]
        public void Luminance_ReturnsNewInstanceWithModifiedChannels(RgbColor target, decimal percentage)
        {
            // Act
            var result = target.Luminance(percentage);

            // Assert
            Assert.NotSame(target, result);
            Assert.Equal(RgbColor.Luminance(target.Red, percentage), result.Red);
            Assert.Equal(RgbColor.Luminance(target.Green, percentage), result.Green);
            Assert.Equal(RgbColor.Luminance(target.Blue, percentage), result.Blue);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ToString"/> method formats its output as a hexadecimal string with a leading '#' character.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToString_FormatsAsHexString(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.ToString();

            // Assert
            Assert.Equal($"#{red:X2}{green:X2}{blue:X2}", result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Equals(object)"/> method returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="RgbColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NotEqualObjects", MemberType = typeof(RgbColorData))]
        public void EqualsMethod_ChannelsNotEqual_IsFalse(object notEqual)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result = target.Equals(notEqual);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Equals(object)"/> method returns <c>true</c> when the argument is another instance of <see cref="RgbColor"/> where all color saturation channels are equal to the original.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_AllChannelsEqual_IsTrue(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new RgbColor(red, green, blue);
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.Equals(equal);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Equals(object)"/> method is commutative for non-<c>null</c> <see cref="RgbColor"/> instances.
        /// </summary>
        /// <param name="compare">The <see cref="RgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("NonNullRgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void EqualsMethod_IsCommutative(RgbColor compare)
        {
            // Arrange
            var target = RgbColorData.DefaultRgbColor;

            // Act
            var result1 = target.Equals(compare);
            var result2 = compare.Equals(target);

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Equals(object)"/> method is reflexive for non-<c>null</c> <see cref="RgbColor"/> instances.
        /// </summary>
        /// <param name="color">The <see cref="RgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("NonNullRgbColorsWithDefault", MemberType = typeof(RgbColorData))]
        public void EqualsMethod_IsReflexive(RgbColor color)
        {
            // Act
            var result = color.Equals(color);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.Equals(object)"/> method is reflexive for the same instance of <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new RgbColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.GetHashCode"/> method returns a value based on the sum of the color channels,
        /// with red being offset by 0x1000, green being offset 0x100, and blue with no offset.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void GetHashCode_IsSumOfChannelsWithOffset(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new RgbColor(red, green, blue);

            // Act
            var result = target.GetHashCode();

            // Assert
            Assert.Equal((red * 0x10000) + (green * 0x100) + blue, result);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.GetHashCode"/> method returns the same value for <see cref="RgbColor"/> instances that are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void GetHashCode_ColorsAreEqual_AreEqual(byte red, byte green, byte blue)
        {
            // Arrange
            var target1 = new RgbColor(red, green, blue);
            var target2 = new RgbColor(red, green, blue);

            // Act
            var result1 = target1.GetHashCode();
            var result2 = target2.GetHashCode();

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ToHsv(byte, byte, byte)"/> method properly calculates the HSV/HSB values.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(EquivalentHsvAndRgbColorData))]
        public void ToHsv_CalculatesValues(byte red, byte green, byte blue, double hue, double saturation, double value)
        {
            // Act
            var result = RgbColor.ToHsv(red, green, blue);

            // Assert
            Assert.Equal(hue, result.Item1);
            Assert.Equal(saturation, result.Item2);
            Assert.Equal(value, result.Item3);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.FromHsv(double, double, double)"/> method properly calculates the RGB values.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(EquivalentHsvAndRgbColorData))]
        public void FromHsv_CalculatesValues(byte red, byte green, byte blue, double hue, double saturation, double value)
        {
            // Act
            var result = RgbColor.FromHsv(hue, saturation, value);

            // Assert
            Assert.Equal(red, result.Item1);
            Assert.Equal(green, result.Item2);
            Assert.Equal(blue, result.Item3);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ToHsv(byte, byte, byte)"/> method is commutative.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(EquivalentHsvAndRgbColorData))]
        public void ToHsv_IsCommutative(byte red, byte green, byte blue, double hue, double saturation, double value)
        {
            // Arrange
            var target = Tuple.Create(hue, saturation, value);
            var target2 = Tuple.Create(red, green, blue);

            // Act
            var result = RgbColor.ToHsv(red, green, blue);
            var result2 = RgbColor.FromHsv(result.Item1, result.Item2, result.Item3);

            // Assert
            Assert.Equal(target, result);
            Assert.Equal(target2, result2);
        }

        /// <summary>
        /// Verify the <see cref="RgbColor.ToHsv(byte, byte, byte)"/> method is commutative.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(EquivalentHsvAndRgbColorData))]
        public void FromHsv_IsCommutative(byte red, byte green, byte blue, double hue, double saturation, double value)
        {
            // Arrange
            var target = Tuple.Create(red, green, blue);
            var target2 = Tuple.Create(hue, saturation, value);

            // Act
            var result = RgbColor.FromHsv(hue, saturation, value);
            var result2 = RgbColor.ToHsv(result.Item1, result.Item2, result.Item3);

            // Assert
            Assert.Equal(target, result);
            Assert.Equal(target2, result2);
        }

        /// <summary>
        /// Verify that each constructor creates an equivalent instance when provided with mathematically equivalent RGB and HSV/HSB data.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        /// <param name="hue">The hue to test.</param>
        /// <param name="saturation">The saturation to test.</param>
        /// <param name="value">The value (brightness) to test.</param>
        [Theory]
        [ClassData(typeof(EquivalentHsvAndRgbColorData))]
        public void Ctor_CreatesEquivalentColors(byte red, byte green, byte blue, double hue, double saturation, double value)
        {
            // Arrange
            var rgbTarget = new RgbColor(red, green, blue);
            var hsvTarget = new RgbColor(hue, saturation, value);

            // Act
            var result = rgbTarget == hsvTarget;

            // Assert
            Assert.True(result);
        }
    }
}
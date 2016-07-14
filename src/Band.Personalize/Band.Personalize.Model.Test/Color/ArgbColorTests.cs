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
    using Data;
    using Library.Color;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ArgbColor"/> class.
    /// </summary>
    public class ArgbColorTests
    {
        /// <summary>
        /// Verify the constructor sets the public properties.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void CtorArgb_SetsProperties(byte alpha, byte red, byte green, byte blue)
        {
            // Act
            var result = new ArgbColor(alpha, red, green, blue);

            // Assert
            Assert.Equal(alpha, result.Alpha);
            Assert.Equal(red, result.Red);
            Assert.Equal(green, result.Green);
            Assert.Equal(blue, result.Blue);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.AlphaSaturation"/> property calculates the
        /// correct saturation based on the <see cref="ArgbColor.Alpha"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void AlphaSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new ArgbColor(colorChannel, 0x00, 0x00, 0x00);
            var expected = RgbColor.PercentageOfMaximumSaturation(target.Alpha);

            // Act
            var result = target.AlphaSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="ArgbColor"/> does returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="ArgbColor"/> where all channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullNotEqualArgbColors), MemberType = typeof(ArgbColorData))]
        public void EqualityOperator_ChannelsNotEqual_IsFalse(ArgbColor notEqual)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="ArgbColor"/> returns <c>true</c> when the argument is another instance of <see cref="ArgbColor"/> where all channels are equal.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void EqualityOperator_AllChannelsEqual_IsTrue(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new ArgbColor(alpha, red, green, blue);
            var target = new ArgbColor(alpha, red, green, blue);

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="ArgbColor"/> returns <c>true</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void EqualityOperator_BothOperandsNull_IsTrue()
        {
            // Arrange
            ArgbColor equal = null;
            ArgbColor target = null;

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative for <see cref="ArgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="ArgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void EqualityOperator_IsCommutative(ArgbColor compare)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result1 = target == compare;
            var result2 = compare == target;

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for <see cref="ArgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void EqualityOperator_IsReflexive(ArgbColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for the same instance of <see cref="ArgbColor"/>.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void EqualityOperator_SameInstance_IsReflexive(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var color = new ArgbColor(alpha, red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) returns <c>true</c> when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_RgbColorAndArgbMaxAlpha_IsTrue(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result = rgbTarget == argbTarget;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_RgbColorAndArgbMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result1 = rgbTarget == argbTarget;
            var result2 = argbTarget == rgbTarget;

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the inequality operator (!=) returns <c>false</c> when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_RgbColorAndArgbNotMaxAlpha_IsTrue(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result = rgbTarget == argbTarget;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_RgbColorAndArgbNotMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result1 = rgbTarget == argbTarget;
            var result2 = argbTarget == rgbTarget;

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="ArgbColor"/> returns <c>true</c> if <paramref name="notEqual"/> is not an instance of <see cref="ArgbColor"/> where all channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullNotEqualArgbColors), MemberType = typeof(ArgbColorData))]
        public void InequalityOperator_ChannelsNotEqual_IsTrue(ArgbColor notEqual)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="ArgbColor"/> returns <c>false</c> when the argument is another instance of <see cref="ArgbColor"/> where all channels are equal.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void InequalityOperator_AllChannelsEqual_IsFalse(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new ArgbColor(alpha, red, green, blue);
            var target = new ArgbColor(alpha, red, green, blue);

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="ArgbColor"/> returns <c>false</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void InequalityOperator_BothOperandsNull_IsFalse()
        {
            // Arrange
            ArgbColor equal = null;
            ArgbColor target = null;

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative for <see cref="ArgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="ArgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void InequalityOperator_IsCommutative(ArgbColor compare)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result1 = target != compare;
            var result2 = compare != target;

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for <see cref="ArgbColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void InequalityOperator_IsReflexive(ArgbColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for the same instance of <see cref="ArgbColor"/>.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void InequalityOperator_SameInstance_IsReflexive(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var color = new ArgbColor(alpha, red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the equality operator (==) returns <c>false</c> when an instance or <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_RgbColorAndArgbMaxAlpha_IsFalse(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result = rgbTarget != argbTarget;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative when an instance or <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_RgbColorAndArgbMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result1 = rgbTarget != argbTarget;
            var result2 = argbTarget != rgbTarget;

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the inequality operator (!=) returns <c>true</c> when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_RgbColorAndArgbNotMaxAlpha_IsFalse(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result = rgbTarget != argbTarget;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_RgbColorAndArgbNotMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result1 = rgbTarget != argbTarget;
            var result2 = argbTarget != rgbTarget;

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.TryFromArgbString(string, out ArgbColor)"/> method does not throw an <see cref="ArgumentNullException"/>
        /// when it is passed <c>null</c>, but instead returns <c>false</c> and outputs <c>null</c>.
        /// </summary>
        [Fact]
        public void TryFromArgbString_NullInput_DoesNotThrowAndIsFalse()
        {
            // Arrange
            string target = null;
            ArgbColor parseResult;

            // Act
            var result = ArgbColor.TryFromArgbString(target, out parseResult);

            // Assert
            Assert.False(result);
            Assert.Null(parseResult);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.TryFromArgbString(string, out ArgbColor)"/> method does not throw a <see cref="FormatException"/>
        /// when it is passed string data it cannot parse, but instead returns <c>false</c> and outputs <c>null</c>.
        /// </summary>
        /// <param name="target">The target string to try to parse.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.InvalidHexadecimalArgbColorStrings), MemberType = typeof(ArgbColorData))]
        public void TryFromArgbString_InvalidFormat_DoesNotThrowAndIsFalse(string target)
        {
            // Arrange
            ArgbColor parseResult;

            // Act
            var result = ArgbColor.TryFromArgbString(target, out parseResult);

            // Assert
            Assert.False(result);
            Assert.Null(parseResult);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.TryFromArgbString(string, out ArgbColor)"/> method outputs the expected <see cref="ArgbColor"/>
        /// and returns <c>true</c> when it is passed string data it can parse.
        /// </summary>
        /// <param name="target">The target string to try to parse.</param>
        /// <param name="expected">The expected output of parsing.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ValidHexadecimalArgbColorStrings), MemberType = typeof(ArgbColorData))]
        public void TryFromArgbString_ValidFormat_OutputsEquivalentArgbColorAndIsTrue(string target, ArgbColor expected)
        {
            // Arrange
            ArgbColor parseResult;

            // Act
            var result = ArgbColor.TryFromArgbString(target, out parseResult);

            // Assert
            Assert.True(result);
            Assert.Equal(expected, parseResult);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.FromArgbString(string)"/> method throws an <see cref="ArgumentNullException"/> when it is passed <c>null</c>.
        /// </summary>
        [Fact]
        public void FromArgbString_NullInput_Throws()
        {
            // Arrange
            string target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => ArgbColor.FromArgbString(target));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("str", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.FromArgbString(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.InvalidHexadecimalArgbColorStrings), MemberType = typeof(ArgbColorData))]
        public void FromArgbString_InvalidFormat_Throws(string target)
        {
            // Act
            var expected = Assert.Throws<FormatException>(() => ArgbColor.FromArgbString(target));

            // Assert
            Assert.NotNull(expected);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.FromArgbString(string)"/> method returns the expected <see cref="ArgbColor"/> when it is passed string data it can parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        /// <param name="expected">The expected result of parsing.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.ValidHexadecimalArgbColorStrings), MemberType = typeof(ArgbColorData))]
        public void FromArgbString_ValidFormat_ReturnsEquivalentArgbColor(string target, ArgbColor expected)
        {
            // Act
            var result = ArgbColor.FromArgbString(target);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.ToString"/> method formats its output as a hexadecimal string with a leading '#' character.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void ToString_FormatsAsHexString(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var target = new ArgbColor(alpha, red, green, blue);

            // Act
            var result = target.ToString();

            // Assert
            Assert.Equal($"#{alpha:X2}{red:X2}{green:X2}{blue:X2}", result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="ArgbColor"/> where all channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NotEqualObjects), MemberType = typeof(ArgbColorData))]
        public void EqualsMethod_ChannelsNotEqual_IsFalse(object notEqual)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result = target.Equals(notEqual);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method returns <c>true</c> when the argument is another instance of <see cref="ArgbColor"/> where all channels are equal to the original.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void EqualsMethod_AllChannelsEqual_IsTrue(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new ArgbColor(alpha, red, green, blue);
            var target = new ArgbColor(alpha, red, green, blue);

            // Act
            var result = target.Equals(equal);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method is commutative for non-<c>null</c> <see cref="ArgbColor"/> instances.
        /// </summary>
        /// <param name="compare">The <see cref="ArgbColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void EqualsMethod_IsCommutative(ArgbColor compare)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;

            // Act
            var result1 = target.Equals(compare);
            var result2 = compare.Equals(target);

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method is reflexive for non-<c>null</c> <see cref="ArgbColor"/> instances.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void EqualsMethod_IsReflexive(ArgbColor color)
        {
            // Act
            var result = color.Equals(color);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method is reflexive for the same instance of <see cref="ArgbColor"/>.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void EqualsMethod_SameInstance_IsReflexive(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var color = new ArgbColor(alpha, red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself

            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method returns <c>true</c> when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_RgbColorAndArgbMaxAlpha_IsTrue(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result = argbTarget.Equals(rgbTarget);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method is commutative when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_RgbColorAndArgbMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(byte.MaxValue, red, green, blue);

            // Act
            var result1 = argbTarget.Equals(rgbTarget);
            var result2 = rgbTarget.Equals(argbTarget);

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method returns <c>false</c> when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_RgbColorAndArgbNotMaxAlpha_IsTrue(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result = argbTarget.Equals(rgbTarget);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.Equals(object)"/> method is commutative when an instance of <see cref="ArgbColor"/>
        /// and <see cref="RgbColor"/> have the same <see cref="RgbColor.Red"/>, <see cref="RgbColor.Green"/>,
        /// and <see cref="RgbColor.Blue"/> values and <see cref="ArgbColor.Alpha"/> is a value other than
        /// <see cref="byte.MaxValue"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_RgbColorAndArgbNotMaxAlpha_IsCommutative(byte red, byte green, byte blue)
        {
            // Assert
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(0x80, red, green, blue);

            // Act
            var result1 = argbTarget.Equals(rgbTarget);
            var result2 = rgbTarget.Equals(argbTarget);

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.GetHashCode"/> method returns a value based on the sum of the color channels,
        /// with red being offset by 0x1000, green being offset 0x100, and blue with no offset.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void GetHashCode_IsHashCodeOfLongSumOfChannelsWithOffset(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var rgbTarget = new RgbColor(red, green, blue);
            var argbTarget = new ArgbColor(alpha, red, green, blue);
            var targetHashCode = ((long)(alpha * 0x1000000) + rgbTarget.GetHashCode()).GetHashCode();

            // Act
            var result = argbTarget.GetHashCode();

            // Assert
            Assert.Equal(targetHashCode, result);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.GetHashCode"/> method returns the same value for <see cref="ArgbColor"/> instances that are equal.
        /// </summary>
        /// <param name="alpha">The alpha channel saturation to test.</param>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(ArgbColorByteData))]
        public void GetHashCode_ColorsAreEqual_AreEqual(byte alpha, byte red, byte green, byte blue)
        {
            // Arrange
            var target1 = new ArgbColor(alpha, red, green, blue);
            var target2 = new ArgbColor(alpha, red, green, blue);

            // Act
            var result1 = target1.GetHashCode();
            var result2 = target2.GetHashCode();

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.GetHashCode()"/> method does not throw.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to verify <see cref="ArgbColor.GetHashCode()"/> does not throw.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void GetHashCode_DoesNotThrow(ArgbColor color)
        {
            // Act
            var result = color.GetHashCode();

            // Assert: passes as long as an exception is not thrown
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.GetHashCode()"/> method returns the same value when two instances of <see cref="ArgbColor"/> are equal based on <see cref="ArgbColor.Equals(object)"/>.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to which may or may not be equal to <see cref="ArgbColorData.DefaultArgbColor"/>.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullArgbColorsWithDefault), MemberType = typeof(ArgbColorData))]
        public void GetHashCode_EqualityOperator_EqualInstancesSameHashCode(ArgbColor color)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;
            var targetHashCode = target.GetHashCode();
            var isEqual = color == target;

            // Act
            var result = color.GetHashCode();

            // Assert
            if (isEqual)
            {
                Assert.Equal(targetHashCode, result);
            }
        }

        /// <summary>
        /// Verify the <see cref="ArgbColor.GetHashCode()"/> method returns the same value when two instances of <see cref="ArgbColor"/> are equal based on the equality operator.
        /// </summary>
        /// <param name="color">The <see cref="ArgbColor"/> to which may or may not be equal to <see cref="ArgbColorData.DefaultArgbColor"/>.</param>
        [Theory]
        [MemberData(nameof(ArgbColorData.NonNullNotEqualArgbColors), MemberType = typeof(ArgbColorData))]
        public void GetHashCode_EqualsMethod_EqualInstancesSameHashCode(ArgbColor color)
        {
            // Arrange
            var target = ArgbColorData.DefaultArgbColor;
            var targetHashCode = target.GetHashCode();
            var isEqual = color.Equals(target);

            // Act
            var result = color.GetHashCode();

            // Assert
            if (isEqual)
            {
                Assert.Equal(targetHashCode, result);
            }
        }
    }
}
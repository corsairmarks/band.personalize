namespace Band.Personalize.Common.Test.Color
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Color;
    using Data;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="HexadecimalColor"/> class.
    /// </summary>
    public class HexadecimalColorTests
    {
        /// <summary>
        /// Gets the test data for the <see cref="HexadecimalColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor, decimal)"/> test.
        /// </summary>
        private static IEnumerable<IEnumerable<object>> LuminanceBytes
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
        /// Gets the test data for the <see cref="HexadecimalColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor, decimal)"/> test.
        /// </summary>
        private static IEnumerable<IEnumerable<object>> LuminanceHexadecimalColors
        {
            get
            {
                yield return new object[] { new HexadecimalColor(0x00, 0x00, 0x00), 0.25M, };
                yield return new object[] { new HexadecimalColor(0x00, 0x00, 0x00), -0.25M, };
                yield return new object[] { new HexadecimalColor(0x00, 0x00, 0x00), 1.25M, };
                yield return new object[] { new HexadecimalColor(0x00, 0x00, 0x00), -1.25M, };
                yield return new object[] { new HexadecimalColor(0xFF, 0xFF, 0xFF), 0.25M, };
                yield return new object[] { new HexadecimalColor(0xFF, 0xFF, 0xFF), -0.25M, };
                yield return new object[] { new HexadecimalColor(0xFF, 0xFF, 0xFF), 1.25M, };
                yield return new object[] { new HexadecimalColor(0xFF, 0xFF, 0xFF), -1.25M, };
                yield return new object[] { new HexadecimalColor(0x04, 0x13, 0x84), decimal.Zero, };
                yield return new object[] { new HexadecimalColor(0x04, 0x13, 0x84), 0.001953124M, };
                yield return new object[] { new HexadecimalColor(0x04, 0x13, 0x84), -0.001953124M, };
                yield return new object[] { new HexadecimalColor(0x04, 0x13, 0x84), 0.001953125M, };
                yield return new object[] { new HexadecimalColor(0x04, 0x13, 0x84), -0.0019531251M, };
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
        public void Ctor_SetsProperties(byte red, byte green, byte blue)
        {
            // Act
            var result = new HexadecimalColor(red, green, blue);

            // Assert
            Assert.Equal(red, result.Red);
            Assert.Equal(green, result.Green);
            Assert.Equal(blue, result.Blue);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.RedSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Red"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void RedSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(colorChannel, 0x00, 0x00);
            var expected = HexadecimalColor.PercentageOfMaximumSaturation(target.Red);

            // Act
            var result = target.RedSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GreenSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Green"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void GreenSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(0x00, colorChannel, 0x00);
            var expected = HexadecimalColor.PercentageOfMaximumSaturation(target.Green);

            // Act
            var result = target.GreenSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.BlueSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Blue"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorChannelByteData))]
        public void BlueSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(0x00, 0x00, colorChannel);
            var expected = HexadecimalColor.PercentageOfMaximumSaturation(target.Blue);

            // Act
            var result = target.BlueSaturation;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> does returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NonNullNotEqualHexadecimalColors", MemberType = typeof(HexadecimalColorData))]
        public void EqualityOperator_ChannelsNotEqual_IsFalse(HexadecimalColor notEqual)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> returns <c>true</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_AllChannelsEqual_IsTrue(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new HexadecimalColor(red, green, blue);
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> returns <c>true</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void EqualityOperator_BothOperandsNull_IsTrue()
        {
            // Arrange
            HexadecimalColor equal = null;
            HexadecimalColor target = null;

            // Act
            var result = target == equal;

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("HexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void EqualityOperator_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result1 = target == compare;
            var result2 = compare == target;

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("HexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void EqualityOperator_IsReflexive(HexadecimalColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualityOperator_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new HexadecimalColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>true</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NonNullNotEqualHexadecimalColors", MemberType = typeof(HexadecimalColorData))]
        public void InequalityOperator_ChannelsNotEqual_IsTrue(HexadecimalColor notEqual)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result = target == notEqual;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>false</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_AllChannelsEqual_IsFalse(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new HexadecimalColor(red, green, blue);
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>false</c> when both operands are <c>null</c>.
        /// </summary>
        [Fact]
        public void InequalityOperator_BothOperandsNull_IsFalse()
        {
            // Arrange
            HexadecimalColor equal = null;
            HexadecimalColor target = null;

            // Act
            var result = target != equal;

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("HexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void InequalityOperator_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result1 = target != compare;
            var result2 = compare != target;

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("HexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void InequalityOperator_IsReflexive(HexadecimalColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718
            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void InequalityOperator_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new HexadecimalColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws an <see cref="ArgumentNullException"/> when it is passed <c>null</c>.
        /// </summary>
        [Fact]
        public void Parse_NullInput_Throws()
        {
            // Arrange
            string target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => HexadecimalColor.Parse(target));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("str", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        [Theory]
        [MemberData("InvalidHexadecimalColorStrings", MemberType = typeof(HexadecimalColorData))]
        public void Parse_InvalidInput_Throws(string target)
        {
            // Act
            var expected = Assert.Throws<FormatException>(() => HexadecimalColor.Parse(target));

            // Assert
            Assert.NotNull(expected);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        /// <param name="expected">The expected result of parsing.</param>
        [Theory]
        [MemberData("ValidHexadecimalColorStrings", MemberType = typeof(HexadecimalColorData))]
        public void Parse_ValidFormat_ReturnsEquivalentHexadecimalColor(string target, HexadecimalColor expected)
        {
            // Act
            var result = HexadecimalColor.Parse(target);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.PercentageOfMaximumSaturation(byte)"/> method calculates the
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
            var result = HexadecimalColor.PercentageOfMaximumSaturation(colorChannel);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Luminance(byte, decimal)"/> method returns a byte modified brighter or darker by the
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
            var result = HexadecimalColor.Luminance(colorChannel, percentage);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Luminance(decimal)"/> method returns a new instance of <see cref="HexadecimalColor"/> with
        /// each channel modified brighter or darker by the specified <paramref name="percentage"/>.
        /// </summary>
        /// <param name="target">The color to test.</param>
        /// <param name="percentage">The percentage of maximum by which to brighten/darken each channel on the <paramref name="target"/>.</param>
        [Theory]
        [MemberData("LuminanceHexadecimalColors")]
        public void Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor target, decimal percentage)
        {
            // Act
            var result = target.Luminance(percentage);

            // Assert
            Assert.NotSame(target, result);
            Assert.Equal(HexadecimalColor.Luminance(target.Red, percentage), result.Red);
            Assert.Equal(HexadecimalColor.Luminance(target.Green, percentage), result.Green);
            Assert.Equal(HexadecimalColor.Luminance(target.Blue, percentage), result.Blue);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.ToString"/> method formats its output as a hexadecimal string with a leading '#' character.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void ToString_FormatsAsHexString(byte red, byte green, byte blue)
        {
            // Arrange
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target.ToString();

            // Assert
            Assert.Equal($"#{red:X2}{green:X2}{blue:X2}", result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [Theory]
        [MemberData("NotEqualObjects", MemberType = typeof(HexadecimalColorData))]
        public void EqualsMethod_ChannelsNotEqual_IsFalse(object notEqual)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result = target.Equals(notEqual);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method returns <c>true</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal to the original.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_AllChannelsEqual_IsTrue(byte red, byte green, byte blue)
        {
            // Arrange
            var equal = new HexadecimalColor(red, green, blue);
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target.Equals(equal);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is commutative for non-<c>null</c> <see cref="HexadecimalColor"/> instances.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [Theory]
        [MemberData("NonNullHexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void EqualsMethod_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = HexadecimalColorData.DefaultColor;

            // Act
            var result1 = target.Equals(compare);
            var result2 = compare.Equals(target);

            // Assert
            Assert.Equal(result1, result2);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is reflexive for non-<c>null</c> <see cref="HexadecimalColor"/> instances.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [Theory]
        [MemberData("NonNullHexadecimalColorsWithDefault", MemberType = typeof(HexadecimalColorData))]
        public void EqualsMethod_IsReflexive(HexadecimalColor color)
        {
            // Act
            var result = color.Equals(color);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void EqualsMethod_SameInstance_IsReflexive(byte red, byte green, byte blue)
        {
            // Arrange
            var color = new HexadecimalColor(red, green, blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GetHashCode"/> method returns a value based on the sum of the color channels,
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
            var target = new HexadecimalColor(red, green, blue);

            // Act
            var result = target.GetHashCode();

            // Assert
            Assert.Equal((red * 0x10000) + (green * 0x100) + blue, result);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GetHashCode"/> method returns the same value for <see cref="HexadecimalColor"/> instances that are equal.
        /// </summary>
        /// <param name="red">The red channel color saturation to test.</param>
        /// <param name="green">The green channel color saturation to test.</param>
        /// <param name="blue">The blue channel color saturation to test.</param>
        [Theory]
        [ClassData(typeof(RgbColorByteData))]
        public void GetHashCode_ColorsAreEqual_AreEqual(byte red, byte green, byte blue)
        {
            // Arrange
            var target1 = new HexadecimalColor(red, green, blue);
            var target2 = new HexadecimalColor(red, green, blue);

            // Act
            var result1 = target1.GetHashCode();
            var result2 = target2.GetHashCode();

            // Assert
            Assert.Equal(result1, result2);
        }
    }
}
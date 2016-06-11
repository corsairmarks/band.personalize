namespace Band.Personalize.Common.Test.Color
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Color;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="HexadecimalColor"/> class.
    /// </summary>
    [TestFixture(TestOf = typeof(HexadecimalColor))]
    public class HexadecimalColorTests
    {
        /// <summary>
        /// The default red channel saturation used for testing.
        /// </summary>
        private const byte Red = 0x6A;

        /// <summary>
        /// The default green channel saturation used for testing.
        /// </summary>
        private const byte Green = 0x00;

        /// <summary>
        /// The default blue channel saturation used for testing.
        /// </summary>
        private const byte Blue = 0xFF;

        /// <summary>
        /// Gets a collection of sample color saturation channels.
        /// </summary>
        private static IEnumerable<byte> Channels
        {
            get
            {
                yield return 0x00;
                yield return 0x11;
                yield return 0x22;
                yield return 0x33;
                yield return 0x44;
                yield return 0x55;
                yield return 0x66;
                yield return 0x77;
                yield return 0x88;
                yield return 0x99;
                yield return 0xAA;
                yield return 0xBB;
                yield return 0xCC;
                yield return 0xDD;
                yield return 0xEE;
                yield return 0xFF;
            }
        }

        /// <summary>
        /// Gets collection of invalid hexadecimal strings.
        /// </summary>
        private static IEnumerable<string> InvalidHexadecimalColorStrings
        {
            get
            {
                yield return string.Empty;
                yield return "#";
                yield return "#0";
                yield return "#00";
                yield return "#0000";
                yield return "#00000";
                yield return "#0000000";
                yield return "#ABG";
                yield return "#ABCDEG";
                yield return "0";
                yield return "00";
                yield return "0000";
                yield return "00000";
                yield return "0000000";
                yield return "ABG";
                yield return "ABCDEG";
                yield return "#***";
                yield return "#******";
                yield return "#ABC 123";
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="HexadecimalColorTests.Parse_ValidFormat_ReturnsEquivalentHexadecimalColor(string)"/> test.
        /// </summary>
        private static IEnumerable<TestCaseData> ValidHexadecimalColorStrings
        {
            get
            {
                yield return new TestCaseData("#000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("#000000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" #000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" #000000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("#000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("#000000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" #000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" #000000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("000000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" 000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" 000000").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData("000000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" 000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
                yield return new TestCaseData(" 000000 ").Returns(new HexadecimalColor(0x00, 0x00, 0x00));
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="HexadecimalColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor, decimal)"/> test.
        /// </summary>
        private static IEnumerable<TestCaseData> LuminanceBytes
        {
            get
            {
                yield return new TestCaseData((byte)0x00, 0.25M).Returns(0x40);
                yield return new TestCaseData((byte)0x00, -0.25M).Returns(0x00);
                yield return new TestCaseData((byte)0x00, 1.25M).Returns(0xFF);
                yield return new TestCaseData((byte)0x00, -1.25M).Returns(0x00);
                yield return new TestCaseData((byte)0xFF, 0.25M).Returns(0xFF);
                yield return new TestCaseData((byte)0xFF, -0.25M).Returns(0xBF);
                yield return new TestCaseData((byte)0xFF, 1.25M).Returns(0xFF);
                yield return new TestCaseData((byte)0xFF, -1.25M).Returns(0x00);
                yield return new TestCaseData((byte)0x13, decimal.Zero).Returns(0x13);
                yield return new TestCaseData((byte)0x13, 0.001953124M).Returns(0x13);
                yield return new TestCaseData((byte)0x13, -0.001953124M).Returns(0x13);
                yield return new TestCaseData((byte)0x13, 0.001953125M).Returns(0x14);
                yield return new TestCaseData((byte)0x13, -0.0019531251M).Returns(0x12);
            }
        }

        /// <summary>
        /// Gets the test data for the <see cref="HexadecimalColorTests.Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor, decimal)"/> test.
        /// </summary>
        private static IEnumerable<TestCaseData> LuminanceHexadecimalColors
        {
            get
            {
                yield return new TestCaseData(new HexadecimalColor(0x00, 0x00, 0x00), 0.25M);
                yield return new TestCaseData(new HexadecimalColor(0x00, 0x00, 0x00), -0.25M);
                yield return new TestCaseData(new HexadecimalColor(0x00, 0x00, 0x00), 1.25M);
                yield return new TestCaseData(new HexadecimalColor(0x00, 0x00, 0x00), -1.25M);
                yield return new TestCaseData(new HexadecimalColor(0xFF, 0xFF, 0xFF), 0.25M);
                yield return new TestCaseData(new HexadecimalColor(0xFF, 0xFF, 0xFF), -0.25M);
                yield return new TestCaseData(new HexadecimalColor(0xFF, 0xFF, 0xFF), 1.25M);
                yield return new TestCaseData(new HexadecimalColor(0xFF, 0xFF, 0xFF), -1.25M);
                yield return new TestCaseData(new HexadecimalColor(0x04, 0x13, 0x84), decimal.Zero);
                yield return new TestCaseData(new HexadecimalColor(0x04, 0x13, 0x84), 0.001953124M);
                yield return new TestCaseData(new HexadecimalColor(0x04, 0x13, 0x84), -0.001953124M);
                yield return new TestCaseData(new HexadecimalColor(0x04, 0x13, 0x84), 0.001953125M);
                yield return new TestCaseData(new HexadecimalColor(0x04, 0x13, 0x84), -0.0019531251M);
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances that are not equal to the color created with <see cref="Red"/>, <see cref="Green"/>, and <see cref="Blue"/>.
        /// </summary>
        private static IEnumerable<HexadecimalColor> NotEqualHexadecimalColors
        {
            get
            {
                yield return null;
                yield return new HexadecimalColor(0xDE, 0xAD, 0xBE); // none equal
                yield return new HexadecimalColor(Red, 0xAD, 0xBE); // R equal
                yield return new HexadecimalColor(0xDE, Green, 0xBE); // G equal
                yield return new HexadecimalColor(0xDE, 0xAD, Blue); // B equal
                yield return new HexadecimalColor(Red, Green, 0xBE); // RG equal
                yield return new HexadecimalColor(Red, 0xAD, Blue); // RB equal
                yield return new HexadecimalColor(0xDE, Blue, Green); // BG equal
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances including <see cref="NonNullHexadecimalColors"/> and <c>null</c>.
        /// </summary>
        private static IEnumerable<HexadecimalColor> HexadecimalColors => NonNullHexadecimalColors.Concat(new[]
        {
            new HexadecimalColor(Red, Green, Blue), // equal
        });

        /// <summary>
        /// Gets a collection of <see cref="HexadecimalColor"/> instances including <see cref="NotEqualHexadecimalColors"/> and the color created with <see cref="Red"/>, <see cref="Green"/>, and <see cref="Blue"/>.
        /// </summary>
        private static IEnumerable<HexadecimalColor> NonNullHexadecimalColors => NotEqualHexadecimalColors.Where(hc => hc != null);

        /// <summary>
        /// Gets a collection of <see cref="object"/> instances including <see cref="NotEqualHexadecimalColors"/> and an object of another type.
        /// </summary>
        private static IEnumerable<object> NotEqualObjects => NotEqualHexadecimalColors.Concat(new[]
        {
            new object(),
        });

        /// <summary>
        /// Verify the constructor sets the public properties.
        /// </summary>
        [Test]
        public void Ctor_SetsProperties()
        {
            // Act
            var result = new HexadecimalColor(Red, Green, Blue);

            // Assert
            Assert.That(result.Red, Is.EqualTo(Red));
            Assert.That(result.Green, Is.EqualTo(Green));
            Assert.That(result.Blue, Is.EqualTo(Blue));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.RedSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Red"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [TestCaseSource("Channels")]
        public void RedSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(colorChannel, 0x00, 0x00);
            var expectedResult = HexadecimalColor.PercentageOfMaximumSaturation(target.Red);

            // Act
            var result = target.RedSaturation;

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GreenSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Green"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [TestCaseSource("Channels")]
        public void GreenSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(0x00, colorChannel, 0x00);
            var expectedResult = HexadecimalColor.PercentageOfMaximumSaturation(target.Green);

            // Act
            var result = target.GreenSaturation;

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.BlueSaturation"/> property calculates the
        /// correct saturation based on the <see cref="HexadecimalColor.Blue"/> property.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [TestCaseSource("Channels")]
        public void BlueSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var target = new HexadecimalColor(0x00, 0x00, colorChannel);
            var expectedResult = HexadecimalColor.PercentageOfMaximumSaturation(target.Blue);

            // Act
            var result = target.BlueSaturation;

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> does returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [TestCaseSource("NonNullHexadecimalColors")]
        public void EqualityOperator_ChannelsNotEqual_IsFalse(HexadecimalColor notEqual)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target == notEqual;

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> returns <c>true</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        [Test]
        public void EqualityOperator_AllChannelsEqual_IsTrue()
        {
            // Arrange
            var equal = new HexadecimalColor(Red, Green, Blue);
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target == equal;

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the equality operator (==) for <see cref="HexadecimalColor"/> returns <c>true</c> when both operands are <c>null</c>.
        /// </summary>
        [Test]
        public void EqualityOperator_BothOperandsNull_IsTrue()
        {
            // Arrange
            HexadecimalColor equal = null;
            HexadecimalColor target = null;

            // Act
            var result = target == equal;

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the equality operator (==) is commutative for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [TestCaseSource("HexadecimalColors")]
        public void EqualityOperator_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result1 = target == compare;
            var result2 = compare == target;

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [TestCaseSource("HexadecimalColors")]
        public void EqualityOperator_IsReflexive(HexadecimalColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the equality operator (==) is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        [Test]
        public void EqualityOperator_SameInstance_IsReflexive()
        {
            // Arrange
            var color = new HexadecimalColor(Red, Green, Blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>true</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [TestCaseSource("NonNullHexadecimalColors")]
        public void InequalityOperator_ChannelsNotEqual_IsTrue(HexadecimalColor notEqual)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target == notEqual;

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>false</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        [Test]
        public void InequalityOperator_AllChannelsEqual_IsFalse()
        {
            // Arrange
            var equal = new HexadecimalColor(Red, Green, Blue);
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target != equal;

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the inequality operator (!=) for <see cref="HexadecimalColor"/> returns <c>false</c> when both operands are <c>null</c>.
        /// </summary>
        [Test]
        public void InequalityOperator_BothOperandsNull_IsFalse()
        {
            // Arrange
            HexadecimalColor equal = null;
            HexadecimalColor target = null;

            // Act
            var result = target != equal;

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is commutative for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [TestCaseSource("HexadecimalColors")]
        public void InequalityOperator_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result1 = target != compare;
            var result2 = compare != target;

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for <see cref="HexadecimalColor"/> instances and <c>null</c>.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [TestCaseSource("HexadecimalColors")]
        public void InequalityOperator_IsReflexive(HexadecimalColor color)
        {
#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718
            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the inequality operator (!=) is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        [Test]
        public void InequalityOperator_SameInstance_IsReflexive()
        {
            // Arrange
            var color = new HexadecimalColor(Red, Green, Blue);

#pragma warning disable CS1718 // disabled because the purpose of the test is to compare the object to itself
            // Act
            var result = color != color;
#pragma warning restore CS1718

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws an <see cref="ArgumentNullException"/> when it is passed <c>null</c>.
        /// </summary>
        [Test]
        public void Parse_NullInput_Throws()
        {
            // Arrange
            string target = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => HexadecimalColor.Parse(target));

            // Assert
            Assert.That(expected, Is.Not.Null);
            Assert.That(expected.ParamName, Is.EqualTo("str"));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        [TestCaseSource("InvalidHexadecimalColorStrings")]
        public void Parse_InvalidInput_Throws(string target)
        {
            // Act
            var expected = Assert.Throws<FormatException>(() => HexadecimalColor.Parse(target));

            // Assert
            Assert.That(expected, Is.Not.Null);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Parse(string)"/> method throws a <see cref="FormatException"/> when it is passed string data it cannot parse.
        /// </summary>
        /// <param name="target">The target string to parse.</param>
        /// <returns>The parsed <see cref="HexadecimalColor"/> for NUnit to verify.</returns>
        [TestCaseSource("ValidHexadecimalColorStrings")]
        public HexadecimalColor Parse_ValidFormat_ReturnsEquivalentHexadecimalColor(string target)
        {
            // Act
            var result = HexadecimalColor.Parse(target);

            // Assert
            return result;
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.PercentageOfMaximumSaturation(byte)"/> method calculates the
        /// percentage of <see cref="byte.MaxValue"/> of the specified <paramref name="colorChannel"/>.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        [TestCaseSource("Channels")]
        public void PercentageOfMaximumSaturation_CalculatesPercentage(byte colorChannel)
        {
            // Arrange
            var expectedResult = colorChannel / (decimal)byte.MaxValue;

            // Act
            var result = HexadecimalColor.PercentageOfMaximumSaturation(colorChannel);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Luminance(byte, decimal)"/> method returns a byte modified brighter or darker by the
        /// specified <paramref name="percentage"/>.
        /// </summary>
        /// <param name="colorChannel">The color channel to test.</param>
        /// <param name="percentage">The percentage of maximum by which to brighten/darken the <paramref name="colorChannel"/>.</param>
        /// <returns>The new <see cref="byte"/> for NUnit to verify.</returns>
        [TestCaseSource("LuminanceBytes")]
        public byte Luminance_Static_ReturnsModifiedByte(byte colorChannel, decimal percentage)
        {
            // Act
            var result = HexadecimalColor.Luminance(colorChannel, percentage);

            // Assert
            return result;
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Luminance(decimal)"/> method returns a new instance of <see cref="HexadecimalColor"/> with
        /// each channel modified brighter or darker by the specified <paramref name="percentage"/>.
        /// </summary>
        /// <param name="target">The color to test.</param>
        /// <param name="percentage">The percentage of maximum by which to brighten/darken each channel on the <paramref name="target"/>.</param>
        [TestCaseSource("LuminanceHexadecimalColors")]
        public void Luminance_ReturnsNewInstanceWithModifiedChannels(HexadecimalColor target, decimal percentage)
        {
            // Act
            var result = target.Luminance(percentage);

            // Assert
            Assert.That(result, Is.Not.SameAs(target));
            Assert.That(result.Red, Is.EqualTo(HexadecimalColor.Luminance(target.Red, percentage)));
            Assert.That(result.Green, Is.EqualTo(HexadecimalColor.Luminance(target.Green, percentage)));
            Assert.That(result.Blue, Is.EqualTo(HexadecimalColor.Luminance(target.Blue, percentage)));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.ToString"/> method formats its output as a hexadecimal string with a leading '#' character.
        /// </summary>
        [Test]
        public void ToString_FormatsAsHexString()
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);
            var expectedResult = "#6A00FF";

            // Act
            var result = target.ToString();

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method returns <c>false</c> if <paramref name="notEqual"/> is not an instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal.
        /// </summary>
        /// <param name="notEqual">A value that is expected the be not equal to the default test color.</param>
        [TestCaseSource("NotEqualObjects")]
        public void EqualsMethod_ChannelsNotEqual_IsFalse(object notEqual)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target.Equals(notEqual);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method returns <c>true</c> when the argument is another instance of <see cref="HexadecimalColor"/> where all color saturation channels are equal to the original.
        /// </summary>
        [Test]
        public void EqualsMethod_AllChannelsEqual_IsTrue()
        {
            // Arrange
            var equal = new HexadecimalColor(Red, Green, Blue);
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target.Equals(equal);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is commutative for non-<c>null</c> <see cref="HexadecimalColor"/> instances.
        /// </summary>
        /// <param name="compare">The <see cref="HexadecimalColor"/> to verify for commutative equality.</param>
        [TestCaseSource("NonNullHexadecimalColors")]
        public void EqualsMethod_IsCommutative(HexadecimalColor compare)
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result1 = target.Equals(compare);
            var result2 = compare.Equals(target);

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is reflexive for non-<c>null</c> <see cref="HexadecimalColor"/> instances.
        /// </summary>
        /// <param name="color">The <see cref="HexadecimalColor"/> to verify for reflexive equality.</param>
        [TestCaseSource("NonNullHexadecimalColors")]
        public void EqualsMethod_IsReflexive(HexadecimalColor color)
        {
            // Act
            var result = color.Equals(color);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.Equals(object)"/> method is reflexive for the same instance of <see cref="HexadecimalColor"/>.
        /// </summary>
        [Test]
        public void EqualsMethod_SameInstance_IsReflexive()
        {
            // Arrange
            var color = new HexadecimalColor(Red, Green, Blue);

#pragma warning disable CS1718 // disabled because the purpose of the test if to compare the object to itself
            // Act
            var result = color == color;
#pragma warning restore CS1718

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GetHashCode"/> method returns a value based on the sum of the color channels,
        /// with red being offset by 0x1000, green being offset 0x100, and blue with no offset.
        /// </summary>
        [Test]
        public void GetHashCode_IsSumOfChannelsWithOffset()
        {
            // Arrange
            var target = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result = target.GetHashCode();

            // Assert
            Assert.That(result, Is.EqualTo((Red * 0x10000) + (Green * 0x100) + Blue));
        }

        /// <summary>
        /// Verify the <see cref="HexadecimalColor.GetHashCode"/> method returns the same value for <see cref="HexadecimalColor"/> instances that are equal.
        /// </summary>
        [Test]
        public void GetHashCode_ColorsAreEqual_AreEqual()
        {
            // Arrange
            var target1 = new HexadecimalColor(Red, Green, Blue);
            var target2 = new HexadecimalColor(Red, Green, Blue);

            // Act
            var result1 = target1.GetHashCode();
            var result2 = target2.GetHashCode();

            // Assert
            Assert.That(result1, Is.EqualTo(result2));
        }
    }
}
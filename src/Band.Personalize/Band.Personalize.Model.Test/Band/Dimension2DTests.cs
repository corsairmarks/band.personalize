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

namespace Band.Personalize.Model.Test.Band
{
    using System.Collections.Generic;
    using Library.Band;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="Dimension2D"/> class.
    /// </summary>
    public class Dimension2DTests
    {
        /// <summary>
        /// Sample width and height values for testing.
        /// </summary>
        public static readonly IEnumerable<object[]> WidthAndHeight = new[]
        {
            new object[] { 50, 25, },
            new object[] { 13, 13, },
            new object[] { 25, 50, },
            new object[] { 0, 0, },
            new object[] { -50, -25, },
            new object[] { -13, -13, },
            new object[] { -25, -50, },
        };

        /// <summary>
        /// Verify the constructor of <see cref="Dimension2D"/> creates an instance with the provided arguments as properties.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void Ctor_CreatesInstance(int width, int height)
        {
            // Act
            var result = new Dimension2D(width, height);

            // Assert
            Assert.Equal(width, result.Width);
            Assert.Equal(height, result.Height);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method returns <c>true</c> when two intances with equal
        /// <see cref="Dimension2D.Width"/> and <see cref="Dimension2D.Height"/> properties are tested.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_AllPropertiesEqual_IsTrue(int width, int height)
        {
            // Arange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width, height);

            // Act
            var result = target1.Equals(target2);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method returns <c>false</c> when two intances with unequal
        /// <see cref="Dimension2D.Width"/> properties are tested.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_WidthNotEqual_IsFalse(int width, int height)
        {
            // Arange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width + height + 2, height);

            // Act
            var result = target1.Equals(target2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method returns <c>false</c> when two intances with unequal
        /// <see cref="Dimension2D.Height"/> properties are tested.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_HeightNotEqual_IsFalse(int width, int height)
        {
            // Arange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width, height + width + 5);

            // Act
            var result = target1.Equals(target2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method returns <c>false</c> when two intances with
        /// no equal properties are tested.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_NoPropertiesEqual_IsFalse(int width, int height)
        {
            // Arange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width + height + 2, height + width + 5);

            // Act
            var result = target1.Equals(target2);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method is reflexive.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_SameInstance_IsReflexive(int width, int height)
        {
            // Arrange
            var target = new Dimension2D(width, height);

            // Act
            var result = target.Equals(target);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.Equals(object)"/> method is commutative.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void EqualsMethod_IsCommutative(int width, int height)
        {
            // Arrange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width, height);

            // Act
            var result1 = target1.Equals(target2);
            var result2 = target2.Equals(target1);

            // Assert
            Assert.True(result1 == result2, "evaluation was not commutative");
        }

        /// <summary>
        /// Verify the <see cref="Dimension2D.GetHashCode"/> method returns the same value for <see cref="Dimension2D"/> instances that are equal.
        /// </summary>
        /// <param name="width">The width to test.</param>
        /// <param name="height">The height to test.</param>
        [Theory]
        [MemberData(nameof(WidthAndHeight))]
        public void GetHashCode_InstancesAreEqual_AreEqual(int width, int height)
        {
            // Arrange
            var target1 = new Dimension2D(width, height);
            var target2 = new Dimension2D(width, height);

            // Act
            var result1 = target1.GetHashCode();
            var result2 = target2.GetHashCode();

            // Assert
            Assert.True(result1 == result2);
        }
    }
}
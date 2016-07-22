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

namespace Band.Personalize.Model.Test.Repository
{
    using System;
    using Implementation.Repository;
    using Microsoft.Band;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BandRepository"/> class.
    /// </summary>
    public class BandRepositoryTests
    {
        /// <summary>
        /// The <see cref="Mock"/> repository.
        /// </summary>
        private static readonly MockRepository MockRepository = new MockRepository(MockBehavior.Strict);

        /// <summary>
        /// Verify the <see cref="BandRepository(IBandClientManager)"/> constructor throws an
        /// <see cref="ArgumentNullException"/> when the <see cref="IBandClientManager"/> parameter is <c>null</c>.
        /// </summary>
        [Fact]
        public void Ctor_NullBandClientManager_Throws()
        {
            // Arrange
            IBandClientManager bandClientManager = null;

            // Act
            var expected = Assert.Throws<ArgumentNullException>(() => new BandRepository(bandClientManager));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandClientManager", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="BandRepository(IBandClientManager)"/> constructor creates an instance when provided valid parameter(s).
        /// </summary>
        [Fact]
        public void Ctor_CreatesInstance()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;

            // Act
            var result = new BandRepository(bandClientManager);

            // Assert
            Assert.NotNull(result);
        }
    }
}
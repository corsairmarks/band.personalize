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
    using System.Threading;
    using System.Threading.Tasks;
    using Library.Repository;
    using Microsoft.Band;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="IBandClientManagerExtensions"/> class.
    /// </summary>
    public class IBandClientManagerExtensionsTests
    {
        /// <summary>
        /// The <see cref="Mock"/> repository.
        /// </summary>
        private static readonly MockRepository MockRepository = new MockRepository(MockBehavior.Strict);

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformActionAsync(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="IBandClientManager"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformActionAsync_NullBandClientManager_Throws()
        {
            // Arrange
            IBandClientManager bandClientManager = null;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task> clientAction = (bc, t) => Task.CompletedTask;

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformActionAsync(bandInfo, token, clientAction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandClientManager", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformActionAsync(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="IBandInfo"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformActionAsync_NullBandInfo_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            IBandInfo bandInfo = null;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task> clientAction = (bc, t) => Task.CompletedTask;

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformActionAsync(bandInfo, token, clientAction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandInfo", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformActionAsync(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="Func{IBandClient, Task}"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformActionAsync_NullClientAction_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task> clientAction = null;

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformActionAsync(bandInfo, token, clientAction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("clientAction", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformActionAsync(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task})"/>
        /// method throws an <see cref="OperationCanceledException"/> when the <see cref="CancellationToken"/> parameter is already cancelled.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformActionAsync_Cancelled_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            Func<IBandClient, CancellationToken, Task> clientAction = (bc, t) => Task.CompletedTask;
            OperationCanceledException expected;
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.Cancel();

                // Act
                expected = await Assert.ThrowsAsync<OperationCanceledException>(() => bandClientManager.ConnectAndPerformActionAsync(bandInfo, cancellationTokenSource.Token, clientAction));
            }

            // Assert
            Assert.NotNull(expected);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformActionAsync(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task})"/>
        /// method connects to the <see cref="IBandInfo"/> using the <see cref="IBandClientManager"/> and returns a <see cref="Task"/> when all parameters are not <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact(Skip = "Moq alpha for UWP throws when accessing resources defined in a resx file from a Times.*() method")]
        public async Task ConnectAndPerformActionAsync_ConnectsAndPerformsAction()
        {
            // Arrange
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var mockBandClient = MockRepository.Create<IBandClient>();
            mockBandClient.Setup(bc => bc.Dispose());
            var bandClient = mockBandClient.Object;
            var mockBandClientManager = MockRepository.Create<IBandClientManager>();
            mockBandClientManager.Setup(bcm => bcm.ConnectAsync(It.IsIn(bandInfo))).Returns(Task.FromResult(bandClient));
            var bandClientManager = mockBandClientManager.Object;
            var token = new CancellationToken(false);
            var clientActionCallCount = 0;
            Func<IBandClient, CancellationToken, Task> clientAction = (bc, t) =>
            {
                Assert.StrictEqual(bandClient, bc);
                Assert.StrictEqual(token, t);
                clientActionCallCount++;

                return Task.CompletedTask;
            };

            // Act / Assert
            await bandClientManager.ConnectAndPerformActionAsync(bandInfo, token, clientAction);

            // Assert
            mockBandClientManager.Verify(bcm => bcm.ConnectAsync(It.IsAny<IBandInfo>()), Times.Never);
            mockBandClient.Verify(bc => bc.Dispose(), Times.Once);
            Assert.Equal(1, clientActionCallCount);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformFunctionAsync{T}(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task{T}})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="IBandClientManager"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformFunctionAsync_NullBandClientManager_Throws()
        {
            // Arrange
            IBandClientManager bandClientManager = null;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task<object>> clientFunction = (bc, t) => Task.FromResult(null as object);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformFunctionAsync(bandInfo, token, clientFunction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandClientManager", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformFunctionAsync{T}(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task{T}})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="IBandInfo"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformFunctionAsync_NullBandInfo_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            IBandInfo bandInfo = null;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task<object>> clientFunction = (bc, t) => Task.FromResult(null as object);

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformFunctionAsync(bandInfo, token, clientFunction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("bandInfo", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformFunctionAsync{T}(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task{T}})"/>
        /// method throws an <see cref="ArgumentNullException"/> when the <see cref="Func{IBandClient, Task}"/> parameter is <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformFunctionAsync_NullClientFunction_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var token = CancellationToken.None;
            Func<IBandClient, CancellationToken, Task<object>> clientFunction = null;

            // Act
            var expected = await Assert.ThrowsAsync<ArgumentNullException>(() => bandClientManager.ConnectAndPerformFunctionAsync(bandInfo, token, clientFunction));

            // Assert
            Assert.NotNull(expected);
            Assert.Equal("clientFunction", expected.ParamName);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformFunctionAsync{T}(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task{T}})"/>
        /// method throws an <see cref="OperationCanceledException"/> when the <see cref="CancellationToken"/> parameter is already cancelled.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact]
        public async Task ConnectAndPerformFunctionAsync_Cancelled_Throws()
        {
            // Arrange
            var bandClientManager = MockRepository.Create<IBandClientManager>().Object;
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            Func<IBandClient, CancellationToken, Task<object>> clientAction = (bc, t) => Task.FromResult(null as object);
            OperationCanceledException expected;
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                cancellationTokenSource.Cancel();

                // Act
                expected = await Assert.ThrowsAsync<OperationCanceledException>(() => bandClientManager.ConnectAndPerformActionAsync(bandInfo, cancellationTokenSource.Token, clientAction));
            }

            // Assert
            Assert.NotNull(expected);
        }

        /// <summary>
        /// Verify the <see cref="IBandClientManagerExtensions.ConnectAndPerformFunctionAsync{T}(IBandClientManager, IBandInfo, CancellationToken, Func{IBandClient, CancellationToken, Task{T}})"/>
        /// method connects to the <see cref="IBandInfo"/> using the <see cref="IBandClientManager"/> and returns a <see cref="Task{T}"/> when all parameters are not <c>null</c>.
        /// </summary>
        /// <returns>An asynchronous task that returns when the test is complete.</returns>
        [Fact(Skip = "Moq alpha for UWP throws when accessing resources defined in a resx file from a Times.*() method")]
        public async Task ConnectAndPerformFunctionAsync_ConnectsAndPerformsFunction()
        {
            // Arrange
            var bandInfo = MockRepository.Create<IBandInfo>().Object;
            var mockBandClient = MockRepository.Create<IBandClient>();
            mockBandClient.Setup(bc => bc.Dispose());
            var bandClient = mockBandClient.Object;
            var mockBandClientManager = MockRepository.Create<IBandClientManager>();
            mockBandClientManager.Setup(bcm => bcm.ConnectAsync(bandInfo)).Returns(Task.FromResult(bandClient));
            var bandClientManager = mockBandClientManager.Object;
            var token = new CancellationToken(false);
            var clientFunctionCallCount = 0;
            Func<IBandClient, CancellationToken, Task<object>> clientFunction = (bc, t) =>
            {
                Assert.StrictEqual(bandClient, bc);
                Assert.StrictEqual(token, t);
                clientFunctionCallCount++;

                return Task.FromResult(new object());
            };

            // Act / Assert
            var result = await bandClientManager.ConnectAndPerformFunctionAsync(bandInfo, token, clientFunction);

            // Assert
            mockBandClientManager.Verify(bcm => bcm.ConnectAsync(bandInfo), Times.Once);
            mockBandClient.Verify(bc => bc.Dispose(), Times.Once);
            Assert.Equal(1, clientFunctionCallCount);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<object>(result);
        }
    }
}
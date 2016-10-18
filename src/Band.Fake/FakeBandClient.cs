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

namespace Band.Fake
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Band;
    using Microsoft.Band.Notifications;
    using Microsoft.Band.Personalization;
    using Microsoft.Band.Sensors;
    using Microsoft.Band.Tiles;

    /// <summary>
    /// A fake implementation of <see cref="IBandClient"/>.
    /// </summary>
    public class FakeBandClient : IBandClient
    {
        /// <summary>
        /// The delay added to all Band operations.
        /// </summary>
        private readonly int delay;

        /// <summary>
        /// A lazy, fake <see cref="IBandPersonalizationManager"/>.
        /// </summary>
        private readonly Lazy<IBandPersonalizationManager> personalizationManager;

        /// <summary>
        /// The fake <see cref="IBandInfo"/> to which this client "connects."
        /// </summary>
        private readonly FakeBandInfo fakeBandInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandClient"/> class.  Defaults to a 0 millisecond delay.
        /// </summary>
        /// <param name="bandInfo">The <see cref="IBandInfo"/> to fake.</param>
        public FakeBandClient(IBandInfo bandInfo)
            : this(bandInfo, 0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBandClient"/> class.
        /// </summary>
        /// <param name="bandInfo">The <see cref="IBandInfo"/> to fake.</param>
        /// <param name="delay">The delay in milliseconds to be added to all Band operations.  Automatically sets values less than 0 to 0.</param>
        /// <param name="random">An instance of <see cref="Random"/> to use for choosing fake Band data.</param>
        public FakeBandClient(IBandInfo bandInfo, int delay, Random random)
        {
            if (bandInfo == null)
            {
                throw new ArgumentNullException(nameof(bandInfo));
            }
            else if (random == null)
            {
                random = new Random();
            }

            this.delay = delay >= 0 ? delay : 0;

            var fakeBandInfo = bandInfo as FakeBandInfo;
            if (fakeBandInfo != null)
            {
                this.fakeBandInfo = fakeBandInfo;
            }
            else
            {
                this.fakeBandInfo = new FakeBandInfo(bandInfo)
                {
                    FirmwareVersion = random.Next(1, 9) + "." + random.Next(0, 99) + "." + random.Next(0, 9999),
                    HardwareVersion = random.Next(1, 99).ToString(),
                };
            }

            this.personalizationManager = new Lazy<IBandPersonalizationManager>(() => new FakePersonalizationManager(this.fakeBandInfo, delay, random));
        }

        /// <summary>
        /// Gets the notification manager.
        /// </summary>
        /// <exception cref="NotImplementedException">Always.</exception>
        public IBandNotificationManager NotificationManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the personalization manager.
        /// </summary>
        public IBandPersonalizationManager PersonalizationManager
        {
            get { return this.personalizationManager.Value; }
        }

        /// <summary>
        /// Gets the sensor manager.
        /// </summary>
        /// <exception cref="NotImplementedException">Always.</exception>
        public IBandSensorManager SensorManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the tile manager.
        /// </summary>
        /// <exception cref="NotImplementedException">Always.</exception>
        public IBandTileManager TileManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// No operations.
        /// </remarks>
        public void Dispose()
        {
        }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <returns>An asynchronous task that returns the firmware version when it completes.</returns>
        public Task<string> GetFirmwareVersionAsync()
        {
            return this.GetFirmwareVersionAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the firmware version when it completes.</returns>
        public Task<string> GetFirmwareVersionAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task
                .Delay(this.delay, token)
                .ContinueWith(
                    t =>
                    {
                        this.ThrowIfNotConnected();

                        return this.fakeBandInfo.FirmwareVersion;
                    },
                    token);
        }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <returns>An asynchronous task that returns the hardware version when it completes.</returns>
        public Task<string> GetHardwareVersionAsync()
        {
            return this.GetHardwareVersionAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to observe.</param>
        /// <returns>An asynchronous task that returns the hardware version when it completes.</returns>
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Task
                .Delay(this.delay, token)
                .ContinueWith(
                    t =>
                    {
                        this.ThrowIfNotConnected();

                        return this.fakeBandInfo.HardwareVersion;
                    },
                    token);
        }

        /// <summary>
        /// Throw an instance of <see cref="BandIOException"/> if <see cref="fakeBandInfo"/> is not connected.  This behavior is consistent with the Band SDK, but requires reflection to instantiate the exception.
        /// </summary>
        /// <exception cref="BandIOException">If <see cref="fakeBandInfo"/> is not connected.</exception>
        private void ThrowIfNotConnected()
        {
            if (!this.fakeBandInfo.IsConnected)
            {
                var ctors = typeof(BandIOException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                var ctor = ctors.SingleOrDefault(c => !c.GetParameters().Any());
                if (ctor != null)
                {
                    throw (BandIOException)ctor.Invoke(null);
                }

                throw new MissingMethodException($"No public parameterless constructor for {typeof(BandIOException)}.");
            }
        }
    }
}
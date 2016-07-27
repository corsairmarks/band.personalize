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

namespace Band.Personalize.Model.Test
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;
    using Xunit;

    /// <summary>
    /// Helper methods for UI-related unit tests.
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Verify that two <see cref="WriteableBitmap"/> instances are equal by comparing each <see cref="WriteableBitmap.PixelBuffer"/>.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
        /// <exception cref="ArgumentNullException"><paramref name="expected"/> or <paramref name="actual"/> is <c>null</c>.</exception>
        public static void AssertWriteableBitmapPixelBuffersEqual(WriteableBitmap expected, WriteableBitmap actual)
        {
            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected));
            }
            else if (actual == null)
            {
                throw new ArgumentNullException(nameof(actual));
            }

            var expectedBytes = expected.PixelBuffer.ToArray();
            var actualBytes = actual.PixelBuffer.ToArray();

            Assert.Equal(expectedBytes, actualBytes);
        }
    }
}
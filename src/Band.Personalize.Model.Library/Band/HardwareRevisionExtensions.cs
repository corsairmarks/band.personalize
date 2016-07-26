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

namespace Band.Personalize.Model.Library.Band
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Extension methods for the <see cref="HardwareRevision"/> enumeration.
    /// </summary>
    public static class HardwareRevisionExtensions
    {
        /// <summary>
        /// A map of hardware revision to allowed Me Tile image dimensions.  The last element in the collection is the preferred default.
        /// </summary>
        private static readonly IReadOnlyDictionary<HardwareRevision, IReadOnlyCollection<Dimension2D>> BandHardwareRevisionToMeTileImageDimensionsMap = new ReadOnlyDictionary<HardwareRevision, IReadOnlyCollection<Dimension2D>>(new Dictionary<HardwareRevision, IReadOnlyCollection<Dimension2D>>
        {
            { HardwareRevision.Unknown, new Dimension2D[0] },
            { HardwareRevision.Band, new ReadOnlyCollection<Dimension2D>(new[] { new Dimension2D(310, 102), }) },
            { HardwareRevision.Band2, new ReadOnlyCollection<Dimension2D>(new[] { new Dimension2D(310, 102), new Dimension2D(310, 128), }) },
        });

        /// <summary>
        /// Convert the specific hardware version number into a major hardware revision level.
        /// </summary>
        /// <param name="version">The specific hardware version.</param>
        /// <returns>The major <see cref="HardwareRevision"/> for this specific <paramref name="version"/>.</returns>
        /// <remarks>As defined by Microsoft, the origin Microsoft Band is represented by hardware version string “19” or lower, whereas Microsoft Band 2 is represented by Hardware version string “20” or higher.</remarks>
        public static HardwareRevision ToHardwareRevision(this int version)
        {
            return version < 20
                ? HardwareRevision.Band
                : HardwareRevision.Band2;
        }

        /// <summary>
        /// Convert the specific hardware version number into a major hardware revision level, or return <see cref="HardwareRevision.Unknown"/>
        /// when <paramref name="version"/> is <c>null</c>.
        /// </summary>
        /// <param name="version">The specific hardware version.</param>
        /// <returns>If <paramref name="version"/> has a vlue, the major <see cref="HardwareRevision"/> for this specific <paramref name="version"/>; otherwise, <see cref="HardwareRevision.Unknown"/>.</returns>
        public static HardwareRevision ToHardwareRevision(this int? version)
        {
            return version.HasValue
                ? ToHardwareRevision(version.Value)
                : HardwareRevision.Unknown;
        }

        /// <summary>
        /// Get the allowable Me Tile image dimensions for the <paramref name="hardwareRevision"/>.  The last element is the default (preferred) dimensions.
        /// </summary>
        /// <param name="hardwareRevision">The major hardware revision level.</param>
        /// <returns>A read-only collection of possible Me Tile image dimensions.</returns>
        /// <exception cref="NotImplementedException">When <paramref name="hardwareRevision"/> is a value that does not have dimension mappings.</exception>
        public static IReadOnlyCollection<Dimension2D> GetAllowedMeTileDimensions(this HardwareRevision hardwareRevision)
        {
            IReadOnlyCollection<Dimension2D> allowedDimensions;
            if (BandHardwareRevisionToMeTileImageDimensionsMap.TryGetValue(hardwareRevision, out allowedDimensions))
            {
                return allowedDimensions;
            }
            else
            {
                throw new NotImplementedException($"Unhandled {typeof(HardwareRevision)}: \"{hardwareRevision}\"");
            }
        }

        /// <summary>
        /// Get the default (preferred) Me Tile image dimensions for the <paramref name="hardwareRevision"/>.
        /// </summary>
        /// <param name="hardwareRevision">The major hardware revision level.</param>
        /// <returns>A read-only collection of possible Me Tile image dimensions.</returns>
        /// <exception cref="NotImplementedException">When <paramref name="hardwareRevision"/> is a value that does not have dimension mappings.</exception>
        public static Dimension2D GetDefaultMeTileDimensions(this HardwareRevision hardwareRevision)
        {
            return GetAllowedMeTileDimensions(hardwareRevision).LastOrDefault();
        }
    }
}
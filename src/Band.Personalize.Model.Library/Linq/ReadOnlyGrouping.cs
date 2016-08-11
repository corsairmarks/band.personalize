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

namespace Band.Personalize.Model.Library.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a read-only collection of objects that have a common key.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of the key of the <see cref="IGrouping{TKey, TElement}"/>.  This type parameter is covariant.
    /// That is, you can use either the type you specified or any type that is more derived.
    /// For more information about covariance and contravariance, see Covariance and
    /// Contravariance in Generics.
    /// </typeparam>
    /// <typeparam name="TElement">The type of the values in the <see cref="IGrouping{TKey, TElement}"/>.</typeparam>
    public class ReadOnlyGrouping<TKey, TElement> : ReadOnlyCollection<TElement>, IGrouping<TKey, TElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyGrouping{TKey, TElement}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="list">The list to wrap.</param>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
        public ReadOnlyGrouping(TKey key, IList<TElement> list)
            : base(list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            this.Key = key;
        }

        /// <summary>
        /// Gets the key of the <see cref="IGrouping{TKey, TElement}"/>.
        /// </summary>
        public TKey Key { get; }
    }
}
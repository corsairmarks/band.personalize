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

namespace Band.Personalize.Model.Implementation.Repository
{
    using System;
    using Library.Color;
    using Newtonsoft.Json;

    /// <summary>
    /// Converts an <see cref="ArgbColor"/> to and from JSON.
    /// </summary>
    public class ArgbColorJsonConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified <paramref name="objectType"/>.
        /// </summary>
        /// <param name="objectType"><see cref="Type"/> of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified <paramref name="objectType"/>; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(ArgbColor))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType"><see cref="Type"/> of the <paramref name="existingValue"/>.</param>
        /// <param name="existingValue">The existing value of <see cref="object"/> being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ArgbColor color;
            return ArgbColor.TryFromArgbString(serializer.Deserialize<string>(reader), out color)
                ? color
                : null;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value != null ? ((ArgbColor)value).ToString() : string.Empty);
        }
    }
}
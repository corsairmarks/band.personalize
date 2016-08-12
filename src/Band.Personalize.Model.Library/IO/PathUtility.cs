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

namespace Band.Personalize.Model.Library.IO
{
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Utility methods for maniputlating filepaths.
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// A regular expression that identifies one or more contiguous whitespace characters.
        /// </summary>
        private static readonly Regex WhiteSpace = new Regex("\\s+", RegexOptions.Compiled);

        /// <summary>
        /// Strip invalid filename characters from a string.
        /// </summary>
        /// <param name="input">The <see cref="string"/> from which to remove the invalid filename characters.</param>
        /// <returns>A string that does not contain any characters identified by <see cref="Path.GetInvalidFileNameChars()"/>.</returns>
        public static string StripInvalidFileNameCharacters(this string input)
        {
            if (input == null)
            {
                return null;
            }

            return Regex.Replace(input, $"[{string.Join(string.Empty, Path.GetInvalidFileNameChars())}]", string.Empty);
        }

        /// <summary>
        /// Replace any substrings of one o more whitespace characters with a single dash character.
        /// </summary>
        /// <param name="input">The <see cref="string"/> in which to replace the whitespace characters.</param>
        /// <returns>A <see cref="string"/> where each whitespace substring has been replaced with a dash.</returns>
        public static string ReplaceWhiteSpaceWithDash(this string input)
        {
            if (input == null)
            {
                return null;
            }

            return WhiteSpace.Replace(input, "-");
        }
    }
}
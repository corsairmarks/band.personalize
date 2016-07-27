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

namespace Band.Personalize.App.Universal.ViewModels.Design
{
    using System.Threading.Tasks;

    /// <summary>
    /// Constants used for design-time stub classes.
    /// </summary>
    public static class StubConstants
    {
        /// <summary>
        /// The default amount of milliseconds an asynchronous stub operation should <see cref="Task.Delay(int)"/> before returning.
        /// </summary>
        public const int DefaultAsyncDelayMilliseconds = 1500;
    }
}
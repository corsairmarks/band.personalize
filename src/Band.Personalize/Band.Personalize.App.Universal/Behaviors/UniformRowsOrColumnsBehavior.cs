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

namespace Band.Personalize.App.Universal.Behaviors
{
    using System;
    using Microsoft.Xaml.Interactivity;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// A behavior for <see cref="ItemsWrapGrid"/> the stretches in the direction of the <see cref="ItemsWrapGrid.Orientation"/> property.
    /// </summary>
    public class UniformRowsOrColumnsBehavior : Behavior<ItemsWrapGrid>
    {
        /// <summary>
        /// Called after the behavior is attached to the <see cref="Behavior.AssociatedObject"/>.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SizeChanged += OnSizeChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its <see cref="Behavior.AssociatedObject"/>.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SizeChanged -= OnSizeChanged;
        }

        /// <summary>
        /// Represents the method that will handle the SizeChanged event.
        /// See also <seealso cref="SizeChangedEventHandler"/>.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var senderItemsWrapGrid = sender as ItemsWrapGrid;
            if (senderItemsWrapGrid != null && e != null)
            {
                switch (senderItemsWrapGrid.Orientation)
                {
                    case Orientation.Horizontal:
                        // tiles across, so MaximumRowsOrColumns represents the maximum number of columns
                        // thus the calculation is total width / columns
                        if (e.NewSize.Width != e.PreviousSize.Width)
                        {
                            var tileWidth = Math.Floor(e.NewSize.Width / senderItemsWrapGrid.MaximumRowsOrColumns);
                            senderItemsWrapGrid.ItemHeight = tileWidth;
                            senderItemsWrapGrid.ItemWidth = tileWidth;
                        }

                        break;
                    case Orientation.Vertical:
                        // tiles down, so MaximumRowsOrColumns represents the maximum number of rows
                        // thus the calculation is total height / rows
                        if (e.NewSize.Height != e.PreviousSize.Height)
                        {
                            var tileHeight = Math.Floor(e.NewSize.Height / senderItemsWrapGrid.MaximumRowsOrColumns);
                            senderItemsWrapGrid.ItemHeight = tileHeight;
                            senderItemsWrapGrid.ItemWidth = tileHeight;
                        }

                        break;
                    default:
                        throw new NotImplementedException($"Unhandled {senderItemsWrapGrid.Orientation.GetType()}: \"{senderItemsWrapGrid.Orientation}\"");
                }
            }
        }
    }
}
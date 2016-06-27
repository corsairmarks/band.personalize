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

namespace Band.Personalize.App.Universal.Controls
{
    using System;
    using Windows.ApplicationModel;
    using Windows.Foundation;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Positions child elements sequentially from left to right or top to bottom. When
    /// elements in a row (horizontal) or column (vertical) exceed the maximum, elements
    /// are positioned in the next row or column. Elements are scaled to fill available
    /// space assuming there were the specified number of elements in each row or column.
    /// </summary>
    /// <remarks>
    /// The class should flow in the direction of "infinity" for <see cref="ScrollViewer"/>
    /// attached properties.  It is impossible to divide "infinity" into a collection of equal
    /// parts for scaling.
    /// </remarks>
    public class UniformItemsWrapGrid : Panel
    {
        /// <summary>
        /// Gets the <see cref="MaximumRowsOrColumns"/> dependency property.
        /// </summary>
        public static DependencyProperty MaximumRowsOrColumnsProperty { get; } = DependencyProperty.Register("MaximumRowsOrColumns", typeof(int), typeof(UniformItemsWrapGrid), new PropertyMetadata(1, OnMaximumRowsOrColumnsChanged));

        /// <summary>
        /// Gets the <see cref="Orientation"/> dependency property.
        /// </summary>
        public static DependencyProperty OrientationProperty { get; } = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(UniformItemsWrapGrid), null);

        /// <summary>
        /// Gets or sets a value that influences the wrap point, also accounting for <see cref="Orientation"/>.
        /// </summary>
        public int MaximumRowsOrColumns
        {
            get { return (int)this.GetValue(MaximumRowsOrColumnsProperty); }
            set { this.SetValue(MaximumRowsOrColumnsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dimension by which child elements are stacked.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Provides the behavior for the Measure pass of the layout cycle. Classes can override
        /// this method to define their own Measure pass behavior.
        /// </summary>
        /// <param name="availableSize">
        /// The available size that this object can give to child objects. Infinity can be
        /// specified as a value to indicate that the object will size to whatever content
        /// is available.
        /// </param>
        /// <returns>
        /// The size that this object determines it needs during layout, based on its calculations
        /// of the allocated sizes for child objects or based on other considerations such
        /// as a fixed container size.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            bool isInfiniteHeight = double.IsInfinity(availableSize.Height);
            bool isInfiniteWidth = double.IsInfinity(availableSize.Width);

            // HACK: disable during design, attempting to retrieve the ApplicationView causes access violations
            if (!DesignMode.DesignModeEnabled)
            {
                var appView = ApplicationView.GetForCurrentView();

                // TODO: what if infinity x infinity? MaxItemWidthAndHeight...?
                if (isInfiniteHeight && isInfiniteWidth)
                {
                    availableSize = new Size(appView.VisibleBounds.Width, appView.VisibleBounds.Height);
                }
            }

            double finalWidth, finalHeight, itemWidthHeight;
            if (this.Orientation == Orientation.Horizontal)
            {
                if (isInfiniteWidth)
                {
                    // TODO: fix - assumes NOT infinite height
                    finalHeight = itemWidthHeight = availableSize.Height;
                    finalWidth = itemWidthHeight * this.Children.Count;
                }
                else
                {
                    itemWidthHeight = Math.Floor(availableSize.Width / this.MaximumRowsOrColumns);
                    var rowCount = Math.Ceiling((double)this.Children.Count / this.MaximumRowsOrColumns);
                    finalHeight = itemWidthHeight * rowCount;
                    finalWidth = availableSize.Width; // * cols
                }
            }
            else
            {
                if (isInfiniteHeight)
                {
                    // TODO: fix - assumes NOT infinite width
                    finalWidth = itemWidthHeight = availableSize.Width;
                    finalHeight = itemWidthHeight * this.Children.Count;
                }
                else
                {
                    itemWidthHeight = Math.Floor(availableSize.Height / this.MaximumRowsOrColumns);
                    var columnCount = Math.Ceiling((double)this.Children.Count / this.MaximumRowsOrColumns);
                    finalWidth = itemWidthHeight * columnCount;
                    finalHeight = availableSize.Height; // * rows
                }
            }

            var itemSize = new Size(itemWidthHeight, itemWidthHeight);
            foreach (var child in this.Children)
            {
                child.Measure(itemSize);
            }

            return new Size(finalWidth, finalHeight);
        }

        /// <summary>
        /// Provides the behavior for the Arrange pass of layout. Classes can override this
        /// method to define their own Arrange pass behavior.
        /// </summary>
        /// <param name="finalSize">
        /// The final area within the parent that this object should use to arrange itself
        /// and its children.
        /// </param>
        /// <returns>The actual size that is used after the element is arranged in layout.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Orientation == Orientation.Horizontal)
            {
                var actualRows = Math.Ceiling((double)this.Children.Count / this.MaximumRowsOrColumns);
                var cellWidth = Math.Floor(finalSize.Width / this.MaximumRowsOrColumns);
                Size cellSize = new Size(cellWidth, cellWidth);
                int row = 0, col = 0;
                foreach (var child in this.Children)
                {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, cellSize.Height * row), cellSize));
                    var element = child as FrameworkElement;
                    if (element != null)
                    {
                        element.Height = cellSize.Height;
                        element.Width = cellSize.Width;
                    }

                    if (++col == this.MaximumRowsOrColumns)
                    {
                        row++;
                        col = 0;
                    }
                }
            }
            else
            {
                var actualColumns = Math.Ceiling((double)this.Children.Count / this.MaximumRowsOrColumns);
                var cellHeight = Math.Floor(finalSize.Height / this.MaximumRowsOrColumns);
                var cellSize = new Size(cellHeight, cellHeight);
                int row = 0, col = 0;
                foreach (var child in this.Children)
                {
                    child.Arrange(new Rect(new Point(cellSize.Width * col, cellSize.Height * row), cellSize));
                    var element = child as FrameworkElement;
                    if (element != null)
                    {
                        element.Height = cellSize.Height;
                        element.Width = cellSize.Width;
                    }

                    if (++row == this.MaximumRowsOrColumns)
                    {
                        col++;
                        row = 0;
                    }
                }
            }

            return finalSize;
        }

        /// <summary>
        /// The callback that is invoked when the effective property value of the <see cref="MaximumRowsOrColumns"/> property changes, matching <seealso cref="PropertyChangedCallback"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnMaximumRowsOrColumnsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            int cols = (int)e.NewValue;
            if (cols < 1)
            {
                ((UniformItemsWrapGrid)obj).MaximumRowsOrColumns = 1;
            }
        }
    }
}
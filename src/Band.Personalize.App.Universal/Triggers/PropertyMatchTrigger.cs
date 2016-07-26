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

namespace Band.Personalize.App.Universal.Triggers
{
    using System.Collections.Generic;
    using Windows.UI.Xaml;

    /// <summary>
    /// A custom visual state trigger that watches a property for changes.
    /// </summary>
    /// <typeparam name="TValue">The type of the property to watch.</typeparam>
    public class PropertyMatchTrigger<TValue> : StateTriggerBase
    {
        /// <summary>
        /// Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ExpectedValueProperty = DependencyProperty.Register(
            nameof(ExpectedValue),
            typeof(TValue),
            typeof(PropertyMatchTrigger<TValue>),
            new PropertyMetadata(null, OnTriggerChanged));

        /// <summary>
        /// Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(TValue),
            typeof(PropertyMatchTrigger<TValue>),
            new PropertyMetadata(null, OnTriggerChanged));

        /// <summary>
        /// Equality comparer dependency property.
        /// </summary>
        public static readonly DependencyProperty EqualityComparerProperty = DependencyProperty.Register(
            nameof(EqualityComparer),
            typeof(IEqualityComparer<TValue>),
            typeof(PropertyMatchTrigger<TValue>),
            new PropertyMetadata(EqualityComparer<TValue>.Default, OnTriggerChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMatchTrigger{TValue}"/> class.
        /// </summary>
        public PropertyMatchTrigger()
        {
            this.SetActive();
        }

        /// <summary>
        /// Gets or sets the property value that is considered Active.
        /// </summary>
        public TValue ExpectedValue
        {
            get { return (TValue)this.GetValue(ExpectedValueProperty); }
            set { this.SetValue(ExpectedValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value that is considered Active.
        /// </summary>
        public TValue Value
        {
            get { return (TValue)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the equality comparer for the <see cref="Value"/>.
        /// </summary>
        public IEqualityComparer<TValue> EqualityComparer
        {
            get { return (IEqualityComparer<TValue>)this.GetValue(EqualityComparerProperty); }
            set { this.SetValue(EqualityComparerProperty, value); }
        }

        /// <summary>
        /// Represents the <see cref="PropertyChangedCallback"/> that is invoked when the effective property value of the <see cref="ExpectedValue"/>,
        /// <see cref="Value"/>, or <see cref="EqualityComparer"/> dependency properties change.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = d as PropertyMatchTrigger<TValue>;
            if (that != null)
            {
                that.SetActive();
            }
        }

        /// <summary>
        /// Sets the value that indicates whether the state trigger is active, calculated by using the
        /// <see cref="EqualityComparer"/> to determin whether the <see cref="Value"/> is equal to <see cref="ExpectedValue"/>.
        /// </summary>
        private void SetActive()
        {
            this.SetActive(this.EqualityComparer.Equals(this.ExpectedValue, this.Value));
        }
    }
}
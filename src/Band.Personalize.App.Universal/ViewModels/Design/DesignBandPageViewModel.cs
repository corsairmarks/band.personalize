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
    using Model.Implementation.Repository;
    using Model.Library.Band;
    using Newtonsoft.Json;
    using Prism.Windows.AppModel;
    using Prism.Windows.Navigation;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;

    /// <summary>
    /// The design View Model for the Band Page.
    /// </summary>
    public class DesignBandPageViewModel : BandPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignBandPageViewModel"/> class.
        /// </summary>
        public DesignBandPageViewModel()
            : base(
                  NavigationServiceStub.Instance,
                  new ResourceLoaderAdapter(new ResourceLoader()),
                  BandPersonalizerStub.Instance,
                  new CustomThemeRepository(ApplicationData.Current.RoamingFolder, JsonSerializer.Create(new JsonSerializerSettings { Converters = new[] { new RgbColorJsonConverter(), }, })),
                  (id, m) => new PersistedTitledThemeViewModel(id, null, null))
        {
            this.OnNavigatedTo(
                new NavigatedToEventArgs
                {
                    Parameter = new BandStub
                    {
                        Name = "Band F0:F0",
                        ConnectionType = ConnectionType.Usb,
                        HardwareRevision = HardwareRevision.Band,
                        HardwareVersion = 21,
                    },
                },
                null);
        }
    }
}
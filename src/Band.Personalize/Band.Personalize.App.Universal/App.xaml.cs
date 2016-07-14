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

namespace Band.Personalize.App.Universal
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.Band;
    using Microsoft.Practices.Unity;
    using Model.Implementation.Repository;
    using Model.Library.Band;
    using Model.Library.Repository;
    using Prism.Events;
    using Prism.Unity.Windows;
    using Prism.Windows;
    using Prism.Windows.AppModel;
    using Prism.Windows.Navigation;
    using ViewModels.Design;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    [Bindable]
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class, which is the singleton application object.
        /// This is the first line of authored code executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Gets the Event Aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="e">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>The asynchronous task.</returns>
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            if (e != null)
            {
                if (!string.IsNullOrWhiteSpace(e.Arguments))
                {
                    // The app was launched from a Secondary Tile
                    // Navigate to the item's page
                    this.NavigationService.Navigate("ItemDetail", e.Arguments);
                }
                else if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                    this.NavigationService.RestoreSavedNavigation();
                }
                else
                {
                    this.NavigateToDefaultPage();
                }
            }
            else
            {
                this.NavigateToDefaultPage();
            }

            Window.Current.Activate();
            return Task.CompletedTask;
        }

        /// <summary>
        ///  Used for setting up the list of known types for the <see cref="PrismApplication.SessionStateService"/>,
        ///  using the <see cref="ISessionStateService.RegisterKnownType(Type)"/> method.
        /// </summary>
        protected override void OnRegisterKnownTypesForSerialization()
        {
            // Set up the list of known types for the SuspensionManager
            // this.SessionStateService.RegisterKnownType(typeof(Address));
            // this.SessionStateService.RegisterKnownType(typeof(PaymentMethod));
            // this.SessionStateService.RegisterKnownType(typeof(UserInfo));
            // this.SessionStateService.RegisterKnownType(typeof(CheckoutDataViewModel));
            // this.SessionStateService.RegisterKnownType(typeof(ObservableCollection<CheckoutDataViewModel>));
            // this.SessionStateService.RegisterKnownType(typeof(ShippingMethod));
            // this.SessionStateService.RegisterKnownType(typeof(Dictionary<string, Collection<string>>));
            // this.SessionStateService.RegisterKnownType(typeof(Order));
            // this.SessionStateService.RegisterKnownType(typeof(Product));
            // this.SessionStateService.RegisterKnownType(typeof(Collection<Product>));
        }

        /// <summary>
        /// The initialization logic of the application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="e">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>The asynchronous task.</returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs e)
        {
            this.EventAggregator = new EventAggregator();

            this.Container.RegisterInstance<INavigationService>(this.NavigationService);
            this.Container.RegisterInstance<ISessionStateService>(this.SessionStateService);
            this.Container.RegisterInstance<IEventAggregator>(this.EventAggregator);
            this.Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            // Register services
            // this.Container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());
            // this.Container.RegisterType<ICredentialStore, RoamingCredentialStore>(new ContainerControlledLifetimeManager());
            // this.Container.RegisterType<ICacheService, TemporaryFolderCacheService>(new ContainerControlledLifetimeManager());
            // this.Container.RegisterType<ISecondaryTileService, SecondaryTileService>(new ContainerControlledLifetimeManager());
            // this.Container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());

            // Register repositories
#if DEBUG && STUB
            this.Container.RegisterInstance<IBandPersonalizer>(BandPersonalizerStub.Instance);
            this.Container.RegisterInstance<IBandRepository>(BandRepositoryStub.Instance);
#else
            this.Container.RegisterInstance<IBandClientManager>(BandClientManager.Instance);
            this.Container.RegisterInstance<Func<IBand>>(() => null); // TODO: actually get the "currently selected Band" (child container?)
            this.Container.RegisterType<IBandPersonalizer, BandPersonalizer>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IBandRepository, BandRepository>(new ContainerControlledLifetimeManager());
#endif

            // Register child view models
            // this.Container.RegisterType<IShippingAddressUserControlViewModel, ShippingAddressUserControlViewModel>();
            // this.Container.RegisterType<IBillingAddressUserControlViewModel, BillingAddressUserControlViewModel>();
            // this.Container.RegisterType<IPaymentMethodUserControlViewModel, PaymentMethodUserControlViewModel>();
            // this.Container.RegisterType<ISignInUserControlViewModel, SignInUserControlViewModel>();
            return base.OnInitializeAsync(e);
        }

        /// <summary>
        /// Navigate to the default page of the application.
        /// </summary>
        /// <returns>Returns <c>true</c> if navigation succeeds; otherwise, <c>false</c>.</returns>
        private bool NavigateToDefaultPage()
        {
            return this.NavigationService.Navigate("Main", null);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            this.NavigationService.Suspending();

            deferral.Complete();
        }
    }
}
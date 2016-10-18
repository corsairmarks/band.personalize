﻿// Copyright 2016 Nicholas Butcher
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
    using System.Linq;
    using System.Threading.Tasks;
    using Fake;
    using Microsoft.Band;
    using Microsoft.Practices.Unity;
    using Model.Implementation.Repository;
    using Model.Library.Repository;
    using Newtonsoft.Json;
    using Prism.Mvvm;
    using Prism.Unity.Windows;
    using Prism.Windows.AppModel;
    using Prism.Windows.Mvvm;
    using ViewModels;
    using Views;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.UI.Popups;
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
            : base()
        {
            this.InitializeComponent();
            this.UnhandledException += this.OnUnhandledBandException;
        }

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

            if (e == null || e.PreviousExecutionState != ApplicationExecutionState.Terminated)
            {
                this.NavigateToDefaultPage();
            }
            else
            {
                // TODO: Load state from previously suspended application
                this.NavigationService.RestoreSavedNavigation();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Configures the <see cref="ViewModelLocator"/> used by Prism.
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // TODO: could inject a lambda?
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                // var xx = $"I{viewType.Name}ViewModel";
                // Assembly.Load(null).GetTypes().SingleOrDefault(type => type.GetTypeInfo().Int);
                if (viewType == typeof(MainPage))
                {
                    return typeof(IMainPageViewModel);
                }
                else if (viewType == typeof(BandPage))
                {
                    return typeof(IBandPageViewModel);
                }

                return null;
            });
        }

        /// <summary>
        /// Creates and configures the <see cref="PrismUnityApplication.Container"/> and service locator.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // NOTE: the PrismUnityApplication.ConfigureContainer() automatically registers singletons of:
            // * IServiceLocator
            // * ISessionStateService
            // * IDeviceGestureService
            // * IEventAggregator
            // PrismApplication.CreateNavigationService() creates INavigationService after the container is initially configured
            this.Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            var applicationData = ApplicationData.Current;
            applicationData.DataChanged += (s, e) =>
            {
                var cached = this.Container.Resolve<ICachedRepository<ICustomThemeRepository>>();
                cached.Clear();
            };
            this.Container.RegisterInstance<ApplicationData>(applicationData, new ExternallyControlledLifetimeManager());

            this.Container.RegisterType<StorageFolder>(
                "RoamingStorageFolder",
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => container.Resolve<ApplicationData>().RoamingFolder));

            this.Container.RegisterType<Func<Guid, TitledThemeViewModel, PersistedTitledThemeViewModel>>(new InjectionFactory(context =>
            {
                return new Func<Guid, TitledThemeViewModel, PersistedTitledThemeViewModel>((id, model) => new PersistedTitledThemeViewModel(id, context.Resolve<IResourceLoader>(), context.Resolve<ICustomThemeRepository>())
                {
                    Title = model.Title,
                    Base = model.Base,
                    HighContrast = model.HighContrast,
                    Lowlight = model.Lowlight,
                    Highlight = model.Highlight,
                    Muted = model.Muted,
                    SecondaryText = model.SecondaryText,
                });
            }));

            // Register JSON serialization
            this.Container.RegisterType<JsonConverter, RgbColorJsonConverter>(nameof(RgbColorJsonConverter), new ContainerControlledLifetimeManager());
            this.Container.RegisterType<JsonSerializerSettings>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => new JsonSerializerSettings { Converters = container.ResolveAll<JsonConverter>().ToList(), }));
            this.Container.RegisterType<JsonSerializer>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => JsonSerializer.Create(container.Resolve<JsonSerializerSettings>())));

            // Register repositories
            this.Container.RegisterType<ICustomThemeRepository, CustomThemeRepository>(
                "UncachedCustomThemeRepository",
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<StorageFolder>("RoamingStorageFolder"), new ResolvedParameter<JsonSerializer>()));
            this.Container.RegisterType<ICustomThemeRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => container.Resolve<ICachedRepository<ICustomThemeRepository>>().Repository));
            this.Container.RegisterType<ICachedRepository<ICustomThemeRepository>, CachedCustomThemeRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<ICustomThemeRepository>("UncachedCustomThemeRepository")));

            this.Container.RegisterType<IBandRepository, BandRepository>(
                "UncachedBandRepository",
                new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IBandRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => container.Resolve<ICachedRepository<IBandRepository>>().Repository));
            this.Container.RegisterType<ICachedRepository<IBandRepository>, CachedBandRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBandRepository>("UncachedBandRepository")));

            this.Container.RegisterType<IBandPersonalizer, BandPersonalizer>(new ContainerControlledLifetimeManager());

            // Register the IBandClientManager
            IBandClientManager bandClientManager;
#if DEBUG && STUB
            var fakeBandInfos = new[]
            {
                new FakeBandInfo
                {
                    ConnectionType = BandConnectionType.Bluetooth,
                    Name = "Sample Band",
                    HardwareVersion = "10",
                },
                new FakeBandInfo
                {
                    ConnectionType = BandConnectionType.Usb,
                    Name = "Sample Band 2",
                    HardwareVersion = "26",
                },
                new FakeBandInfo
                {
                    ConnectionType = (BandConnectionType)(-1),
                    Name = "Sample Unknown Band",
                    IsConnected = false,
                },
            };

            bandClientManager = new FakeBandClientManager(
                1000,
                new Random(),
                fakeBandInfos);
#else
            bandClientManager = BandClientManager.Instance;
#endif
            this.Container.RegisterInstance<IBandClientManager>(bandClientManager, new ExternallyControlledLifetimeManager());

            // register ViewModels
            this.Container.RegisterType<IMainPageViewModel, MainPageViewModel>();
            this.Container.RegisterType<IBandPageViewModel, BandPageViewModel>();
        }

        /// <summary>
        /// Navigate to the default page of the application.
        /// </summary>
        /// <returns>Returns <c>true</c> if navigation succeeds; otherwise, <c>false</c>.</returns>
        private bool NavigateToDefaultPage()
        {
            return this.NavigationService.Navigate(PageNavigationTokens.MainPage, null);
        }

        /// <summary>
        /// Represents the method that will handle the <see cref="Application.UnhandledException"/> event for exceptions that are or descend from <see cref="BandException"/>.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Event data.</param>
        private async void OnUnhandledBandException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null)
            {
                // WORKAROUND: the UnhandledExceptionEventArgs.Exception value only returns useful information the first time it is accessed,
                // so storing a local works around that limitation so that exception data can be used for reasoning about the error
                // See: https://petermeinl.wordpress.com/2016/07/09/global-error-handling-for-uwp-apps/
                var localException = e.Exception;
                if (localException != null && localException is BandException)
                {
                    e.Handled = true;
                    var messageDialog = new MessageDialog(e.Message);
                    await messageDialog.ShowAsync();
                }
            }
        }
    }
}
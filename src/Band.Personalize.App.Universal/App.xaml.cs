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
    using Microsoft.Band;
    using Microsoft.Practices.Unity;
    using Model.Implementation.Repository;
    using Model.Library.Repository;
    using Newtonsoft.Json;
    using Prism.Events;
    using Prism.Unity.Windows;
    using Prism.Windows;
    using Prism.Windows.AppModel;
    using Prism.Windows.Navigation;
    using ViewModels;
    using ViewModels.Design;
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
        ///  Used for setting up the list of known types for the <see cref="PrismApplication.SessionStateService"/>,
        ///  using the <see cref="ISessionStateService.RegisterKnownType(Type)"/> method.
        /// </summary>
        protected override void OnRegisterKnownTypesForSerialization()
        {
            // Set up the list of known types for the SuspensionManager
            // this.SessionStateService.RegisterKnownType(typeof(Type));
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

            var applicationData = ApplicationData.Current;
            applicationData.DataChanged += this.DataChanged; // TODO: inject?
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
            this.Container.RegisterType<IBandRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(container => container.Resolve<ICachedRepository<IBandRepository>>().Repository));
            this.Container.RegisterType<ICachedRepository<IBandRepository>, CachedBandRepository>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new ResolvedParameter<IBandRepository>("UncachedBandRepository")));

#if DEBUG && STUB
            this.Container.RegisterInstance<IBandPersonalizer>(BandPersonalizerStub.Instance, new ExternallyControlledLifetimeManager());
            this.Container.RegisterInstance<IBandRepository>(
                "UncachedBandRepository",
                BandRepositoryStub.Instance,
                new ExternallyControlledLifetimeManager());
#else
            this.Container.RegisterInstance<IBandClientManager>(BandClientManager.Instance, new ExternallyControlledLifetimeManager());
            this.Container.RegisterType<IBandPersonalizer, BandPersonalizer>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IBandRepository, BandRepository>(
                "UncachedBandRepository",
                new ContainerControlledLifetimeManager());
#endif

            return base.OnInitializeAsync(e);
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

        /// <summary>
        /// Occurs when roaming application data is synchronized.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data. If there is no event data, this parameter will be <c>null</c>.</param>
        private void DataChanged(ApplicationData sender, object args)
        {
            var cached = this.Container.Resolve<ICachedRepository<ICustomThemeRepository>>();
            cached.Clear();
        }
    }
}
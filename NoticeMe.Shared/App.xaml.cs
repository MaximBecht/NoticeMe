using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using NoticeMe.Data;
using NoticeMe.Pages;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Popups;

namespace NoticeMe
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private Window _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeLogging();

            this.InitializeComponent();

#if HAS_UNO || NETFX_CORE
            this.Suspending += OnSuspending;
#endif
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await DataManager.LoadAllDataAsync();
            DataManager.InitAutoSaver();

            SetThemeOnStartup();

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

#if NET6_0_OR_GREATER && WINDOWS && !HAS_UNO
            _window = new Window();
            _window.Activate();
#else
            _window = Microsoft.UI.Xaml.Window.Current;
#endif

            var rootFrame = _window.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (args.UWPLaunchActivatedEventArgs.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                _window.Content = rootFrame;
            }

#if !(NET6_0_OR_GREATER && WINDOWS)
            if (args.UWPLaunchActivatedEventArgs.PrelaunchActivated == false)
#endif
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), args.Arguments);
                }
                // Ensure the current window is active
                _window.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new InvalidOperationException($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            await DataManager.SaveAllDataAsync();

            deferral.Complete();
        }

        /// <summary>
        /// Configures global Uno Platform logging
        /// </summary>
        private static void InitializeLogging()
        {
#if DEBUG
            // Logging is disabled by default for release builds, as it incurs a significant
            // initialization cost from Microsoft.Extensions.Logging setup. If startup performance
            // is a concern for your application, keep this disabled. If you're running on web or 
            // desktop targets, you can use url or command line parameters to enable it.
            //
            // For more performance documentation: https://platform.uno/docs/articles/Uno-UI-Performance.html

            var factory = LoggerFactory.Create(builder =>
            {
#if __WASM__
                builder.AddProvider(new global::Uno.Extensions.Logging.WebAssembly.WebAssemblyConsoleLoggerProvider());
#elif __IOS__
                builder.AddProvider(new global::Uno.Extensions.Logging.OSLogLoggerProvider());
#elif NETFX_CORE
                builder.AddDebug();
#else
                builder.AddConsole();
#endif

                // Exclude logs below this level
                builder.SetMinimumLevel(LogLevel.Information);

                // Default filters for Uno Platform namespaces
                builder.AddFilter("Uno", LogLevel.Warning);
                builder.AddFilter("Windows", LogLevel.Warning);
                builder.AddFilter("Microsoft", LogLevel.Warning);

                // Generic Xaml events
                // builder.AddFilter("Windows.UI.Xaml", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.VisualStateGroup", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.StateTriggerBase", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.UIElement", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.FrameworkElement", LogLevel.Trace );

                // Layouter specific messages
                // builder.AddFilter("Windows.UI.Xaml.Controls", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.Controls.Layouter", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.Controls.Panel", LogLevel.Debug );

                // builder.AddFilter("Windows.Storage", LogLevel.Debug );

                // Binding related messages
                // builder.AddFilter("Windows.UI.Xaml.Data", LogLevel.Debug );
                // builder.AddFilter("Windows.UI.Xaml.Data", LogLevel.Debug );

                // Binder memory references tracking
                // builder.AddFilter("Uno.UI.DataBinding.BinderReferenceHolder", LogLevel.Debug );

                // RemoteControl and HotReload related
                // builder.AddFilter("Uno.UI.RemoteControl", LogLevel.Information);

                // Debug JS interop
                // builder.AddFilter("Uno.Foundation.WebAssemblyRuntime", LogLevel.Debug );
            });

            global::Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory = factory;

#if HAS_UNO
            global::Uno.UI.Adapter.Microsoft.Extensions.Logging.LoggingAdapter.Initialize();
#endif
#endif
        }


        public enum Theme
        {
            Default,
            Light,
            Dark,
            HighContrastBlack,
            HighContrastWhite
        }
        public enum Font
        {
            AkzidenzGrotesk,
            Helvetica,
            Didot,
            Baskerville,
            GillSans,
            Bembo,
            Sabon,
            Georgia,
            NewsGothic,
            Myriad,
            Minion,
            MrsEaves,
            Garamond,
            Gotham,
            Futura,
            Bodoni,
            Arial,
            TimesNewRoman,
            Verdana,
            Rockwell,
            FranklinGothic,
            Univers,
            Frutiger,
            Avenir,
        }
        private static void SetThemeOnStartup()
        {
            // ToDo: if(theme like OS Theme setting)                -> toggle       in settings
            // ToDo: theme selection                                -> dropdown     in settings
            // ToDo: font selection + save + .xaml dictionaries     -> dropdown     in settings

            bool setOSRequestedTheme = true;

            if (setOSRequestedTheme)
            {
                ApplicationTheme requestedTheme = App.Current.RequestedTheme;
                if (requestedTheme == ApplicationTheme.Light)
                {
                    ((App)Application.Current).ChangeTheme(Theme.Light, false);
                }
                else if (requestedTheme == ApplicationTheme.Dark)
                {
                    ((App)Application.Current).ChangeTheme(Theme.Dark, false);
                }
            }
            else
            {
                ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Theme requestedTheme = Theme.Default;
                if (localSettings.Values.ContainsKey("AppTheme"))
                {
                    requestedTheme = ParseEnum<Theme>(localSettings.Values["AppTheme"]);
                }

                ((App)Application.Current).ChangeTheme(requestedTheme, true);
            }
        }
        public void ChangeTheme(Theme theme, bool refresh)
        {
            switch (theme)
            {
                case Theme.Default: ChangeThemeSource("ms-appx:///Assets/Themes/LightTheme.xaml"); break;
                case Theme.Light: ChangeThemeSource("ms-appx:///Assets/Themes/LightTheme.xaml"); break;
                case Theme.Dark: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
                case Theme.HighContrastBlack: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
                case Theme.HighContrastWhite: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
            }

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["AppTheme"] = theme.ToString();

            if (refresh)
            {
                var rootFrame = _window.Content as Frame;
                rootFrame.Navigate(typeof(MainPage), null);
            }
        }
        private void ChangeThemeSource(string source)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri(source);
        }
        public void ChangeFont(Font font)
        {

        }


        private static T ParseEnum<T>(object value)
        {
            if (value == null)
                return default(T);
            return (T)Enum.Parse(typeof(T), value.ToString());
        }
    }
}

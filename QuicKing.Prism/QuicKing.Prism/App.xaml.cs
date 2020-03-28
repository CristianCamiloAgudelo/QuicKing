using Prism;
using Prism.Ioc;
using QuicKing.Common.Services;
using QuicKing.Prism.ViewModels;
using QuicKing.Prism.Views;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuicKing.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjI4OTk3QDMxMzcyZTM0MmUzMFVmM1d1ajhNOEdwMFRENEMvbXFkQUhxR0lwL2djVDlxT1UrZlhrWll6ZTQ9");
            InitializeComponent();

            await NavigationService.NavigateAsync("/TaxiMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<TaxiMasterDetailPage, TaxiMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<TaxiHistoryPage, TaxiHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupPage, GroupPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}

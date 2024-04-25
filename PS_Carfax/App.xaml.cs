using Microsoft.Extensions.DependencyInjection;
using PS_Carfax.Data;
using PS_Carfax.Data.Seed;
using PS_Carfax.UI.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PS_Carfax
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public DataService DataService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            SeedData.Initialize(ServiceProvider);
            this.DataService = ServiceProvider.GetRequiredService<DataService>();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register DataService
            services.AddSingleton<DataService>();

            // Register your main window
            services.AddTransient<MainWindow>();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HotalAppLibrary.Data;
using HotalAppLibrary.Databases;

namespace HotelApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // We declare serviceProvider and set it to public to keep using the same instance without having to do controtor injection
        public static ServiceProvider serviceProvider;

        // 1- we need to override the start up method
        protected override void OnStartup(StartupEventArgs e)
        {
            // call the OnStartup
            base.OnStartup(e);

            // we add on top of it not replacing it. 
            var services = new ServiceCollection();
            services.AddTransient<MainWindow>();
            services.AddTransient<CheckInWindow>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
            services.AddTransient<IDatabaseData, SqlData>();

            // 2- Setup Configiration 
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            
            IConfiguration config = builder.Build();

            services.AddSingleton(config);

            serviceProvider = services.BuildServiceProvider();
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
    }
}

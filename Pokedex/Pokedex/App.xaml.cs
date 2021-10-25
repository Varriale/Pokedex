using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Pokedex.Services;
using Pokedex.Entities;
using Microsoft.EntityFrameworkCore;
using Pokedex.ViewModels;

namespace Pokedex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlite("data source=Pokedex.db");
            });
            services.AddSingleton<MainWindow>();
            services.AddScoped<IPokeAPIClient, PokeAPIClient>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.DataContext = new MainViewModel();
            mainWindow.Show();
        }
    }
}

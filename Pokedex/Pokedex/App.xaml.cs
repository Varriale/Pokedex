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
using Pokedex.Views;
using System.Windows.Controls;

namespace Pokedex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider serviceProvider;
        public Trainer CurrentTrainer { get; private set; }
        public Page LastPage { get; set; }
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlite("data source=Pokedex.db");
            });
            services.AddSingleton<MainWindow>();
            services.AddScoped<TestView>();
            services.AddScoped<HomeView>();
            services.AddScoped<TrainerSelection>();
            services.AddScoped<NewTrainerView>();
            services.AddScoped<ViewsDependancy>();
            services.AddScoped<IPokeAPIClient, PokeAPIClient>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.MainFrame.Content = serviceProvider.GetService<TrainerSelection>();
            mainWindow.Show();
        }
        public void NavigateTo<T>(Func<ViewsDependancy,T> del) where T:Page
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            LastPage = (Page)mainWindow.MainFrame.Content;
            mainWindow.MainFrame.Navigate(del(serviceProvider.GetService<ViewsDependancy>()));
            mainWindow.Show();
        }

        public void NavigateBack()
        {
            if (LastPage != null)
            {
                var mainWindow = serviceProvider.GetService<MainWindow>();
                var newLastPage = (Page)mainWindow.MainFrame.Content;
                mainWindow.MainFrame.Navigate(LastPage);
                LastPage = newLastPage;
                mainWindow.Show();
            }            
        }

        public void SetTrainer(Trainer trainer)
        {
            CurrentTrainer = trainer;
        }
    }
}

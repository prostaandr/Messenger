using Messenger.Client.WPF.Views;
using Messenger.Data;
using Messenger.Data.Interfaces;
using Messenger.Data.Repositories;
using Messenger.Services.Interfaces;
using Messenger.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Messenger.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<MessengerContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();


            services.AddSingleton<LoginWindow>();
            services.AddSingleton<RegistrationWindow>();
            services.AddSingleton<MainWindow>();

        }
    }
}


using Messenger.Client.WPF.Commands;
using Messenger.Client.WPF.Views;
using Messenger.Dto;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.Client.WPF.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (value == _login) return;
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                  (_loginCommand = new RelayCommand(obj =>
                  {
                      var currentWindow = obj as Window;
                      LoginAsync(currentWindow);
                  }));
            }
        }


        public LoginViewModel()
        {
            
        }

        private async void LoginAsync(Window currentWindow)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var request = new LoginRequest() { Login = Login, Password = Password};
                    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:7076/Account/Login", content);

                    var token = await response.Content.ReadAsStringAsync();
                    MainViewModel.Token = token;
                    response.EnsureSuccessStatusCode();
                }

                currentWindow.Hide();
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудачная попытка входа: " + ex.Message);
            }
        }

        private RelayCommand _closingCommand;
        public RelayCommand ClosingCommand
        {
            get
            {
                return _closingCommand ??
                  (_closingCommand = new RelayCommand(obj =>
                  {
                      var current = obj as Window;
                      current.Hide();
                  }));
            }
        }
    }
}

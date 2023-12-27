using Messenger.Client.WPF.Commands;
using Messenger.Client.WPF.Helpers;
using Messenger.Client.WPF.Views;
using Messenger.Domain;
using Messenger.Dto;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.Client.WPF.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                if (value == _user) return;
                _user = value;
                OnPropertyChanged();
            }
        } 

        private string _repeatePassword;
        public string RepeatePassword
        {
            get => _repeatePassword;
            set
            {
                if (value == _repeatePassword) return;
                _repeatePassword = value;
                OnPropertyChanged();
            }
        }

        private string _loginErrors;
        public string LoginErrors
        {
            get => _loginErrors;
            set
            {
                if (value == _loginErrors) return;
                _loginErrors = value;
                OnPropertyChanged();
            }
        }

        private string _emailErrors;
        public string EmailErrors
        {
            get => _emailErrors;
            set
            {
                if (value == _emailErrors) return;
                _emailErrors = value;
                OnPropertyChanged();
            }
        }

        private string _passwordErrors;
        public string PasswordErrors
        {
            get => _passwordErrors;
            set
            {
                if (value == _passwordErrors) return;
                _passwordErrors = value;
                OnPropertyChanged();
            }
        }

        private string _nickNameErrors;
        public string NickNameErrors
        {
            get => _nickNameErrors;
            set
            {
                if (value == _nickNameErrors) return;
                _nickNameErrors = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand
        {
            get
            {
                return _addUserCommand ??
                  (_addUserCommand = new RelayCommand(obj =>
                  {
                      var values = (object[])obj;

                      LoginErrors = String.Join("\n", User.Login.Rules().NotEmpty().MinCharacters(5).MaxCharacters(30).Validate());
                      EmailErrors = String.Join("\n", User.Email.Rules().NotEmpty().Validate());
                      PasswordErrors = String.Join("\n", User.Password.Rules().NotEmpty().MinCharacters(6).Compliance(RepeatePassword, "Подтверждение пароля").Validate());
                      NickNameErrors = String.Join("\n", User.Nickname.Rules().NotEmpty().MinCharacters(5).MaxCharacters(20).Validate());

                      if (String.IsNullOrEmpty(LoginErrors) && String.IsNullOrEmpty(PasswordErrors) && String.IsNullOrEmpty(NickNameErrors))
                      {
                          AddUserAsync();
                          MessageBox.Show("Регистрация прошла успешно");
                          ClosingCommand.Execute(values[1]);
                      }
                  }));
            }
        }

        private async void AddUserAsync()
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:7076/Account/Registration", content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неудачная попытка регистрации: " + ex.Message);
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
                      var login = new LoginWindow();
                      login.Show();
                      current.Hide();
                  }));
            }
        }

        public RegistrationViewModel()
        {
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            User = new User();
            User.Icon = "";
        }
    }
}

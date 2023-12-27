using Messenger.Client.WPF.Commands;
using Messenger.Client.WPF.Views;
using Messenger.Data.Repositories;
using Messenger.Domain;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Messenger.Client.WPF.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private readonly HubConnection _connection;

        private User _currentInterlocutor;
        public User CurrentInterlocutor
        {
            get => _currentInterlocutor;
            set
            {
                if (value == _currentInterlocutor) return;
                _currentInterlocutor = value;
                OnPropertyChanged();
            }
        }

        private string _sendingText;
        public string SendingText
        {
            get => _sendingText;
            set
            {
                if (value == _sendingText) return;
                _sendingText = value;
                OnPropertyChanged();
            }
        }

        private string _sendedText;
        public string SendedText
        {
            get => _sendedText;
            set
            {
                if (value == _sendedText) return;
                _sendedText = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _sendCommand;
        public RelayCommand SendCommand
        {
            get
            {
                return _sendCommand ??
                  (_sendCommand = new RelayCommand(obj =>
                  {
                      SendAsync();
                  }));
            }
        }

        private async void SendAsync()
        {
            try
            {
                await _connection.InvokeAsync("PrivateSend", SendingText, CurrentInterlocutor.Id.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ChatViewModel()
        {
            SendingText = "gg2";
            SendedText = "gg1";

            _userService = App.serviceProvider.GetService<IUserService>();

            var token = MainViewModel.Token;
            _connection = new HubConnectionBuilder().WithUrl("https://localhost:7076/chat", option =>
            {
                option.UseDefaultCredentials = true;
                option.AccessTokenProvider = () => Task.FromResult(token);
            }).WithAutomaticReconnect().Build();


            _connection.On<string, string>("SendPrivateReceive", (message, name) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{name}: {message}";
                    SendedText = newMessage;
                });
            });

            Connection();

            InitilizationAsync();
        }

        private async void InitilizationAsync()
        {
            CurrentInterlocutor = await _userService.GetUser(3);
        }

        private async void Connection()
        {
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

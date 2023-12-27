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
using System.Collections.ObjectModel;
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

        private InterlocutorsViewModel _interlocutorsViewModel;
        public InterlocutorsViewModel InterlocutorsViewModel
        {
            get => _interlocutorsViewModel;
            set
            {
                if (value == _interlocutorsViewModel) return;
                _interlocutorsViewModel = value;
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
                await _connection.InvokeAsync("PrivateSend", SendingText, InterlocutorsViewModel.CurrentInterlocutor.Interlocutor.Id.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public ChatViewModel()
        {
            SendingText = "";
            _userService = App.serviceProvider.GetService<IUserService>();

            var token = MainViewModel.Token;
            _connection = new HubConnectionBuilder().WithUrl("https://localhost:7076/chat", option =>
            {
                option.UseDefaultCredentials = true;
                option.AccessTokenProvider = () => Task.FromResult(token);
            }).WithAutomaticReconnect().Build();
            
            _connection.On<string, bool>("SendPrivateReceive", (messageValue, isSender) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var newMessage = Task.FromResult(_userService.GetLastUserMessage(MainViewModel.CurrentUser.Id, InterlocutorsViewModel.CurrentInterlocutor.Interlocutor.Id));

                    var message = new Message
                    {
                        Value = messageValue,
                        Type = MessageType.Text,
                        Date = DateTime.Now
                    };

                    var newUserMessage = new UserMessage
                    {
                        SenderId = MainViewModel.CurrentUser.Id,
                        ReciverId = InterlocutorsViewModel.CurrentInterlocutor.Interlocutor.Id,
                        Message = message
                    };

                    Task.FromResult(_userService.SendUserMessage(newUserMessage));

                    var inter = InterlocutorsViewModel.Interlocutors.Where(i => i.CurrentUserId == newUserMessage.SenderId && i.Interlocutor.Id == newUserMessage.ReciverId).FirstOrDefault();
                    inter.Messages.Add(new MessageViemModel(newUserMessage.SenderId, newUserMessage, isSender));

                    SendingText = "";
                });
            });

            _connection.On<int>("OnConnectedRecive", (id) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var inter = InterlocutorsViewModel.Interlocutors.Where(i => i.Interlocutor.Id == id).FirstOrDefault();
                    inter.Interlocutor.Status = UserStatus.Online;
                });
            });

            _connection.On<int>("OnDisconnectedAsync", (id) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var inter = InterlocutorsViewModel.Interlocutors.Where(i => i.Interlocutor.Id == id).FirstOrDefault();
                    inter.Interlocutor.Status = UserStatus.Offline;
                });
            });

            Connection();

            InterlocutorsViewModel = new InterlocutorsViewModel(MainViewModel.CurrentUser.Id);
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

using Messenger.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger.Client.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        HubConnection _connection;

        public ChatPage()
        {
            InitializeComponent();

            _connection = new HubConnectionBuilder().WithUrl("https://localhost:7076/chat").WithAutomaticReconnect().Build();

            _connection.On<string, string>("Receive", (message, name) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{name}: {message}";
                    testTextBloc.Text = newMessage;
                });
            });

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _connection.InvokeAsync("PrivateSend", testTextBox.Text, "test");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
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
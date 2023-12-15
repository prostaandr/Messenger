using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (value == _currentViewModel) return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private readonly HubConnection _connection;
        public MainViewModel()
        {
            _currentViewModel = new ChatViewModel();

            //var _connection = new HubConnection("https://localhost:7076/chat");
        }
    }
}

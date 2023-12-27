using Messenger.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static string Token = "";

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

        public MainViewModel()
        {
            _currentViewModel = new ChatViewModel();
        }
    }
}

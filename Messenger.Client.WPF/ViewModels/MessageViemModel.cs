using Messenger.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.ViewModels
{
    public class MessageViemModel : BaseViewModel
    {
        private int _currentUserId;
        public int CurrentUserId
        {
            get => _currentUserId;
            set
            {
                if (value == _currentUserId) return;
                _currentUserId = value;
                OnPropertyChanged();
            }
        }

        private UserMessage _message;
        public UserMessage Message
        {
            get => _message;
            set
            {
                if (value == _message) return;
                _message = value;
                OnPropertyChanged();
            }
        }

        private bool _isSender;
        public bool IsSender
        {
            get => _isSender;
            set
            {
                if (value == _isSender) return;
                _isSender = value;
                OnPropertyChanged();
            }
        }

        public MessageViemModel(int currentUserId, UserMessage message, bool isSender)
        {
            CurrentUserId = currentUserId;
            Message = message;
            IsSender = isSender;
        }
    }
}

using Messenger.Domain;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.ViewModels
{
    public class InterlocutorViewModel : BaseViewModel
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

        private User _interlocutor;
        public User Interlocutor
        {
            get => _interlocutor;
            set
            {
                if (value == _interlocutor) return;
                _interlocutor = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MessageViemModel> _messages;
        public ObservableCollection<MessageViemModel> Messages
        {
            get => _messages;
            set
            {
                if (value == _messages) return;
                _messages = value;
                OnPropertyChanged();
            }
        }

        public InterlocutorViewModel(int currentUserId, User user, List<UserMessage> messages)
        {
            CurrentUserId = currentUserId;
            Interlocutor = user;

            Messages = new ObservableCollection<MessageViemModel> { };

            foreach (var message in messages)
            {
                if (message.SenderId != CurrentUserId)
                Messages.Add(new MessageViemModel(CurrentUserId, message, false));
                if (message.SenderId == CurrentUserId)
                    Messages.Add(new MessageViemModel(CurrentUserId, message, true));
            }

            Messages = new ObservableCollection<MessageViemModel>(Messages.OrderBy(m => m.Message.Message.Date));
        }
    }
}

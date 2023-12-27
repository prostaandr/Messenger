using Messenger.Client.WPF.Commands;
using Messenger.Domain;
using Messenger.Services;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.WPF.ViewModels
{
    public class InterlocutorsViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

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

        private ObservableCollection<InterlocutorViewModel> _interlocutors;
        public ObservableCollection<InterlocutorViewModel> Interlocutors
        {
            get => _interlocutors;
            set
            {
                if (value == _interlocutors) return;
                _interlocutors = value;
                OnPropertyChanged();
            }
        }

        private InterlocutorViewModel _currentInterlocutor;
        public InterlocutorViewModel CurrentInterlocutor
        {
            get => _currentInterlocutor;
            set
            {
                if (value == _currentInterlocutor) return;
                _currentInterlocutor = value;
                OnPropertyChanged();
            }
        }


        private RelayCommand _setCurrentInterlocutorCommand;
        public RelayCommand SetCurrentInterlocutorCommand
        {
            get
            {
                return _setCurrentInterlocutorCommand ??
                  (_setCurrentInterlocutorCommand = new RelayCommand(obj =>
                  {
                      var id = Convert.ToInt32(obj);
                      CurrentInterlocutor = Interlocutors.Where(i => i.Interlocutor.Id == id).FirstOrDefault();
                  }));
            }
        }

        public InterlocutorsViewModel(int userId)
        {
            _userService = App.serviceProvider.GetService<IUserService>();

            CurrentUserId = userId;

            InitilizationAsync();
        }

        private async void InitilizationAsync()
        {
            Interlocutors = new ObservableCollection<InterlocutorViewModel>();
            var users = await _userService.GetInterlocutors(MainViewModel.CurrentUser.Id);
            foreach (var user in users)
            {
                Interlocutors.Add(new InterlocutorViewModel(CurrentUserId, user, await _userService.GetUserMessages(CurrentUserId, user.Id)));
            }

            CurrentInterlocutor = Interlocutors.FirstOrDefault();
        }
    }
}

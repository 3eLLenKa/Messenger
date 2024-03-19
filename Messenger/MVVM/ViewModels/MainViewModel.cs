using Messenger.MVVM.Models;
using Messenger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel? _currentUserAccount;
        private IUserRepository _userRepository;
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set 
            { 
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"Welcome, {user.Name} {user.LastName}",
                    ProfilePicture = null
                };
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user";
                Application.Current.Shutdown();
            }
        }
    }
}
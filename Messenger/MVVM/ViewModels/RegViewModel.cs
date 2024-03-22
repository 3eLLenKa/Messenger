using Messenger.MVVM.Models;
using Messenger.Repositories;
using Messenger.Navigation;
using Messenger.Checks;
using Messenger.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger.MVVM.ViewModels
{
    internal class RegViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _username;
        private string _errorMessage;

        private SecureString _password;

        private bool _isViewVisible = true;

        private IUserRepository _userRepository;

        private UserModel _user;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value; 
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand regCommand { get; }
        public ICommand ShowLoginForm {  get; }

        public RegViewModel()
        {
            _userRepository = new UserRepository();
            regCommand = new ViewModelCommand(ExecuteRegCommand, CanExecuteRegCommand);
            ShowLoginForm = new ViewModelCommand(ExecuteShowLoginFormCommand);
        }

        private void ExecuteShowLoginFormCommand(object parameter)
        {
            NavigationSource.GetNavigation.Navigate(new LoginView());
            IsViewVisible = false;
        }

        private void ExecuteRegCommand(object parameter)
        {
            bool isCheckedUser = _userRepository.CheckUserByEmail(Email);

            if (isCheckedUser)
            {
                _user = new UserModel()
                {
                    Id = null,
                    Username = UserName,
                    Password = new System.Net.NetworkCredential(UserName, Password).Password,
                    Name = FirstName,
                    LastName = LastName,
                    Email = Email,
                };

                _userRepository.RegUser(_user);

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserName), null);

                IsViewVisible = false;

                NavigationSource.GetNavigation.Navigate(new MainView());
            }
            else
            {
                ErrorMessage = "an error occured! This email is already taken.";
            }
        }

        private bool CanExecuteRegCommand(object parameter)
        {
            bool validData;

            if (CheckUserData.CheckSecurePassword(Password) &
                CheckUserData.CheckOtherData([UserName, FirstName, LastName, Email]))
            {
                validData = true;
            }
            else validData = false;

            return validData;
        }
    }
}
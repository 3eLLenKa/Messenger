﻿using Messenger.MVVM.Models;
using Messenger.MVVM.Views;
using Messenger.Repositories;
using Messenger.Navigation;
using Messenger.Checks;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Security.Principal;
using System.Security.Cryptography.Xml;
using System.Windows.Navigation;

namespace Messenger.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository _userRepository;

        public string Username
        {
            get { return _username; }
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand ShowRegistrationPage {  get; }

        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(value => ExecuteRecoverPasswordCommand("", ""));
            ShowRegistrationPage = new ViewModelCommand(ExecuteShowRegistrationPageCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _userRepository.AuthUser(new NetworkCredential(Username, Password));

            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);

                NavigationSource.GetNavigation.Navigate(new MainView());

                SessionManager.Username = this.Username;
                SessionManager.AuthToken = this.Username + "token";
                SessionManager.UserId = _userRepository.GetByUsername(this.Username).Id;

                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;

            if (CheckUserData.CheckSecurePassword(Password) &
                CheckUserData.CheckOtherData([Username]))
            {
                validData = true;
            }
            else validData = false;

            return validData;
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowRegistrationPageCommand(object obj)
        {
            NavigationSource.GetNavigation.Navigate(new RegView());
            IsViewVisible = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Messenger.MVVM.Models;
using Messenger.MVVM.Views;
using Messenger.Repositories;
using Messenger.Utils;

namespace Messenger.MVVM.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        private string _searchText;
        private string _selectedItem;
        private IUserRepository _userRepository;
        private IContactRepository _contactRepository;

        public ObservableCollection<string> SearchResult { get; }

        public string SearchText
        {
            get { return _searchText; }
            set 
            { 
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                Task.Delay(90).Wait();

                Search();
            }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public ICommand AddContactCommand { get; }
        public ICommand DeleteContactCommand { get; }

        public AddContactViewModel()
        {
            _userRepository = new UserRepository();
            _contactRepository = new ContactRepository();
            SearchResult = new ObservableCollection<string>();

            AddContactCommand = new ViewModelCommand(ExecuteAddContactCommand);
            DeleteContactCommand = new ViewModelCommand(ExecuteDeleteContactCommand);
        }

        private void ExecuteAddContactCommand(object parameter)
        {
            var newContact = _userRepository.GetByUsername(SelectedItem);
            var currentUserId = _userRepository.GetByUsername(SessionManager.Username);

            if (newContact != null)
            {
                AddContactModel contact = new AddContactModel()
                {
                    ContactId = newContact.Id,
                    Username = newContact.Username,
                    FirstName = newContact.Name,
                    LastName = newContact.LastName
                };

                _contactRepository.AddContact(contact, currentUserId.Id);
            }
        }

        private void ExecuteDeleteContactCommand(object parameter)
        {

        }

        private void Search()
        {
            SearchResult.Clear();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var foundUsernames = _userRepository.GetUsernames(SearchText);

                if (foundUsernames != null)
                {
                    foreach (string foundUsername in foundUsernames)
                    {
                        SearchResult.Add(foundUsername);
                    }
                }
                else SearchResult.Clear();
            }
        }
    }
}
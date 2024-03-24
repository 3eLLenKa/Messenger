using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Messenger.MVVM.Models;
using Messenger.MVVM.Views;
using Messenger.Repositories;

namespace Messenger.MVVM.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        private string _searchText;
        private IUserRepository _userRepository;

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
        public ICommand AddContactCommand { get; }
        public ICommand DeleteContactCommand { get; }

        public AddContactViewModel()
        {
            _userRepository = new UserRepository();
            SearchResult = new ObservableCollection<string>();

            AddContactCommand = new ViewModelCommand(ExecuteAddContactCommand);
            DeleteContactCommand = new ViewModelCommand(ExecuteDeleteContactCommand);
        }

        private void ExecuteAddContactCommand(object parameter)
        {

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
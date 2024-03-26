using Messenger.MVVM.Models;
using Messenger.Repositories;
using Messenger.Utils;
using Messenger.MVVM.Views;
using Messenger.Checks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Messenger.Navigation;
using System.Windows;
using System.Security.Principal;

namespace Messenger.MVVM.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private string _selectedItem;
        private IContactRepository _contactRepository;
        public ObservableCollection<string> Contacts { get; }
        public ICommand ShowChatCommand { get; }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(SelectedItem);
            }
        }

        public ChatViewModel() 
        {
            _contactRepository = new ContactRepository();
            Contacts = new ObservableCollection<string>();
            ShowChatCommand = new ViewModelCommand(ExecuteShowChatCommand);

            LoadContacts();
        }

        private void ExecuteShowChatCommand(object parameter)
        {
            Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(SelectedItem), null);

            NavigationSource.GetNavigation.Navigate(new MessagesView());
        }

        private void LoadContacts()
        {
            var contacts = _contactRepository.GetContactById(SessionManager.UserId);

            if (contacts != null)
            {
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact.Username);
                }
            }
        }
    }
}

using Messenger.MVVM.Models;
using Messenger.Repositories;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.MVVM.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public ObservableCollection<string> Contacts { get; }
        private IContactRepository _contactRepository;
        public ChatViewModel() 
        { 
            _contactRepository = new ContactRepository();
            Contacts = new ObservableCollection<string>();

            LoadContacts();
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

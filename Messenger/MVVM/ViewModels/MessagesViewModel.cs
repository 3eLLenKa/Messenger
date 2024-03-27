using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Messenger.MVVM.Models;
using Messenger.Repositories;

namespace Messenger.MVVM.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        private string _contactName;
        private string _contentMessage;

        private INetworkRepository _networkRepository;
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                OnPropertyChanged(nameof(ContactName));
            }
        }

        public string ContentMessage
        {
            get { return _contentMessage; }
            set
            {
                _contentMessage = value;
                OnPropertyChanged(nameof(ContentMessage));
            }
        }

        public ICommand SendMessage { get; }
        public ICommand ReceiveMessage { get; }

        public MessagesViewModel()
        {
            ContactName = Thread.CurrentPrincipal.Identity.Name;

            _networkRepository = new NetworkRepository();
            SendMessage = new ViewModelCommand(async (parameter) => await ExecuteSendMessage(parameter));
        }

        private async Task ExecuteSendMessage(object parameter)
        {

        }
    }
}
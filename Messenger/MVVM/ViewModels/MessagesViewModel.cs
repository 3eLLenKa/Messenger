using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger.MVVM.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        private string _contactName;
        public string ContactName
        {
            get { return _contactName; }
            set
            {
                _contactName = value;
                OnPropertyChanged(nameof(ContactName));
            }
        }

        public ICommand SendMessage { get; }

        public MessagesViewModel()
        {
            ContactName = Thread.CurrentPrincipal.Identity.Name;
        }
    }
}
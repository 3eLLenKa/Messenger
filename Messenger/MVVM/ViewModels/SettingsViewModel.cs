using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Messenger.Utils;
using Messenger.MVVM.Views;

namespace Messenger.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand LogoutCommand { get; }

        public SettingsViewModel() 
        {
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);
        }
        private void ExecuteLogoutCommand(object parameter) => SessionManager.Logout();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Messenger.MVVM.Views;
using Messenger.Utils;

namespace Messenger.MVVM.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private static object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ShowProfileCommand { get; }
        public ICommand ShowChatCommand { get; }
        public ICommand ShowAddContactCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public NavigationViewModel()
        {
            ShowProfileCommand = new ViewModelCommand(ExecuteSwowProfileCommand);
            ShowChatCommand = new ViewModelCommand(ExecuteShowChatCommand);
            ShowAddContactCommand = new ViewModelCommand(ExecuteShowAddContactCommand);
            ShowSettingsCommand = new ViewModelCommand(ExecuteShowSettingsCommand);

            CurrentView = new ProfileView();
        }

        private void ExecuteSwowProfileCommand(object parameter) => CurrentView = new ProfileView();
        private void ExecuteShowChatCommand(object parameter) => CurrentView = new ChatView();
        private void ExecuteShowAddContactCommand(object parameter) => CurrentView = new AddContactView();
        private void ExecuteShowSettingsCommand(object parameter) => CurrentView = new SettingsView();
    }
}

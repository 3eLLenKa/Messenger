using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Effects;
using Messenger.MVVM.Views;

namespace Messenger.MVVM.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
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

        public void ExecuteSwowProfileCommand(object parameter) => CurrentView = new ProfileView();
        public void ExecuteShowChatCommand(object parameter) => CurrentView = new ChatView();
        public void ExecuteShowAddContactCommand(object parameter) => CurrentView = new AddContactView();
        public void ExecuteShowSettingsCommand(object parameter) => CurrentView = new SettingsView();
    }
}

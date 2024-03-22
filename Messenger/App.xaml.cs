using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Messenger.MVVM.Views;
using Messenger.Navigation;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var loginPage = new LoginView();

            NavigationSource.GetNavigation = mainWindow.MainFrame;

            mainWindow.Show();

            NavigationSource.GetNavigation.Navigate(loginPage);

            //loginPage.IsVisibleChanged += (s, ev) =>
            //{
            //    if (!loginPage.IsVisible && loginPage.IsLoaded)
            //    {
            //        mainWindow.MainFrame.Navigate(new MainView());
            //    }
            //};
        }
    }
}
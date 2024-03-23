using System.Configuration;
using System.Data;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using Messenger.MVVM.Views;
using Messenger.Navigation;
using Messenger.Utils;

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

            NavigationSource.GetNavigation = mainWindow.MainFrame;

            mainWindow.Show();

            if (SessionManager.IsLoggedIn())
            {
                NavigationSource.GetNavigation.Navigate(new MainView());
            }
            else NavigationSource.GetNavigation.Navigate(new LoginView());

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
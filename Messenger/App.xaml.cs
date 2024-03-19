using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Messenger.MVVM.Views;

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

            mainWindow.Show();
            mainWindow.MainFrame.Navigate(loginPage);

            loginPage.IsVisibleChanged += (s, ev) =>
            {
                if (!loginPage.IsVisible && loginPage.IsLoaded)
                {
                    var dataPage = new MainView();
                    mainWindow.MainFrame.Navigate(dataPage);
                }
            };
        }
    }
}
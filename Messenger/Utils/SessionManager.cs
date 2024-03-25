using System.Configuration;
using Messenger.Navigation;
using Messenger.Utils;
using Messenger.MVVM.Views;

namespace Messenger.Utils
{
    public static class SessionManager
    {
        private const string UsernameKey = "Username";
        private const string TokenKey = "AuthToken";
        private const string UserIdKey = "UserId"; // Новое свойство для идентификатора пользователя

        public static string Username
        {
            get { return ConfigurationManager.AppSettings[UsernameKey]; }
            set { UpdateAppSettings(UsernameKey, value); }
        }

        public static string AuthToken
        {
            get { return ConfigurationManager.AppSettings[TokenKey]; }
            set { UpdateAppSettings(TokenKey, value); }
        }

        public static string UserId // Новое свойство для идентификатора пользователя
        {
            get { return ConfigurationManager.AppSettings[UserIdKey]; }
            set { UpdateAppSettings(UserIdKey, value); }
        }

        private static void UpdateAppSettings(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Проверяем, существует ли указанный ключ в файле конфигурации
            if (config.AppSettings.Settings[key] == null)
            {
                // Если ключ отсутствует, добавляем его
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                // Иначе обновляем значение ключа
                config.AppSettings.Settings[key].Value = value;
            }

            // Сохраняем изменения и обновляем конфигурацию
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(AuthToken);
        }

        public static void Logout()
        {
            Username = string.Empty;
            AuthToken = string.Empty;
            UserId = string.Empty; // Сбрасываем идентификатор пользователя при выходе

            NavigationSource.GetNavigation.Navigate(new LoginView());
        }
    }
}
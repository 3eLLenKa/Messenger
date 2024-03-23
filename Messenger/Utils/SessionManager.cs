using System.Configuration;

namespace Messenger.Utils
{
    public static class SessionManager
    {
        private const string UsernameKey = "Username";
        private const string TokenKey = "AuthToken";

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
        }
    }
}
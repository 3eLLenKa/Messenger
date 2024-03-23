using Messenger.MVVM.Models;
using Messenger.Repositories;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel? _currentUserAccount; // Приватное поле для хранения данных текущего пользователя
        private IUserRepository _userRepository; // Приватное поле для доступа к данным пользователей через репозиторий

        // Свойство для доступа к данным текущего пользователя
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; } // Геттер для возврата текущего пользователя
            set
            {
                _currentUserAccount = value; // Установка нового значения текущего пользователя
                OnPropertyChanged(nameof(CurrentUserAccount)); // Уведомление представления об изменении свойства
            }
        }

        // Конструктор класса
        public MainViewModel()
        {
            _userRepository = new UserRepository(); // Инициализация репозитория для доступа к данным пользователей
            LoadCurrentUserData(); // Загрузка данных текущего пользователя при создании экземпляра MainViewModel
        }

        // Метод для загрузки данных текущего пользователя
        private void LoadCurrentUserData()
        {
            // Получение данных текущего пользователя из репозитория по имени пользователя из текущего контекста

            UserModel user = null;

            if (SessionManager.IsLoggedIn()) { user = _userRepository.GetByUsername(SessionManager.Username); }
            else user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                // Если пользователь найден, создание экземпляра UserAccountModel с данными пользователя
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"Welcome, {user.Name} {user.LastName}",
                    ProfilePicture = null // В данном случае профильное изображение не устанавливается
                };
            }
            else
            {
                // Если пользователь не найден, установка сообщения об ошибке и завершение приложения
                CurrentUserAccount.DisplayName = "Invalid user"; // Установка сообщения об ошибке
                Application.Current.Shutdown(); // Завершение приложения
            }
        }
    }
}
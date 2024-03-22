using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.MVVM.Models
{
    public interface IUserRepository
    {
        bool AuthUser(NetworkCredential credential);

        void RegUser(UserModel user);

        bool CheckUserByEmail(string email);

        void Add(UserModel userModel);
        void Edit(UserModel userModel, string field);
        void Remove(int id);

        UserModel GetById(int id);
        UserModel GetByUsername(string username);

        IEnumerable<UserModel> GetByAll();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.MVVM.Models
{
    public interface IContactRepository
    {
        public void AddContact(AddContactModel contact, string userId);
        public IEnumerable<AddContactModel> GetContactById(string id);
    }
}

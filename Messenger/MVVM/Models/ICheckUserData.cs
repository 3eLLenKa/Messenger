using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.MVVM.Models
{
    public interface ICheckUserData
    {
        public abstract static bool CheckSecurePassword(SecureString password);
        public abstract static bool CheckOtherData(string[] data);
    }
}

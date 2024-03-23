using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Messenger.MVVM.Models;
using Microsoft.VisualBasic.Devices;

namespace Messenger.Checks
{
    public class CheckUserData : ICheckUserData
    {
        public static bool CheckSecurePassword(SecureString password)
        {
            if (password != null &
                !string.IsNullOrWhiteSpace(new System.Net.NetworkCredential("", password).Password))
            {
                return true; 
            }
            else return false;
        }

        public static bool CheckOtherData(string[] data)
        {
            foreach (var elem in data)
            {
                if (elem != null)
                {
                    if (string.IsNullOrEmpty(elem) |
                    elem.Length <= 3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
using Messenger.MVVM.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Messenger.Repositories
{
    public class ContactRepository : RepositoryBase, IContactRepository
    {
        public void AddContact(AddContactModel contact, string userId)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "INSERT INTO Contacts (FirstName, LastName, UserName, UserId) VALUES (@firstname, @lastname, @username, @userid);";
                command.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = contact.FirstName;
                command.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = contact.LastName;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = contact.Username;
                command.Parameters.Add("@userid", SqlDbType.Int).Value = userId;

                command.ExecuteNonQuery();
            }
        }
    }
}
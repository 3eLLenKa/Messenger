using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Messenger.MVVM.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;

namespace Messenger.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetUsernames(string username) 
        {
            List<string> usernames = new List<string>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select Username from [Users] where Username like @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = "%" + username + "%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usernames.Add(reader.GetString(reader.GetOrdinal("Username")));
                    }
                }
            }

            return usernames;
        }

        public void RegUser(UserModel user)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                MessageBox.Show($"{user.Name}, {user.LastName}, {user.Password}, {user.Email}");

                command.Connection = connection;
                command.CommandText = $"insert into [Users] (Username, Password, [Name], [LastName], [Email]) values (@username, @password, @name, @lastname, @email)";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = user.Username;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.Add("@lastname", System.Data.SqlDbType.NVarChar).Value = user.LastName;
                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = user.Email;

                command.ExecuteNonQuery();
            }
        }

        public bool AuthUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select * from [Users] where Username = @username and [Password] = @password";
                command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = credential.Password;

                validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }

        public void Edit(UserModel userModel, string fieldToEdit)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                switch (fieldToEdit.ToLower())
                {
                    case "name":
                        command.CommandText = $"update [Users] set [Name] = @value where Id = @id";
                        command.Parameters.Add("@value", SqlDbType.NVarChar).Value = userModel.Name;
                        break;
                    case "lastname":
                        command.CommandText = $"update [Users] set [LastName] = @value where Id = @id";
                        command.Parameters.Add("@value", SqlDbType.NVarChar).Value = userModel.LastName;
                        break;
                    case "password":
                        command.CommandText = $"update [Users] set [Password] = @value where Id = @id";
                        command.Parameters.Add("@value", SqlDbType.NVarChar).Value = userModel.Password;
                        break;
                    default:
                        throw new ArgumentException("Invalid field to edit");
                }

                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = userModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [Users]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Id = reader["Id"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        };

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public UserModel GetById(int id)
        {
            UserModel user = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select * from [Users] where Id = @id";
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }

            return user;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select * from [Users] where username = @username";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = username;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };

                    }
                }
            }

            return user;
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "delete from [Users] where Id = @id";
                command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = id;
            }
        }

        public bool CheckUserByEmail(string email)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select count(*) from [Users] where Email = @email";
                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar).Value = email;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader[0].ToString()) > 0)
                        {
                            return false;
                        }
                        else return true;
                    }
                    else { return false; }
                }
            }
        }
    }
}
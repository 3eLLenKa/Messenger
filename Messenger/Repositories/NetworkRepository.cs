using Messenger.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Repositories
{
    public class NetworkRepository : RepositoryBase, INetworkRepository
    {
        public IPAddress GetContactIp(string username)
        {
            IPAddress ip = null;

            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();

                command.Connection = connection;
                command.CommandText = "select IPAddress from Contacts where Username = @username";
                command.Parameters.AddWithValue("@username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ip = IPAddress.Parse(reader.GetString(reader.GetOrdinal("IPAddress")));
                    }
                }
            }

            return ip;
        }

        public IPAddress GetIpAddress()
        {
            string host = Dns.GetHostName();

            IPAddress address = Dns.GetHostAddresses(host).First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

            if (address != null)
            {
                return address;
            }
            else throw new NotImplementedException();
        }

        public async Task<bool> Connect(string host, int port)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendMessageAsync(string content)
        {
            throw new NotImplementedException();
        }
        public async Task<string> SendMessageAsync()
        {
            throw new NotImplementedException();
        }
    }
}

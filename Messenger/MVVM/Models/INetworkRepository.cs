using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.MVVM.Models
{
    public interface INetworkRepository
    {
        public IPAddress GetContactIp(string username);
        public IPAddress GetIpAddress();
        public Task<bool> Connect(string host, int port);
        public Task<bool> SendMessageAsync(string content);
        public Task<string> SendMessageAsync();
    }
}

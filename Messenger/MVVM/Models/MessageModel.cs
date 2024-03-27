using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Messenger.MVVM.Models
{
    public class MessageModel
    {
        public string Username { get; set; }
        public string Content {  get; set; }
        public DateTime SendDate { get; set; }
    }
}

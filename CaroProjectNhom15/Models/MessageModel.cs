using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Models
{
    public class MessageModel
    {
        public string SenderName { get; set; } // tên người gửi 
        public string Content { get; set; }  // Nội dung chat
        public DateTime Time { get; set; }

        public MessageModel() { }
    }
}

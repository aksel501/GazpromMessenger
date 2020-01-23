using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GazpromMessenger.Models
{
    public class MessageCombinedViewModel
    {
        public MessageCreateViewModel  Create { get; set; }
        public List<MessageDisplayViewModel> Display { get; set; }
    }
}

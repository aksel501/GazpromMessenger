using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GazpromMessenger.Models
{
    public class MessageDisplayViewModel
    {
        [Key]
        public int TaskID { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

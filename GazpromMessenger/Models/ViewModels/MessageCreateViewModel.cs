using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GazpromMessenger.Models
{
    public class MessageCreateViewModel
    {
        [Required(ErrorMessage = "You cannot send empty messages")]
        public string Description { get; set; }
    }
}

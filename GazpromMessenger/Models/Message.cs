using System;
using System.ComponentModel.DataAnnotations;

namespace GazpromMessenger.Models
{
    public class Message
    {
        [Key]
        public int TaskID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}

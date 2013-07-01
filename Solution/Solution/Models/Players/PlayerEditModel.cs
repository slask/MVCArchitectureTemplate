using System;
using System.ComponentModel.DataAnnotations;

namespace Solution.Models.Players
{
    public class PlayerEditModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? JoinDate { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]
        public string ContactPhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Address Length Should be less than 50 chars")]    
        public string StreetAddress { get; set; }

        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "eMail is not in proper format")]
        public string Email { get; set; }
    }
}
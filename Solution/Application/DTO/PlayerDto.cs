using System;

namespace Application.DTO
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? JoinDate { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
    }
}

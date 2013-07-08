using System;
using System.Collections.Generic;
using Domain.Core;

namespace Domain.Entities
{
    public class ScrabblePlayer: Entity
    {
        public string Name { get; set; }
        public DateTime? JoinDate { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Email { get; set; }
        public List<Game> MatchHistory { get; set; }

    }
}
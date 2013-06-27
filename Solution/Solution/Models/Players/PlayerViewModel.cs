using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Models.Players
{
    public class PlayerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
using CinemaTickets.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.Identity
{
    public enum Role
    {
        ADMIN,
        STANDARD
    }
    public class CinemaTicketsApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual Cart UserCart { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}

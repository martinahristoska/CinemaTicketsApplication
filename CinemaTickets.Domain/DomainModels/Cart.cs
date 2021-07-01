using CinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class Cart: BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual ICollection<TicketInCart> TicketInCarts { get; set; }
        public CinemaTicketsApplicationUser Owner { get; set; }
    }
}

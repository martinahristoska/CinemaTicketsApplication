using CinemaTickets.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class Order: BaseEntity
    {
        public string UserId { get; set; }
        public CinemaTicketsApplicationUser User { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
    }
}

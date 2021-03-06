using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DomainModels
{
    public class Ticket: BaseEntity
    {
     
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Genre { get; set; }
        public string Image { get; set; }
        public virtual ICollection<TicketInCart> TicketInCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }



    }
}

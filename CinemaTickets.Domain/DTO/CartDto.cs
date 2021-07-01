using CinemaTickets.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTickets.Domain.DTO
{
    public class CartDto
    {
        public List<TicketInCart> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}

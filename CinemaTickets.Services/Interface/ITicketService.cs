using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTickets.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdeteExistingTicket(Ticket t);
        AddToCartDto GetCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToCart(AddToCartDto item, string userID);
    }
}

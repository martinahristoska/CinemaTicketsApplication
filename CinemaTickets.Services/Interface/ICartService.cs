using CinemaTickets.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaTickets.Services.Interface
{
    public interface ICartService
    {
        CartDto getCartInfo(string userId);
        bool deleteTicketFromCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.DTO;
using CinemaTickets.Domain.Identity;
using CinemaTickets.Repository;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace CinemaTickets.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._cartService.getCartInfo(userId));
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._cartService.getCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "Cinema Tickets Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                var result = this.Order();

                if (result)
                {
                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    return RedirectToAction("Index", "Cart");
                }
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult DeleteFromCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._cartService.deleteTicketFromCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }


        public Boolean Order()
        {
           
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._cartService.orderNow(userId);

            return result;
        }
    }
}



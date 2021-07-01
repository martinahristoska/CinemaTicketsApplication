using CinemaTickets.Domain.DomainModels;
using CinemaTickets.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CinemaTickets.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CinemaTicketsApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<TicketInCart> TicketInCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Cart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInCart>()
                .HasKey(z => new { z.TicketId, z.CartId });

            builder.Entity<TicketInCart>()
                .HasOne(z => z.Ticket)
                .WithMany(z => z.TicketInCarts)
                .HasForeignKey(z => z.CartId);

            builder.Entity<TicketInCart>()
                .HasOne(z => z.Cart)
                .WithMany(z => z.TicketInCarts)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<Cart>()
                .HasOne<CinemaTicketsApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<Cart>(z => z.OwnerId);

            builder.Entity<TicketInOrder>()
                .HasKey(z => new { z.TicketId, z.OrderId });

            builder.Entity<TicketInOrder>()
               .HasOne(z => z.SelectedTicket)
               .WithMany(z => z.TicketInOrders)
               .HasForeignKey(z => z.TicketId);

            builder.Entity<TicketInOrder>()
               .HasOne(z => z.UserOrder)
               .WithMany(z => z.TicketInOrders)
               .HasForeignKey(z => z.OrderId);
        }
    }
}

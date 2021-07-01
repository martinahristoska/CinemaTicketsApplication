using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CinemaTickets.Domain.Identity;
using CinemaTickets.Repository;
using CinemaTickets.Repository.Interface;
using CinemaTickets.Repository.Implementation;
using CinemaTickets.Services.Interface;
using CinemaTickets.Services.Implementation;
using CinemaTickets.Domain;
using CinemaTickets.Services;
using Stripe;

namespace CinemaTickets.Web
{
    public class Startup
    {
        //private EmailSettings emailService;

        public Startup(IConfiguration configuration)
        {
           // emailService = new EmailSettings();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<CinemaTicketsApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

            //services.AddScoped<EmailSettings>(es => emailService);
            /*services.AddScoped<IEmailService, EmailService>(email => new EmailService(emailService));
            services.AddScoped<IBackgroundEmailSender, BackgroundEmailSender>();
            services.AddHostedService<ConsumeScopedHostedService>();*/

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));


            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, Services.Implementation.OrderService>();

            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );

            
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

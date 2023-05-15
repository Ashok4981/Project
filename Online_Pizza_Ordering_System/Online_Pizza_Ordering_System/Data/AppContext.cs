using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
              
        }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Pizzas> Pizzas { get; set; }
        //public DbSet<Ingredients> Ingredients { get; set; }
        //public DbSet<PizzaIngredients> PizzaIngredients { get; set; }
        //public DbSet<Reviews> Reviews { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Online_Pizza_Ordering_System.Models.CardPayment> CardPayment { get; set; } = default!;

    }
}


using Microsoft.EntityFrameworkCore;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Dal.Concrete
{
    public class OnemliBookStoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=./;Initial Catalog=BookDB;Integrated Security=True");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


    }
}

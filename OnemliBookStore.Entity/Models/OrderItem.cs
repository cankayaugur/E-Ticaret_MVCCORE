using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}

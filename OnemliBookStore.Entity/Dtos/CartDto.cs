using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }

        public double TotalPrice()
        {
            return CartItems.Sum(s => s.Price * s.Quantity);
        }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }


        public Cart Cart { get; set; }
        public Book Book { get; set; }
    }
}

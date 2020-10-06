using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }

        public List<BookCategory> BookCategories { get; set; }
    }
}

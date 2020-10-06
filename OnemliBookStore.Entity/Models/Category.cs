using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}

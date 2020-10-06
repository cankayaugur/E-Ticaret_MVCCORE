using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
    }
}

using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnemliBookStore.Entity.Dtos
{
    public class BookDetailDto
    {
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
    }
}

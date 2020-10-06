using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnemliBookStore.Entity.Dtos
{

    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }

        //bu fonksiyonu her sayfada 3 item gösterceksek sayfa başına 3.3 düşüyor onu 4 e yuvarlamak için.
        public int TotalPages()
        {
            return (int) Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }

    public class BookListDto
    {
        public PageInfo PageInfo { get; set; }
        public List<Book> Books { get; set; }
    }
}

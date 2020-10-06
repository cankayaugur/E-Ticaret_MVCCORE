using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Entity.Dtos
{
    public class CategoryListDto
    {
        public PageInfo PageInfo { get; set; }
        public List<Category> Categories { get; set; }
    }
}

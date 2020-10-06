using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Dal.Abstract
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Category GetByIdWithProducts(int categoryId);
        void DeleteFromCategory(int bookId, int categoryId);
    }
}

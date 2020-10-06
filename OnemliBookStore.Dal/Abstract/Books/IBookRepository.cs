using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Dal.Abstract
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        List<Book> GetBooksByCategoryName(string name, int page, int pageSize);
        Book GetBookDetails(string url);
        List<Book> GetSearchResult(string search);
        List<Book> GetHomePageBooks();
        int GetCountByCategory(string category);
        void Update(Book entity, int[] categoryIds);
        Book GetByIdWithCategories(int id);

    }
}

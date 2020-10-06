using OnemliBookStore.Business.Abstract.Validator;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Business.Abstract.Books
{
    public interface IBookService : IValidator<Book>
    {
        Book GetById(int id);
        Book GetBookDetails(string url);
        Book GetByIdWithCategories(int id);
        List<Book> GetAll();
        List<Book> GetHomePageBooks();
        List<Book> GetSearchResult(string search);
        List<Book> GetBooksByCategory(string category, int page, int pageSize);

        bool Create(Book entity);

        void Update(Book entity);
        bool Update(Book entity, int[] categoryIds);
        void Delete(Book entity);
        int GetCountByCategory(string category);
    }
}

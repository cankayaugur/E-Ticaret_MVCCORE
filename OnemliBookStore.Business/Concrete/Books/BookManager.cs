using OnemliBookStore.Business.Abstract.Books;
using OnemliBookStore.Dal.Abstract;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Business.Concrete.Books
{
    public class BookManager : IBookService
    {
        private IBookRepository _bookRepository;

        public BookManager(IBookRepository BookRepository)
        {
            _bookRepository = BookRepository;
        }
        public bool Create(Book entity)
        {
            if (Validate(entity))
            {
                _bookRepository.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Book entity)
        {
            _bookRepository.Delete(entity);
        }

        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _bookRepository.GetCountByCategory(category);
        }

        public List<Book> GetHomePageBooks()
        {
            return _bookRepository.GetHomePageBooks();
        }

        public List<Book> GetHomePageBooks(string search)
        {
            throw new NotImplementedException();
        }

        public Book GetBookDetails(string url)
        {
            return _bookRepository.GetBookDetails(url);
        }

        public List<Book> GetBooksByCategory(string name, int page, int pageSize)
        {
            return _bookRepository.GetBooksByCategoryName(name, page, pageSize);
        }

        public List<Book> GetSearchResult(string search)
        {
            return _bookRepository.GetSearchResult(search);
        }

        public void Update(Book entity)
        {
            _bookRepository.Update(entity);
        }

        public Book GetByIdWithCategories(int id)
        {
            return _bookRepository.GetByIdWithCategories(id);
        }

        public bool Update(Book entity, int[] categoryIds)
        {
            if (Validate(entity))
            {
                if (categoryIds.Length <= 0)
                {
                    ErrorMessage += "Kitaba bir kategori seçmeniz zorunludur";
                    return false;
                }
                _bookRepository.Update(entity, categoryIds);
                return true;
            }
            return false;
        }

        public string ErrorMessage { get; set; }

        public bool Validate(Book entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage += "ürün ismi girmelisiniz \n";
                isValid = false;
            }
            if (entity.Price < 0)
            {
                ErrorMessage += "ürün fiyatı sıfırdan büyük olmalıdırS \n";
                isValid = false;
            }
            return isValid;
        }
    }
}

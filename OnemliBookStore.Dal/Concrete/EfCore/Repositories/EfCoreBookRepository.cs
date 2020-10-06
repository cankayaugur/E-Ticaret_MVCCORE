using Microsoft.EntityFrameworkCore;
using OnemliBookStore.Dal.Abstract;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnemliBookStore.Dal.Concrete.EfCore.Repositories
{
    public class EfCoreBookRepository :
        EfCoreGenericRepository<Book, OnemliBookStoreContext>, IBookRepository
    {
        public int GetCountByCategory(string category)
        {
            using (var context = new OnemliBookStoreContext())
            {
                var books = context.Books.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    books = books
                        .Include(s => s.BookCategories)
                        .ThenInclude(s => s.Category)
                        .Where(s => s.BookCategories.Any(a => a.Category.Url == category));
                }

                return books.Where(s => s.IsApproved).Count();
            }
        }

        public List<Book> GetHomePageBooks()
        {
            using (var context = new OnemliBookStoreContext())
            {
                return context.Books.Where(s => s.IsHome && s.IsApproved).ToList();
            }
        }

        public List<Book> GetPopularBooks()
        {
            using (var context = new OnemliBookStoreContext())
            {
                return context.Books.ToList();
            }
        }

        public Book GetBookDetails(string url)
        {
            using (var context = new OnemliBookStoreContext())
            {
                return context.Books.Where(s => s.Url == url)
                    .Include(s => s.BookCategories)
                    .ThenInclude(s => s.Category)
                    .FirstOrDefault();
            }
        }

        public List<Book> GetBooksByCategoryName(string name, int page, int pageSize)
        {
            using (var context = new OnemliBookStoreContext())
            {
                // buraya .Tolist deseydik veritabanına gidip hepsini çekecekti onun üzerinden sorgu yapmak çok yanlış
                // AsQuearyable dediğimiz zaman veritabanına henüz gitmemiş oluyor.
                // o yüzden aşağıdaki linq sorgusuyla beraber dbye sorgu atmak için AsQueryable dedik
                // duruma göre fiyata göre sorgu vb. çoğaltıp sorgu atmak mümkün.
                var books = context.Books.Where(s => s.IsApproved).AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    books = books
                        .Include(s => s.BookCategories)
                        .ThenInclude(s => s.Category)
                        .Where(s => s.BookCategories.Any(a => a.Category.Url == name));
                }

                return books.Skip((page - 1) * pageSize).Take(pageSize).ToList(); // bu esnada veritabanına sorgu gitmşi oluyor.
            }
        }

        public List<Book> GetBooksByCategoryName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetSearchResult(string search)
        {
            using (var context = new OnemliBookStoreContext())
            {
                var books = context.Books.Where(s => s.IsApproved && (s.Name.Contains(search) || s.Description.Contains(search))).AsQueryable();

                return books.ToList(); // bu esnada veritabanına sorgu gitmşi oluyor.
            }
        }

        public Book GetByIdWithCategories(int id)
        {
            using (var context = new OnemliBookStoreContext())
            {

                return context.Books.Where(s => s.BookId == id)
                                    .Include(s => s.BookCategories)
                                    .ThenInclude(s => s.Category)
                                    .FirstOrDefault();
            }
        }

        public void Update(Book entity, int[] categoryIds)
        {
            using (var context = new OnemliBookStoreContext())
            {
                var book = context.Books
                    .Include(s => s.BookCategories)
                    .FirstOrDefault(s => s.BookId == entity.BookId);

                if (book != null)
                {
                    book.Name = entity.Name;
                    book.Url = entity.Url;
                    book.ImageUrl = entity.ImageUrl;
                    book.Description = entity.Description;
                    book.Price = entity.Price;
                    book.BookCategories = categoryIds.Select(s => new BookCategory()
                    {
                        BookId = entity.BookId,
                        CategoryId = s
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}

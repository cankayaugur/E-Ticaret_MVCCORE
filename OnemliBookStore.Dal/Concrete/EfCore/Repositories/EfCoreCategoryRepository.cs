using Microsoft.EntityFrameworkCore;
using OnemliBookStore.Dal.Abstract;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnemliBookStore.Dal.Concrete.EfCore.Repositories
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category, OnemliBookStoreContext>, ICategoryRepository
    {
        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new OnemliBookStoreContext())
            {
                return context.Categories.Where(s => s.CategoryId == categoryId)
                                         .Include(s => s.BookCategories)
                                         .ThenInclude(s => s.Book)
                                         .FirstOrDefault(); // 1 tane category alıyorum içinde bookları olan
            }
        }

        public void DeleteFromCategory(int bookId, int categoryId)
        {
            using (var context = new OnemliBookStoreContext())
            {
                // sadece sql sorgusu çalıştırıp geçiyorum, dbden dönen model yok, Map etmeyi (ORM) gerektircek bir durum yok 
                // o yüzden çalıştırıp geçiyorum.
                var query = "delete from bookcategory where BookId=@p0 and CategoryId=@p1";

                int sayi = context.Database.ExecuteSqlRaw(query, bookId, categoryId);
                // Bu kategoriden {sayi} tane ürün silinmiştir.
            }
        }
    }
}

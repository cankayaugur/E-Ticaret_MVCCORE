using Microsoft.EntityFrameworkCore;
using OnemliBookStore.Dal.Abstract.Carts;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnemliBookStore.Dal.Concrete.EfCore.Repositories
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, OnemliBookStoreContext>, ICartRepository
    {
        public void DeleteFromCart(int cartId, int bookId)
        {
            using (var context = new OnemliBookStoreContext())
            {
                var cmd = @"delete from CartItems where CartId = @p0 and BookId = @p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, bookId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using(var context = new OnemliBookStoreContext())
            {
                return context.Carts.Include(s => s.CartItems)
                    .ThenInclude(s => s.Book)
                    .FirstOrDefault(s => s.UserId == userId);
            }
        }

        // Update ezildi çünkü state'i değişen entitynin ilişkili olan kayıtlarında bilgi güncellemesi olmuyor
        public override void Update(Cart entity)
        {
            using (var context = new OnemliBookStoreContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}

using OnemliBookStore.Business.Abstract.Carts;
using OnemliBookStore.Dal.Abstract.Carts;
using OnemliBookStore.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnemliBookStore.Business.Concrete.Carts
{
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(string userId, int bookId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                // sepette eklemek istenilen ürün varmı bakılıyor. Varsa +1 yoksa ürün oluşturuluyor.
                int index = cart.CartItems.FindIndex(s => s.BookId == bookId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        BookId = bookId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                _cartRepository.Update(cart);
            }
        }

        public void DeleteFromCart(string userId, int bookId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _cartRepository.DeleteFromCart(cart.Id, bookId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartRepository.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartRepository.Create(new Cart() { UserId = userId });
        }
    }
}

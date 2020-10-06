using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnemliBookStore.Business.Abstract.Carts;
using OnemliBookStore.Entity.Dtos;
using OnemliBookStore.Ui.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnemliBookStore.Ui.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<User> _userManager;

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartDto()
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(s => new CartItemDto()
                {
                    Id = s.Id,
                    Name = s.Book.Name,
                    Price = (double)s.Book.Price,
                    ImageUrl = s.Book.ImageUrl,
                    BookId = s.Book.BookId,
                    Quantity = s.Quantity
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {

                _cartService.AddToCart(userId, bookId, quantity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            _cartService.DeleteFromCart(userId, bookId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderDto();

            orderModel.Cart = new CartDto()
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(s => new CartItemDto()
                {
                    Id = s.Id,
                    Name = s.Book.Name,
                    Price = (double)s.Book.Price,
                    ImageUrl = s.Book.ImageUrl,
                    BookId = s.Book.BookId,
                    Quantity = s.Quantity
                }).ToList()
            };


            return View(orderModel);
        }
    }
}

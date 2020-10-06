using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnemliBookStore.Business.Abstract.Books;
using OnemliBookStore.Entity.Dtos;
using OnemliBookStore.Entity.Models;

namespace OnemliBookStore.Ui.Controllers
{
    public class ShopController : Controller
    {
        private IBookService _bookService;
        public ShopController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 3; //sayfada kaç ürün gösterilsin
            var BookViewModel = new BookListDto()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _bookService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    CurrentCategory = category
                },
                Books = _bookService.GetBooksByCategory(category, page, pageSize)
            };

            return View(BookViewModel);
        }

        public IActionResult Details(string url)
        {

            if (url == null)
            {
                return NotFound();
            }

            Book Book = _bookService.GetBookDetails(url);
            if (Book == null)
            {
                return NotFound();
            }

            return View(new BookDetailDto
            {
                Book = Book,
                Categories = Book.BookCategories.Select(s => s.Category).ToList()
            });
        }

        public IActionResult Search(string q)
        {

            var BookViewModel = new BookListDto()
            {
                Books = _bookService.GetSearchResult(q)
            };

            return View(BookViewModel);
        }
    }
}

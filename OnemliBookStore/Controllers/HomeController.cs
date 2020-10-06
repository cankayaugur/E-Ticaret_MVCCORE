using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnemliBookStore.Business.Abstract.Books;
using OnemliBookStore.Entity.Dtos;

namespace OnemliBookStore.Controllers
{
    // localhost:5000/home
    public class HomeController : Controller
    {
        private IBookService _bookService;
        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var bookViewModel = new BookListDto()
            {
                Books = _bookService.GetHomePageBooks()
            };

            return View(bookViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

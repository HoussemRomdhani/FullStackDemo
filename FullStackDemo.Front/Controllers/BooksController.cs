using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStackDemo.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStackDemo.Front.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;

        public BooksController(ILogger<BooksController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.Get().ConfigureAwait(false);
            return View(result);
        }
    }
}
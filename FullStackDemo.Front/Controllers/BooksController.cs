using System;
using System.Net.Http;
using System.Threading.Tasks;
using FullStackDemo.Front.Models;
using FullStackDemo.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStackDemo.Front.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;

        public BooksController(ILogger<BooksController> logger, IBookService bookService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        // GET: Books
        public async Task<ActionResult> Index()
        {
            var result = await _bookService.GetAsync();
            return View(result);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  var result = await _bookService.CreateAsync(book);
                    if (!result)
                        return RedirectToAction("AccessDenied", "Authorization");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   var result = await _bookService.UpdateAsync(id, book);
                    if (!result)
                        return RedirectToAction("AccessDenied", "Authorization");
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            try
            {
              var result =  await _bookService.RemoveAsync(bookId);
                if (!result)
                    return RedirectToAction("AccessDenied", "Authorization");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
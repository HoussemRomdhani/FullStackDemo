using System.Threading.Tasks;
using FullStackDemo.Front.Models;
using FullStackDemo.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStackDemo.Front.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<Books1Controller> _logger;
        private readonly IBookService _bookService;

        public BooksController(ILogger<Books1Controller> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        // GET: Books
        public async Task<ActionResult> Index()
        {
            var result = await _bookService.GetAsync().ConfigureAwait(false);
            return View(result);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await _bookService.CreateAsync(book).ConfigureAwait(false);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   await _bookService.UpdateAsync(id, book).ConfigureAwait(false);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _bookService.GetAsync(id).ConfigureAwait(false);
            return View(result);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            try
            {
                await _bookService.RemoveAsync(bookId).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
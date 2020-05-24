using FullStackDemo.Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FullStackDemo.Back.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        protected readonly BookStoreContext _context;
        public BooksController(BookStoreContext bookStoreContext)
        {
            _context = bookStoreContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                if (books.Count > 0)
                    return Ok(books);
                else
                    return NotFound();

            }
            catch (Exception e)
            {
                throw e;
               // return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(e => e.BookId == id);
                if (book != null)
                    return Ok(book);
                else
                    return NotFound();

            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]Book book)
        {
            if (ModelState.IsValid == false) return BadRequest(ModelState);
            try
            {
                var bookToCreate = new Book
                {
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price
                };
                 _context.Books.Add(bookToCreate);
                await _context.SaveChangesAsync();
                return Created("created", book);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody]Book book)
        {
            if (id != book.BookId) return NotFound();
            if (ModelState.IsValid == false) return BadRequest(ModelState);
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return Ok(book);

            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(e => e.BookId == id);
                if (book == null)
                    return NotFound();
                else
                {
                    _context.Books.Remove(book);
                   await _context.SaveChangesAsync();
                    return Ok();
                }

            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

using BookServiceCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

using System.Threading.Tasks;

namespace BookService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private BookServiceContext _db;

        public BooksController(BookServiceContext db)
        {
            _db = db;
        }

        [HttpGet]
        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            var temp = _db.Books.ToList();
            var books = from b in _db.Books
                        select new BookDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            AuthorName = b.Author.Name
                        };

            return books;
        }

        [HttpGet("{id}")]
        // GET: api/Books/5
        //[ResponseType(typeof(BookDetailDTO))]

        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _db.Books.Include(b => b.Author).Select(b =>
                new BookDetailDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Price = b.Price,
                    AuthorName = b.Author.Name,
                    Genre = b.Genre
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        //[ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IActionResult> PutBook(int id, Books book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            _db.Entry(book).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        //[ResponseType(typeof(Book))]
        [HttpPost]
        public async Task<IActionResult> PostBook(Books book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            // Load author name
            _db.Entry(book).Reference(x => x.Author).Load();

            var dto = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        }

        // DELETE: api/Books/5
        //[ResponseType(typeof(Book))]
        public async Task<IActionResult> DeleteBook(int id)
        {
            Books book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return Ok(book);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool BookExists(int id)
        {
            return _db.Books.Count(e => e.Id == id) > 0;
        }
    }
}
using BookServiceCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace BookServiceCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private BookServiceContext _db;

        public AuthorsController(BookServiceContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Produces("application/json")]

        // GET: api/Authors
        public IQueryable<Authors> GetAuthors()
        {
            return _db.Authors;
        }

        // GET: api/Authors/5
        //[ResponseType(typeof(Author))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            Authors author = await _db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        //[ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IActionResult> PutAuthor(int id, Authors author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            _db.Entry(author).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
            //return StatusCode(HttpStatusCode.NoContent);// StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        //[ResponseType(typeof(Author))]
        [HttpPost]
        public async Task<IActionResult> PostAuthor(Authors author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Authors.Add(author);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        //[ResponseType(typeof(Author))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            Authors author = await _db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

            return Ok(author);
        }

      
        private bool AuthorExists(int id)
        {
            return _db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Labb2.Controllers.Request_Bodies;
using WebApi_Labb2.DTO;
using WebApi_Labb2.Extensions;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BooksDbContext _context;

        public AuthorsController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthor()
        {
			return await _context.Author.AsNoTracking().Select(m => m.ToAuthorDTO()).ToListAsync();
		}

		// GET: api/Authors/5
		[HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
			var author = await _context.Author
                .AsNoTracking()
				.Include(b => b.Books)
				.FirstOrDefaultAsync(b => b.AuthorId == id);

			if (author == null)
			{
				return NotFound();
			}
			else
			{
				return author.ToAuthorDTO();
			}
		}


        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        }


        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO authorDTO)
        {
            var author = authorDTO.ToAuthor();
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

		[HttpPost("assign-books")]
		public async Task<ActionResult<Author>> AssignAuthorsToBook([FromBody] AssignBooksToAuthor assignBooks)
		{
			var author = await _context.Author.FindAsync(assignBooks.AuthorId);

			if (author == null)
			{
				return NotFound();
			}

			var books = await _context.Books.Where(b => assignBooks.BookIds.Contains(b.BookId)).ToListAsync();

			if (books.Count != assignBooks.BookIds.Count)
			{
				return NotFound();
			}

			foreach (var book in books)
			{
				if (!author.Books.Contains(book))
				{
					author.Books.Add(book);
				}
			}

			await _context.SaveChangesAsync();

			return Ok();

		}


		// DELETE: api/Authors/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}

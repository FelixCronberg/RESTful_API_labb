using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Labb2.DTO;
using WebApi_Labb2.Models;
using WebApi_Labb2.Extensions;
using WebApi_Labb2.Controllers.Request_Bodies;

namespace WebApi_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksDbContext _context;

        public BooksController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
			return await _context.Book.AsNoTracking().Select(m => m.ToBookDTO()).ToListAsync();
		}

		// GET: api/Books/5
		[HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _context.Book
                .AsNoTracking()
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
				return book.ToBookDTO();
			}
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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


		[HttpPut("{id}/assign-authors")]
		public async Task<ActionResult<Book>> AssignAuthorsToBook(int id, [FromBody] AssignAuthorsToBook assignAuthors)
		{
			var book = await _context.Book.FindAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			var authors = await _context.Author.Where(a => assignAuthors.AuthorIds.Contains(a.AuthorId)).ToListAsync();

			if (authors.Count != assignAuthors.AuthorIds.Count)
			{
				return NotFound();
			}

			foreach (var author in authors)
			{
				if (!book.Authors.Contains(author))
				{
					book.Authors.Add(author);
				}
			}

			await _context.SaveChangesAsync();

			return Ok();

		}


		// POST: api/Books
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Book>> PostBook(CreateBookDTO bookDTO)
        {
            var book = bookDTO.ToBook();
            _context.Book.Add(book);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }
    }
}

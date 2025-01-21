using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Labb2.DTO;
using WebApi_Labb2.Extensions;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly BooksDbContext _context;

        public LoansController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoan()
        {
            return await _context.Loan.AsNoTracking().ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _context.Loan.AsNoTracking().FirstOrDefaultAsync(l => l.LoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

		[HttpPut("return/{id}")]
		public async Task<IActionResult> ReturnBook(int id)
		{
            var loan = await _context.Loan.FindAsync(id);

			if (loan == null)
			{
				return NotFound(new { message = "Loan not found" } );
			}

            var book = await _context.Book.FindAsync(loan.BookId);

            if(book == null)
            {
                return NotFound(new { message = "Book was not found" });
            }

            loan.ReturnedDate = DateTime.Now;
            book.IsAvailable = true;

            await _context.SaveChangesAsync();

			return NoContent();
		}


		// POST: api/Loans
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(CreateLoanDTO newLoan)
        {
			if (!LoanCardExists(newLoan.LoanCardId))
			{
				return NotFound(new { message = "Loan card was not found" });
			}

			var loan = newLoan.ToLoan();
			loan.LoanedDate = DateTime.Now;
			loan.ReturnedDate = null;

			var book = await _context.Book.FindAsync(newLoan.BookId);

			if (book == null)
			{
				return NotFound(new { message = "Book was not found" });
			}
			else
			{
				book.IsAvailable = false;
			}

            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loan.LoanId }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.LoanId == id);
        }
		private bool LoanCardExists(int id)
		{
			return _context.LoanCard.Any(e => e.LoanCardId == id);
		}
	}
}

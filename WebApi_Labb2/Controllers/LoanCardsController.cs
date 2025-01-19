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
    public class LoanCardsController : ControllerBase
    {
        private readonly BooksDbContext _context;

        public LoanCardsController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/LoanCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanCard>>> GetLoanCard()
        {
            return await _context.LoanCard.AsNoTracking().ToListAsync();
        }

        // GET: api/LoanCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanCard>> GetLoanCard(int id)
        {
            var loanCard = await _context.LoanCard.FindAsync(id);

            if (loanCard == null)
            {
                return NotFound();
            }

            return loanCard;
        }

        // PUT: api/LoanCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanCard(int id, LoanCard loanCard)
        {
            if (id != loanCard.LoanCardId)
            {
                return BadRequest();
            }

            _context.Entry(loanCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanCardExists(id))
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

        // POST: api/LoanCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoanCard>> PostLoanCard(CreateLoanCardDTO loanCardDTO)
        {
			var loanCard = loanCardDTO.ToLoanCard();

            loanCard.IssueDate = DateOnly.FromDateTime(DateTime.Now);
            loanCard.ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5));

			_context.LoanCard.Add(loanCard);
            try
            {
				await _context.SaveChangesAsync();
			}
            catch
            {
                return Conflict();
            }

			return CreatedAtAction("GetLoanCard", new { id = loanCard.LoanCardId }, loanCard);
		
        }

        // DELETE: api/LoanCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanCard(int id)
        {
            var loanCard = await _context.LoanCard.FindAsync(id);
            if (loanCard == null)
            {
                return NotFound();
            }

            _context.LoanCard.Remove(loanCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanCardExists(int id)
        {
            return _context.LoanCard.Any(e => e.LoanCardId == id);
        }

    }
}

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

namespace WebApi_Labb2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCardOwnersController : ControllerBase
    {
        private readonly BooksDbContext _context;

        public LoanCardOwnersController(BooksDbContext context)
        {
            _context = context;
        }

        // GET: api/LoanCardOwners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanCardOwner>>> GetLoanCardOwner()
        {
            return await _context.LoanCardOwner.AsNoTracking().ToListAsync();
        }

        // GET: api/LoanCardOwners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanCardOwner>> GetLoanCardOwner(int id)
        {
            var loanCardOwner = await _context.LoanCardOwner.AsNoTracking().FirstOrDefaultAsync(l => l.LoanCardOwnerId == id);

            if (loanCardOwner == null)
            {
                return NotFound();
            }

            return loanCardOwner;
        }

        // PUT: api/LoanCardOwners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanCardOwner(int id, LoanCardOwner loanCardOwner)
        {
            if (id != loanCardOwner.LoanCardOwnerId)
            {
                return BadRequest();
            }

            _context.Entry(loanCardOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanCardOwnerExists(id))
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

        // POST: api/LoanCardOwners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoanCardOwner>> PostLoanCardOwner(CreateLoanCardOwnerDTO loanCardOwnerDTO)
        {
            var loanCardOwner = loanCardOwnerDTO.ToLoanCardOwner();

			//Putting this here for easier testing, assignment only says to have a Post for LoanCardOwner (Låntagare)
			loanCardOwner.LoanCard = new LoanCard
			{
				LoanCardOwnerId = loanCardOwner.LoanCardOwnerId,
				IssueDate = DateOnly.FromDateTime(DateTime.Now),
				ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(5))
			};


			_context.LoanCardOwner.Add(loanCardOwner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoanCardOwner", new { id = loanCardOwner.LoanCardOwnerId }, loanCardOwner);
        }

        // DELETE: api/LoanCardOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanCardOwner(int id)
        {
            var loanCardOwner = await _context.LoanCardOwner.FindAsync(id);
            if (loanCardOwner == null)
            {
                return NotFound();
            }

            _context.LoanCardOwner.Remove(loanCardOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanCardOwnerExists(int id)
        {
            return _context.LoanCardOwner.Any(e => e.LoanCardOwnerId == id);
        }
    }
}

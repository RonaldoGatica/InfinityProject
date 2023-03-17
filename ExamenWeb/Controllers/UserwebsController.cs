using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibraryInfinity.Entities;

namespace ExamenWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserwebsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserwebsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Userwebs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Userweb>>> GetUserwebs()
        {
            return await _context.Userwebs.ToListAsync();
        }

        // GET: api/Userwebs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Userweb>> GetUserweb(string id)
        {
            var userweb = await _context.Userwebs.FindAsync(id);

            if (userweb == null)
            {
                return NotFound();
            }

            return userweb;
        }

        // PUT: api/Userwebs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserweb(string id, Userweb userweb)
        {
            if (id != userweb.Nickname)
            {
                return BadRequest();
            }

            _context.Entry(userweb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserwebExists(id))
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

        // POST: api/Userwebs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Userweb>> PostUserweb(Userweb userweb)
        {
            var existNickName = await _context.Userwebs.FindAsync(userweb.Nickname);
            var existEmail = await _context.Userwebs.FindAsync(userweb.Email);

            if (existNickName != null || existEmail != null)
            {
                return Conflict();
            }
            _context.Userwebs.Add(userweb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserwebExists(userweb.Nickname))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserweb", new { id = userweb.Nickname }, userweb);
        }

        // DELETE: api/Userwebs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserweb(string id)
        {
            var userweb = await _context.Userwebs.FindAsync(id);
            if (userweb == null)
            {
                return NotFound();
            }

            _context.Userwebs.Remove(userweb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserwebExists(string id)
        {
            return _context.Userwebs.Any(e => e.Nickname == id);
        }
    }
}

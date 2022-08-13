using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dynamic_Sentence_Web_Api.Database;
using Dynamic_Sentence_Web_Api.Models;

namespace Dynamic_Sentence_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordUnitsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WordUnitsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/WordUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordUnit>>> GetWordUnits()
        {
            return await _context.WordUnits.ToListAsync();
        }

        // GET: api/WordUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WordUnit>> GetWordUnit(int id)
        {
            var wordUnit = await _context.WordUnits.FindAsync(id);

            if (wordUnit == null)
            {
                return NotFound();
            }

            return wordUnit;
        }

        // PUT: api/WordUnits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWordUnit(int id, WordUnit wordUnit)
        {
            if (id != wordUnit.Id)
            {
                return BadRequest();
            }

            _context.Entry(wordUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordUnitExists(id))
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

        // POST: api/WordUnits
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WordUnit>> PostWordUnit(WordUnit wordUnit)
        {
            _context.WordUnits.Add(wordUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWordUnit", new { id = wordUnit.Id }, wordUnit);
        }

        // DELETE: api/WordUnits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WordUnit>> DeleteWordUnit(int id)
        {
            var wordUnit = await _context.WordUnits.FindAsync(id);
            if (wordUnit == null)
            {
                return NotFound();
            }

            _context.WordUnits.Remove(wordUnit);
            await _context.SaveChangesAsync();

            return wordUnit;
        }

        private bool WordUnitExists(int id)
        {
            return _context.WordUnits.Any(e => e.Id == id);
        }
    }
}

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
    public class WordTypesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WordTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/WordTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordType>>> GetWordTypes()
        {
            return await _context.WordTypes.ToListAsync();
        }

        // GET: api/WordTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WordType>> GetWordType(int id)
        {
            var wordType = await _context.WordTypes.FindAsync(id);

            if (wordType == null)
            {
                return NotFound();
            }

            return wordType;
        }

        // PUT: api/WordTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWordType(int id, WordType wordType)
        {
            if (id != wordType.Id)
            {
                return BadRequest();
            }

            _context.Entry(wordType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordTypeExists(id))
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

        // POST: api/WordTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WordType>> PostWordType(WordType wordType)
        {
            _context.WordTypes.Add(wordType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWordType", new { id = wordType.Id }, wordType);
        }

        // DELETE: api/WordTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WordType>> DeleteWordType(int id)
        {
            var wordType = await _context.WordTypes.FindAsync(id);
            if (wordType == null)
            {
                return NotFound();
            }

            _context.WordTypes.Remove(wordType);
            await _context.SaveChangesAsync();

            return wordType;
        }

        private bool WordTypeExists(int id)
        {
            return _context.WordTypes.Any(e => e.Id == id);
        }
    }
}

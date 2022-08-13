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
    public class SentencesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SentencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Sentences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sentence>>> GetSentences()
        {
            return await _context.Sentences.ToListAsync();
        }

        // GET: api/Sentences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sentence>> GetSentence(int id)
        {
            var sentence = await _context.Sentences.FindAsync(id);

            if (sentence == null)
            {
                return NotFound();
            }

            return sentence;
        }

        // PUT: api/Sentences/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSentence(int id, Sentence sentence)
        {
            if (id != sentence.Id)
            {
                return BadRequest();
            }

            _context.Entry(sentence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SentenceExists(id))
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

        // POST: api/Sentences
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sentence>> PostSentence(Sentence sentence)
        {
            _context.Sentences.Add(sentence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSentence", new { id = sentence.Id }, sentence);
        }

        // DELETE: api/Sentences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sentence>> DeleteSentence(int id)
        {
            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();

            return sentence;
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.Id == id);
        }
    }
}

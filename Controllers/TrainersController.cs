﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Data;
using ProiectMedii.Models;

namespace ProiectMedii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ProiectMediiContext _context;

        public TrainersController(ProiectMediiContext context)
        {
            _context = context;
        }

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainer()
        {
            return await _context.Trainer.ToListAsync();
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _context.Trainer.FindAsync(id);

            if (trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

        // PUT: api/Trainers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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

        // POST: api/Trainers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifică dacă un trainer cu același nume există deja
            if (_context.Trainer.Any(t => t.Name == trainer.Name))
            {
                return BadRequest("A trainer with the same name already exists.");
            }

            _context.Trainer.Add(trainer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainer", new { id = trainer.Id }, trainer);
        }

        // DELETE: api/Trainers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainer.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainer.Any(e => e.Id == id);
        }
    }
}

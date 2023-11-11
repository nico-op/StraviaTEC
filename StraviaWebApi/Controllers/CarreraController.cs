using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StraviaWebApi.Data;
using StraviaWebApi.Models;

namespace StraviaWebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarreraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Carrera
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrera>>> GetCarreras()
        {
            return await _context.Carreras.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrera>> GetCarrera(string id)
        {
            try
            {
                var carreras = await _context.Carreras
                    .FromSqlRaw("EXEC ObtenerCarrera @nombreCarrera", 
                                new SqlParameter("@nombreCarrera", id))
                    .ToListAsync();

                if (!carreras.Any())
                {
                    return NotFound();
                }

                var carrera = carreras[0];
                return carrera;
            }
            catch
            {
                // Log the exception, return a generic error message, etc.
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        // PUT: api/Carrera/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrera(string id, Carrera carrera)
        {
            if (id != carrera.NombreCarrera)
            {
                return BadRequest();
            }

            _context.Entry(carrera).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(id))
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

        // POST: api/Carrera
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carrera>> PostCarrera(Carrera carrera)
        {
            _context.Carreras.Add(carrera);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarreraExists(carrera.NombreCarrera))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarrera", new { id = carrera.NombreCarrera }, carrera);
        }

        // DELETE: api/Carrera/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(string id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarreraExists(string id)
        {
            return _context.Carreras.Any(e => e.NombreCarrera == id);
        }
    }
}

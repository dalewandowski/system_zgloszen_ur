using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using system_zgloszen_ur.Data;
using system_zgloszen_ur.Models;
using System_zgloszen_UR.DTOs.Failures;
using System_zgloszen_UR.Mappers;

namespace system_zgloszen_ur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FailuresController : ControllerBase
    {

        private readonly AppDbContext _context;

        public FailuresController(AppDbContext context)
        {
            _context = context;
        }

        /// Pobiera wszystkie awarie z bazy danych i zwraca je w postaci listy obiektów FailureDto.  
        /// Endpoint: localhost:5201/api/failures 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FailureDto>>> GetAll()
        {
            var allFailures = await _context.Failures.Include(f => f.ReportedBy).AsNoTracking().OrderByDescending(f => f.CreatedAt).ToListAsync();
            var DtoFailures = allFailures.Select(f => FailureMapper.FailureToDto(f)).ToList();

            return Ok(DtoFailures);

        }

        /// Pobiera awarię o określonym id z bazy danych i zwraca ją w postaci obiektu FailureDto.
        /// Endpoint: localhost:5201/api/failures/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FailureDto>> GetById([FromRoute] int id)
        {
            var failure = await _context.Failures.Include(f => f.ReportedBy).AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (failure == null)
            {
                return NotFound($"Awaria z ID: ({id}) nie istnieje");
            }

            var failureDto = FailureMapper.FailureToDto(failure);
            return Ok(failureDto);
        }


        /// Dodaje nową awarię do bazy danych na podstawie przesłanego obiektu CreateFailureDto i zwraca utworzony obiekt FailureDto.
        /// Endpoint: localhost:5201/api/failures 
        [HttpPost]
        public async Task<ActionResult<FailureDto>> AddNewFailure([FromBody] CreateFailureDto newFailureDto)
        {

            var user = await _context.Users.FindAsync(newFailureDto.ReportedById);
            if (user == null)
            {
                return BadRequest($"Użytkownik z ID: ({newFailureDto.ReportedById}) nie istnieje");
            }

            var newFailure = FailureMapper.MapFailureToEntity(newFailureDto);
            newFailure.ReportedBy = user;
            await _context.Failures.AddAsync(newFailure);
            await _context.SaveChangesAsync();

            var createdDto = FailureMapper.FailureToDto(newFailure);

            return CreatedAtAction(nameof(GetById), new { id = newFailure.Id }, createdDto);
        }

        /// Aktualizuje istniejącą awarię w bazie danych na podstawie przesłanego obiektu UpdateFailureDto i zwraca zaktualizowany obiekt FailureDto.
        /// Endpoint: localhost:5201/api/failures/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateFailureDto>> UpdateFailure([FromRoute] int id, [FromBody] UpdateFailureDto updateFailureDto)
        {
            var updatedFailure = await _context.Failures.Include(f => f.ReportedBy).FirstOrDefaultAsync(f => f.Id == id);
            if (updatedFailure == null)
            {
                return NotFound($"Awaria z ID: ({id}) nie istnieje");
            }

            FailureMapper.UpdateFailureFromDto(updatedFailure, updateFailureDto);
            await _context.SaveChangesAsync();
            return Ok(FailureMapper.FailureToDto(updatedFailure));
        }

        //Usuwa istniejącą awarię z bazy danych na podstawie przesłanego id i zwraca status 204 No Content.
        //Endpoint: localhost:5201/api/failures/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFailure([FromRoute] int id)
        {

            var failureToDelete = await _context.Failures.FirstOrDefaultAsync(f => f.Id == id);
            if (failureToDelete == null)
            {
                return NotFound($"Awaria z ID: ({id}) nie istnieje");
            }

            _context.Failures.Remove(failureToDelete);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}